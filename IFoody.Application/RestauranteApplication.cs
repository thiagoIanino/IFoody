using IFoody.Application.Interfaces;
using IFoody.Application.Mapping;
using IFoody.Application.Models;
using IFoody.Application.Models.Restaurantes;
using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Entities.Restaurantes;
using IFoody.Domain.Enumeradores;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using IFoody.Domain.Repositories.Restaurantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IFoody.Application
{
    public class RestauranteApplication : IRestauranteApplication
    {
        private readonly IRestauranteRepository _restauranteService;
        private readonly IDominioRestauranteService _dominioRestauranteService;
        private readonly IAvaliacaoRepository _avaliacaoService;
        private readonly IStatusAvaliacaoRepository _statusAvaliacaoService;
        private readonly IPagamentoRepository _pagamentoService;
        private readonly IDominioPedidoService _dominioPedidoService;
        private readonly IPratoRespository _pratoService;
        private readonly IDominioPratoService _dominioPratoService;
        private readonly IWebRepository _webService;
        public RestauranteApplication(IRestauranteRepository restauranteService,
            IDominioRestauranteService dominioRestauranteService,
            IAvaliacaoRepository avaliacaoService,
            IStatusAvaliacaoRepository statusAvaliacaoService,
            IPagamentoRepository pagamentoService,
            IPratoRespository pratoService,
            IDominioPratoService dominioPratoService,
            IDominioPedidoService dominioPedidoService,
            IWebRepository webService)
        {
            _restauranteService = restauranteService;
            _dominioRestauranteService = dominioRestauranteService;
            _avaliacaoService = avaliacaoService;
            _statusAvaliacaoService = statusAvaliacaoService;
            _pagamentoService = pagamentoService;
            _pratoService = pratoService;
            _dominioPratoService = dominioPratoService;
            _dominioPedidoService = dominioPedidoService;
            _webService = webService;
        }

        public async Task CadastrarRestaurante(RestauranteInput restauranteInput)
        {
            var restaurante = new Restaurante(
                restauranteInput.NomeRestaurante,
                restauranteInput.NomeDonoRestaurante,
                restauranteInput.Tipo,
                restauranteInput.CNPJ,
                restauranteInput.Email,
                restauranteInput.Senha,
                restauranteInput.TempoMedioEntrega,
                restauranteInput.SubDescricao,
                restauranteInput.UrlLogo);
            _dominioRestauranteService.ValidarDadosCadastroRestaurante(restaurante);

            var usuarioStripe = new UsuarioStripeDto(
                restaurante.NomeRestaurante,
                restaurante.Email,
                CategoriaStripe.Restaurante);

            var idStripeRestaurante = await _pagamentoService.CadastrarUsuarioStripe(usuarioStripe);
            restaurante.AdicionarIdStripe(idStripeRestaurante);

            var avaliacaoPadrao = new Avaliacao(restaurante.Id);
            var statusAvaliacaoRestaurante = new StatusAvaliacao(restaurante.Id);

            await RegistrarRestaurante(restaurante, avaliacaoPadrao, statusAvaliacaoRestaurante);
        }

        private async Task RegistrarRestaurante(Restaurante restaurante, Avaliacao avaliacao, StatusAvaliacao statusAvaliacao)
        {
            try
            {
                using (var transacao = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _restauranteService.GravarRestaurante(restaurante);
                    await _avaliacaoService.RegistrarInicioRestauranteAvaliacao(avaliacao);
                    await _statusAvaliacaoService.GravarStatusAvaliacaoRestaurante(statusAvaliacao);

                    // transacao.Dispose();
                    transacao.Complete();
                }

            }
            catch (TransactionAbortedException ex)
            {
                throw new Exception("Erro ao na criacao de registros no banco", ex);
            }
        }

        public async Task<RestauranteAutenticacaoOutput> AutenticarRestaurante(string email, string senha)
        {
            _dominioRestauranteService.ValidarDadosAutenticacao(email, senha);
            var restaurante = await _restauranteService.ObterClientePorEmailESenha(email, senha);
            var token = _dominioRestauranteService.AutenticarRestaurante(restaurante);

            return new RestauranteAutenticacaoOutput
            {
                Id = restaurante.Id,
                NomeRestaurante = restaurante.NomeRestaurante,
                Classificacao = new ClassificacaoDto
                {
                    Nota = restaurante.Nota,
                    Status = restaurante.Status
                },
                SubDescricao = restaurante.SubDescricao,
                TempoMedioEntrega = restaurante.TempoMedioEntrega,
                Tipo = restaurante.Tipo,
                Token = token,
                UrlLogo = restaurante.UrlLogo

            };
        }

        public async Task AvaliarRestaurante(AvaliacaoInput avaliacaoInput)
        {
            var avaliacao = new Avaliacao(
                avaliacaoInput.Nota,
                avaliacaoInput.Descricao,
                avaliacaoInput.IdRestaurante,
                avaliacaoInput.IdCliente);

            _dominioRestauranteService.ValidarDadosAvaliacaoRestaurante(avaliacao);
            var pedidoAtualizado = await _dominioPedidoService.AtualizarAvaliacoesPendentesCache(avaliacaoInput.IdCliente, avaliacaoInput.IdRestaurante);
            pedidoAtualizado.Pedidos = pedidoAtualizado.Pedidos.OrderBy(x => x.Status).ToList();
            await _avaliacaoService.AvaliarRestaurante(avaliacao);
            await _webService.EnviarRespostaCliente(avaliacaoInput.IdCliente, pedidoAtualizado);
        }

        public async Task<IEnumerable<RestauranteDto>> ListarRestaurantesPorTipo(string tipo)
        {

            _dominioRestauranteService.ValidarTipoRestaurante(tipo);
            IEnumerable<RestauranteDto> restaurantes;
            if(tipo.ToLower() == "novos")
            {
                restaurantes = await _restauranteService.ListarRestaurantesNovos();
            }
            else
             restaurantes = await _restauranteService.ListarRestaurantesPorTipo(tipo);

            _dominioRestauranteService.ValidarRestornoListaRestaurantes(restaurantes);

            return restaurantes;
        }

        public async Task<IEnumerable<RestauranteDto>> ListarRestaurantesPorClassificacao()
        {
            var restaurantes = await _restauranteService.ListarRestaurantesPorClassificacaoConsiderandoCache();
            _dominioRestauranteService.ValidarRestornoListaRestaurantes(restaurantes);

            return restaurantes;
        }

        public async Task<RestauranteOutput> ObterRestaurante(Guid idRestaurante)
        {
            _dominioPratoService.ValidarIdRestaurante(idRestaurante);
            var pratos = await _pratoService.ListarPratosRestaurante(idRestaurante);
            var pratosPorClassificacao = _dominioPratoService.AgruparPratosPorClassificacao(pratos); 
            var dadosRestaurante = await _restauranteService.ObterRestaurante(idRestaurante);

            return new RestauranteOutput
            {
                PratosPorClassificacao = pratosPorClassificacao,
                Restaurante = dadosRestaurante
            };
        }
    }
}
