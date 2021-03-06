using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models
{
    public class PratoInput
    {
        public string NomePrato { get; set; }
        public string Descricao { get; set; }
        public string UrlImagem { get; set; }
        public double Valor { get; set; }
        public ClassificacaoPrato Classificacao { get; set; }
    }
}
