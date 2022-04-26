using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models
{
    public class CartaoCreditoDto
    {
        public Guid IdCartao { get; set; }
        public string NumeroMascarado { get; set; }
        public string Bandeira { get; set; }
    }
}
