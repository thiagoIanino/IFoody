using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models
{
    public class CartaoCreditoInput
    {
        public string Numero { get; set; }
        public DateTime Validade { get; set; }
        public string NomeTitular { get; set; }
        public string Cpf { get; set; }
        public string Cvv { get; set; }
        public string Bandeira { get; set; }

    }
}
