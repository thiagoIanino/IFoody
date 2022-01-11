using IFoody.Application.Interfaces;
using IFoody.Application.Mapping;
using IFoody.Application.Models;
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
        private readonly IPedidoRepository _pedidoService;
        private readonly IPratoRespository _pratoService;
        private readonly IDominioPedidoService _dominioPedidoService;
        public PedidoApplication(IPedidoRepository pedidoService, IPratoRespository pratoService, IDominioPedidoService dominioPedidoService)
        {
            _pratoService = pratoService;
            _pedidoService = pedidoService;
            _dominioPedidoService = dominioPedidoService;
        }

        public async Task CadastrarPedido(PedidoInput pedidoInput)
        {
            var idsPratos = pedidoInput.Pratos.Select(p => p.Id).ToList();
            var pratos = await _pratoService.ListarPratosPedido(idsPratos);

            var pratosDto = pratos.ToPratoListDto(pedidoInput.Pratos);

            var pedido = _dominioPedidoService.ComporPedidos(pratosDto, pedidoInput.IdCliente);

             _pedidoService.EnviarCobrancaFila();
        }
    }
}
