using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class PedidoClienteDto
    {
        public Guid IdPedido { get; set; }
        public Guid IdRestaurante { get; set; }
        public string NomeRestaurante { get; set; }
        public string UrlImagemRestaurante { get; set; }
        public DateTime TempoPrevistoEntrega { get; set; }
        public Guid IdCliente { get; set; }
        public StatusPedido Status { get; set; }
    }
}
