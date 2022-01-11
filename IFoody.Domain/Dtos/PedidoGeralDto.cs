using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class PedidoGeralDto
    {
        public List<Pedido> Pedidos { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
