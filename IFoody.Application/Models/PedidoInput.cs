using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models
{
    public class PedidoInput
    {
        public Guid IdCartao { get; set; }
        public Guid IdEndereco { get; set; }
        public List<PratoPedidoInput> Pratos { get; set; }
    }
}
