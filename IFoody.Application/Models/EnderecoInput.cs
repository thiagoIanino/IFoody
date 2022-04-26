using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models
{
    public class EnderecoInput
    {
        public string Rua { set; get; }
        public double Numero { set; get; }
        public double? Apto { set; get; }
        public string Bairro { set; get; }
        public string Cidade { set; get; }
        public string Estado { set; get; }
    }
}
