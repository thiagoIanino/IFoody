using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models.Restaurantes
{
    public class AvaliacaoInput
    {
        public int Nota { get; set; }
        public string Descricao { get; set; }
        public Guid IdRestaurante { get; set; }
        public Guid IdCliente { get; set; }
    }
}
