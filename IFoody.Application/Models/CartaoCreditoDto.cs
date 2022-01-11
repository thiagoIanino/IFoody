using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models
{
    public class CartaoCreditoDto
    {
        public Guid IdCartao { get; set; }
        public Guid IdCliente { get; set; }
        public string NumeroMascarado { get; set; }
    }
}
