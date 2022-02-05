using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models
{
    public class PedidoInput
    {
        public Guid IdCliente { get; set; }
        public Guid IdCartao { get; set; }
        public string Cvv { get; set; }
        public List<PratoPedidoInput> Pratos { get; set; }
    }
}
