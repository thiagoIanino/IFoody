using IFoody.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.Domain.Interfaces.Services
{
    public interface IDominioPedidoService
    {
        PedidoGeralDto ComporPedidos(List<PratoDto> pratos, Guid idUsuario);
    }
}
