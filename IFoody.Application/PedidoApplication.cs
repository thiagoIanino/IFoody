using IFoody.Application.Interfaces;
using IFoody.Application.Mapping;
using IFoody.Application.Models;
using IFoody.Domain.Dtos;
using IFoody.Domain.Enumeradores;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using IFoody.Domain.Repositories.Restaurantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Application
{
    public class PedidoApplication : IPedidoApplication
    {
        private readonly IPagamentoRepository _pedidoService;
        private readonly IPratoRespository _pratoService;
        private readonly IDominioPedidoService _dominioPedidoService;
        private readonly IWebRepository _webRepository;
        private readonly IContextoRepository _contextoRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IRestauranteRepository _restauranteRepository;
        private readonly IRedisRepository _redisService;

        public PedidoApplication(IPagamentoRepository pedidoService,
            IPratoRespository pratoService,
            IDominioPedidoService dominioPedidoService,
            IWebRepository webRepository,
            IContextoRepository contextoRepository,
            IEnderecoRepository enderecoRepository,
            IRestauranteRepository restauranteRepository,
            IRedisRepository redisService)
        {
            _pratoService = pratoService;
            _pedidoService = pedidoService;
            _dominioPedidoService = dominioPedidoService;
            _webRepository = webRepository;
            _contextoRepository = contextoRepository;
            _enderecoRepository = enderecoRepository;
            _restauranteRepository = restauranteRepository;
            _redisService = redisService;
        }

        public async Task CadastrarPedido(PedidoInput pedidoInput)
        {
            var idCliente = _contextoRepository.ObterIdUsuarioAutenticado();

            var idsPratos = pedidoInput.Pratos.Select(p => p.Id).ToList();
            var pratos =  await _pratoService.ListarPratosPedido(idsPratos);

            var idsRestaurantes = pratos.Select(x => x.IdRestaurante).Distinct().ToList();
            var dadosBasicosRestaurantes = _restauranteRepository.ListarDadosPedidoRestaurantes(idsRestaurantes);
            var endereco = _enderecoRepository.ObterEndereco(pedidoInput.IdEndereco);

            await Task.WhenAll(dadosBasicosRestaurantes, endereco);

            var pratosDto = pratos.ToPratoListDto(pedidoInput.Pratos);

            var pedido = _dominioPedidoService.ComporPedidos(pratosDto, idCliente, pedidoInput.IdCartao,dadosBasicosRestaurantes.Result, endereco.Result);
            var cobranca = new CobrancaDto
            {
                PedidoGeral = pedido,
                TokenAutenticacao = _contextoRepository.ObterTokenAutenticacaoHeader()
            };
             _pedidoService.EnviarCobrancaFila(cobranca);
        }

        public async Task ConfirmarPagamento(ConfirmacaoPagamentoDto confirmacaoPagamento)
        {
            await _dominioPedidoService.ValidarRespostaPagamento(confirmacaoPagamento.PedidoGeral.IdUsuario,
                confirmacaoPagamento.RespostaPagamento);

            var dadosPedidoCliente = await _dominioPedidoService.MontarESalvarPedidosCliente(confirmacaoPagamento.PedidoGeral.Pedidos, confirmacaoPagamento.PedidoGeral.IdUsuario);
            dadosPedidoCliente.Pedidos = dadosPedidoCliente.Pedidos.OrderBy(x => x.Status).ToList();
            await _dominioPedidoService.EnviarESalvarPedidosParaRestaurantes(
                confirmacaoPagamento.PedidoGeral);

            await _webRepository.EnviarRespostaCliente( confirmacaoPagamento.PedidoGeral.IdUsuario, dadosPedidoCliente);
        }

        public async Task AtualizarPedido(AtualizacaoPedidoDto atualizacaoPedido)
        {
            var pedidosCliente = await _dominioPedidoService.AtualizarPedidoClienteCache(atualizacaoPedido.StatusNovo, atualizacaoPedido.IdCliente, atualizacaoPedido.IdPedido, atualizacaoPedido.IdRestaurante);
            pedidosCliente.Pedidos = pedidosCliente.Pedidos.OrderBy(x => x.Status).ToList();
            var pedidosRestaurante = await _dominioPedidoService.AtualizarPedidoRestauranteCache(atualizacaoPedido.StatusNovo, atualizacaoPedido.IdRestaurante, atualizacaoPedido.IdPedido);

            await _dominioPedidoService.EnviarPedidosRestaurante(atualizacaoPedido.IdRestaurante, pedidosRestaurante);

            await _webRepository.EnviarRespostaCliente( atualizacaoPedido.IdCliente, pedidosCliente);

        }
    }
}
