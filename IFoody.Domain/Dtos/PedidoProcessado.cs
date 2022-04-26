using IFoody.Domain.Entities.Restaurantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Dtos
{
    public class PedidoProcessado
    {
        public List<PedidoClienteDto> Pedidos { get; set; }
        public List<Avaliacao> AvaliacoesPendentes { get; set; }
    }
}
