using IFoody.Application.Interfaces;
using IFoody.Application.Mapping;
using IFoody.Application.Models;
using IFoody.Domain.Dtos;
using IFoody.Domain.Enumeradores;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
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

        public PedidoApplication(IPagamentoRepository pedidoService, IPratoRespository pratoService, IDominioPedidoService dominioPedidoService, IWebRepository webRepository)
        {
            _pratoService = pratoService;
            _pedidoService = pedidoService;
            _dominioPedidoService = dominioPedidoService;
            _webRepository = webRepository;
        }

        public async Task CadastrarPedido(PedidoInput pedidoInput)
        {
            var idsPratos = pedidoInput.Pratos.Select(p => p.Id).ToList();
            var pratos = await _pratoService.ListarPratosPedido(idsPratos);

            var pratosDto = pratos.ToPratoListDto(pedidoInput.Pratos);

            var pedidos = _dominioPedidoService.ComporPedidos(pratosDto, pedidoInput.IdCliente, pedidoInput.IdCartao, pedidoInput.Cvv);

             _pedidoService.EnviarCobrancaFila(pedidos);
        }

        public async Task ConfirmarPagamento(ConfirmacaoPagamentoDto confirmacaoPagamento)
        {
            await _dominioPedidoService.ValidarRespostaPagamento(confirmacaoPagamento.PedidoGeral.IdUsuario,
                confirmacaoPagamento.RespostaPagamento);

            await _dominioPedidoService.EnviarEAtualizarPedidosParaRestaurantes(
                confirmacaoPagamento.PedidoGeral);

            var respostaCliente = new RespostaCLienteDto
            {
                IdPedido = confirmacaoPagamento.RespostaPagamento.IdPedido,
                StatusPedido = StatusPedido.Aberto
            };

            await _webRepository.EnviarRespostaCliente( confirmacaoPagamento.PedidoGeral.IdUsuario, respostaCliente);
        }

        public async Task AtualizarPedido(AtualizacaoPedidoDto atualizacaoPedido)
        {
            var pedidosAtualizados = await _dominioPedidoService.AtualizarPedidoCache(atualizacaoPedido.StatusNovo, atualizacaoPedido.IdRestaurante, atualizacaoPedido.IdPedido);
            await _dominioPedidoService.EnviarPedidosRestaurante(atualizacaoPedido.IdRestaurante, pedidosAtualizados);

            var respostaCliente = new RespostaCLienteDto
            {
                IdPedido = atualizacaoPedido.IdPedido,
                StatusPedido = atualizacaoPedido.StatusNovo
            };

            await _webRepository.EnviarRespostaCliente( atualizacaoPedido.IdCliente, respostaCliente);

        }
    }
}
