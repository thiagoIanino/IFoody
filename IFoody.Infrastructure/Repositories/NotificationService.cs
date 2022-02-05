using IFoody.Domain.Dtos;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Infrastructure.Repositories
{
    public class NotificationService : Hub
    {
        private readonly IDominioPedidoService _dominioPedidoService;
        private readonly IDictionary<string, UserConnection> _connections;

        public NotificationService(IDictionary<string, UserConnection> connections, IDominioPedidoService dominioPedidoService)
        {
            _connections = connections;
            _dominioPedidoService = dominioPedidoService;
        }


        //public async Task SendMessage(NotifyMessage message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", message);
        //}

        public Task SendNotificationAsync(string notification)
        {
             _connections.TryGetValue("idRestaurante", out UserConnection userConn);
            return notification != null
            ? Clients.All.SendAsync("NotificationReceived", notification)
            : Task.CompletedTask;
        }

        public Task CadastraNotficacaoRestaurante(string notification)
        {
            var teste = new UserConnection { Room = Context.ConnectionId, User = notification };
            _connections[notification] = teste;
            return Task.CompletedTask;
        }


        public async Task SendMessage(string message)
        {
            if(_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                await Clients.Group("cd").SendAsync("ReceiveMessage", "f", message);
            }
        }

        public async Task JoinRoom(string idRestaurante)
        {
           

            await Groups.AddToGroupAsync(Context.ConnectionId, idRestaurante);

            var pedidos = await _dominioPedidoService.ListarPedidosCache(Guid.Parse(idRestaurante));

            await _dominioPedidoService.EnviarPedidosRestaurante(Guid.Parse(idRestaurante), pedidos);
            //_connections[Context.ConnectionId] = userConnection;

            //await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", "Heyy", $"{userConnection.User} has joined {userConnection.Room}");
        }

    }
}
