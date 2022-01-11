using IFoody.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Entities
{
    public class Pedido
    {
        public Pedido(PratoDto prato)
        {
            IdRestaurante = prato.IdRestaurante;
            Itens = new List<PratoDto> {
                prato
            };
            ValorTotal = prato.ValorTotal;
        }

        public Guid IdRestaurante { get; set; }
        public List<PratoDto> Itens { get; set; }
        public double? ValorTotal { get; set; }
    }
}
