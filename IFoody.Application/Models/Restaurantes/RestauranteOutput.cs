using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models.Restaurantes
{
    public class RestauranteOutput
    {
        public RestauranteDto Restaurante { get; set; }
        public IEnumerable<GrupoPratos> PratosPorClassificacao { get; set; }
    }
}
