using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class RespostaCLienteDto
    {
        public List<PedidoClienteDto> Pedidos { get; set; }
        public StatusPedido StatusPedido {get ; set;}
    }
}
