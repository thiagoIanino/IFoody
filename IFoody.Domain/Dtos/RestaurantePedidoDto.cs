using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class RestaurantePedidoDto
    {
        public Guid Id { get; set; }
        public string NomeRestaurante { get; set; }
        public int TempoMedioEntrega { get; set; }
        public string UrlLogo { get; set; }
    }
}
