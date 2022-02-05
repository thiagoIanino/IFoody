using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Repositories
{
    public interface IWebRepository
    {
        Task JoinRoom(UserConnection userConnection);
        Task SendMessage(string message);
        Task SendNotificationAsync(string notification);
        Task EnviarPedidosRestaurante(List<Pedido> pedidos);
        Task EnviarRespostaCliente(Guid idUsuario, RespostaCLienteDto respostaCliente);
        //Task SendMessage(NotifyMessage message);
    }
}
