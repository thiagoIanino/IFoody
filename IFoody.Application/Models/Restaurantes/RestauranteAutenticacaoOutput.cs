using IFoody.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models.Restaurantes
{
    public class RestauranteAutenticacaoOutput
    {
        public Guid Id { get; set; }
        public string NomeRestaurante { get; set; }
        public string Tipo { get; set; }
        public ClassificacaoDto Classificacao { get; set; }
        public double? TempoMedioEntrega { get; set; }
        public string SubDescricao { get; set; }
        public string UrlLogo { get; set; }
        public string Token { get; set; }
    }
}
