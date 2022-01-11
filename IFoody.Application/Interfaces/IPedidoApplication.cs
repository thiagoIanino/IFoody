using IFoody.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Application.Interfaces
{
    public interface IPedidoApplication
    {
        Task CadastrarPedido(PedidoInput pedido);
    }
}
