using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores;
using IFoody.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Infrastructure.Repositories
{
    public class WebRepository  : IWebRepository
    {
        private readonly IHubContext<NotificationService> _hubContext;

        public WebRepository(IHubContext<NotificationService> hubContext) =>
            _hubContext = hubContext;

        public Task JoinRoom(UserConnection userConnection)
        {
            throw new NotImplementedException();
        }

        public Task SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        public Task SendNotificationAsync(string notification)
        {
          
            _hubContext.Clients.Group("idRestaurante").SendAsync("ReceiveMessage", notification);
            //_hubContext.Clients.Client(notification).SendAsync("ReceiveMessage2", "f", "eu sou guei");
                return Task.CompletedTask;
        }

        public Task EnviarRespostaCliente( Guid idUsuario, RespostaCLienteDto respostaCliente)
        {

            _hubContext.Clients.Group(idUsuario.ToString()).SendAsync("ReceiveMessage", respostaCliente);
            //_hubContext.Clients.Client(notification).SendAsync("ReceiveMessage2", "f", "eu sou");
            return Task.CompletedTask;
        }

        public Task EnviarRespostaPagamentoCliente(Guid idUsuario, RespostaPagamentoDto respostaPagamento)
        {

            _hubContext.Clients.Group(idUsuario.ToString()).SendAsync("ReceiveMessage", respostaPagamento);
            //_hubContext.Clients.Client(notification).SendAsync("ReceiveMessage2", "f", "eu sou");
            return Task.CompletedTask;
        }

        public Task EnviarPedidosRestaurante(List<Pedido> pedidos)
        {

            _hubContext.Clients.Group(pedidos.FirstOrDefault().IdRestaurante.ToString()).SendAsync("ReceiveMessage", pedidos);
            //_hubContext.Clients.Client(notification).SendAsync("ReceiveMessage2", "f", "eu sou");
            return Task.CompletedTask;
        }

    }
}
