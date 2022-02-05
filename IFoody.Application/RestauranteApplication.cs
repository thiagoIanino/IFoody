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
        public RestauranteApplication(IRestauranteRepository restauranteService, IDominioRestauranteService dominioRestauranteService, IAvaliacaoRepository avaliacaoService, IStatusAvaliacaoRepository statusAvaliacaoService, IPagamentoRepository pagamentoService)
        {
            _restauranteService = restauranteService;
            _dominioRestauranteService = dominioRestauranteService;
            _avaliacaoService = avaliacaoService;
            _statusAvaliacaoService = statusAvaliacaoService;
            _pagamentoService = pagamentoService;
        }

        public async Task CadastrarRestaurante(RestauranteInput restauranteInput)
        {
            var restaurante = new Restaurante(
                restauranteInput.NomeRestaurante,
                restauranteInput.NomeDonoRestaurante,
                restauranteInput.Tipo,
                restauranteInput.CNPJ,
                restauranteInput.Email,
                restauranteInput.Senha);
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

        private async Task RegistrarRestaurante(Restaurante restaurante,Avaliacao avaliacao,StatusAvaliacao statusAvaliacao)
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

            }catch(TransactionAbortedException ex)
            {
                throw new Exception("Erro ao na criacao de registros no banco",ex);
            }
        }

            public async Task AutenticarRestaurante(string email, string senha)
        {
            _dominioRestauranteService.ValidarDadosAutenticacao(email, senha);
            var situacaoAutenticacaoCliente = await _restauranteService.AutenticarRestaurante(email, senha);
            _dominioRestauranteService.VerificarSeRestauranteEstaAutenticado(situacaoAutenticacaoCliente);
        }

        public async Task AvaliarRestaurante(AvaliacaoInput avaliacaoInput)
        {
            var avaliacao = new Avaliacao(
                avaliacaoInput.Nota,
                avaliacaoInput.Descricao,
                avaliacaoInput.IdRestaurante,
                avaliacaoInput.IdCliente);

            _dominioRestauranteService.ValidarDadosAvaliacaoRestaurante(avaliacao);
            await _avaliacaoService.AvaliarRestaurante(avaliacao);
        }

        public async Task<IEnumerable<RestauranteModel>> ListarRestaurantesPorTipo(string tipo)
        {

            _dominioRestauranteService.ValidarTipoRestaurante(tipo);
            var restaurantes = await _restauranteService.ListarRestaurantesPorTipo(tipo);
            _dominioRestauranteService.ValidarRestornoListaRestaurantes(restaurantes);         

            return restaurantes.ToRestauranteListModel();
        }

        public async Task<IEnumerable<RestauranteModel>> ListarRestaurantesPorClassificacao()
        {
            var restaurantes = await _restauranteService.ListarRestaurantesPorClassificacaoConsiderandoCache();
            _dominioRestauranteService.ValidarRestornoListaRestaurantes(restaurantes);

            return restaurantes.ToRestauranteListModel();
        }
    }
}
