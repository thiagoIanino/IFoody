using System;
using System.Collections.Generic;
using System.Text;


namespace IFoody.Application.Models.Restaurantes
{
    public class RestauranteInput
    {
        public string NomeRestaurante { get; set; }
        public string NomeDonoRestaurante { get; set; }
        public string Tipo { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public double TempoMedioEntrega { get; set; }
        public string SubDescricao { get; set; }
        public string UrlLogo { get; set; }
        
    }
}
