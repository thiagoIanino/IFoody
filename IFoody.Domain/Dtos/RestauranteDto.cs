using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class RestauranteDto
    {
        public Guid Id { get; set; }
        public string NomeRestaurante { get; set; }
        public string Tipo { get; set; }
        public ClassificacaoDto Classificacao { get; set; }
        public double TempoMedioEntrega { get; set; }
        public string SubDescricao { get; set; }
        public string UrlLogo { get; set; }

    }
}
