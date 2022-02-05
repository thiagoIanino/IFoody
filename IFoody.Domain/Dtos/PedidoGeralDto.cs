using IFoody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class PedidoGeralDto
    {
        public Guid IdPedidoGeral { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdCartao { get; set; }
        public string Cvv { get; set; }
    }
}
