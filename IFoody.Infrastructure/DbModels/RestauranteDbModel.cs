using IFoody.Domain.Enumeradores.Avaliacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Infrastructure.DbModels
{
    public class RestauranteDbModel
    {

        public Guid Id { get; set; }
        public string NomeRestaurante { get; set; }
        public string Tipo { get; set; }
        public double Nota { get; set; }
        public StatusAvaliacaoRestaurante Status { get; set; }
        public int TempoMedioEntrega { get; set; }
        public string SubDescricao { get; set; }
        public string UrlLogo { get; set; }
    }
}
