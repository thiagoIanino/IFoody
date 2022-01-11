using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFoody.Domain.Services
{
    public class DominioPedidoService : IDominioPedidoService
    {
        public DominioPedidoService()
        {

        }

        public PedidoGeralDto ComporPedidos(List<PratoDto> pratos, Guid idUsuario)
        {
            var pedidos = new List<Pedido>();

            foreach(var prato in pratos)
            {
                var pedidoRestauranteMapeado = pedidos.FirstOrDefault(pedido => pedido.IdRestaurante == prato.IdRestaurante);

                if(pedidoRestauranteMapeado == null)
                {
                    var pedido = new Pedido(prato);
                    pedidos.Add(pedido);
                }
                else
                {
                    pedidoRestauranteMapeado.Itens.Add(prato);
                    pedidoRestauranteMapeado.ValorTotal += prato.ValorTotal;
                }
            }

            var pedidoGeral = new PedidoGeralDto
            {
                Pedidos = pedidos,
                IdUsuario = idUsuario
            };

            return pedidoGeral;
        }
    }
}
