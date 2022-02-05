using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Application.Models
{
    public class AtualizacaoPedidoDto
    {
        public Guid IdRestaurante { get; set; }
        public Guid IdPedido { get; set;}
        public Guid IdCliente { get; set; }
        public StatusPedido StatusNovo { get; set; }
    }
}
