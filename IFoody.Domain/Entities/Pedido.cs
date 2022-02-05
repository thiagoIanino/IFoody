using IFoody.Domain.Dtos;
using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Entities
{
    public class Pedido
    {
        public Pedido()
        {

        }
        public Pedido(PratoDto prato, Guid idCliente)
        {
            IdPedido = Guid.NewGuid();
            IdRestaurante = prato.IdRestaurante;
            IdCliente = idCliente;
            Itens = new List<PratoDto> {
                prato
            };
            ValorTotal = prato.ValorTotal;
            Status = StatusPedido.EmProcessamento;
        }

        public Guid IdPedido {get; set; }
        public Guid IdRestaurante { get; set; }
        public Guid IdCliente { get; set; }
        public List<PratoDto> Itens { get; set; }
        public double? ValorTotal { get; set; }
        public StatusPedido Status { get; set; }
    }
}
