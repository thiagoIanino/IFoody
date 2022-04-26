using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Infrastructure.Repositories
{
    [AllowAnonymous]
    public class NotificationService : Hub
    {
        private readonly IDominioPedidoService _dominioPedidoService;

        public NotificationService( IDominioPedidoService dominioPedidoService)
        {
            _dominioPedidoService = dominioPedidoService;
        }


        //public async Task SendMessage(NotifyMessage message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", message);
        //}

        public Task SendNotificationAsync(string notification)
        {
            return notification != null
            ? Clients.All.SendAsync("NotificationReceived", notification)
            : Task.CompletedTask;
        }

        public Task CadastraNotficacaoRestaurante(string notification)
        {
            var teste = new UserConnection { Room = Context.ConnectionId, User = notification };
            return Task.CompletedTask;
        }


        public async Task<string> SendMessage(string message)
        {
            return "Foi";
        }

        public async Task JoinRestauranteRoom(string idRestaurante)
        {
           

            await Groups.AddToGroupAsync(Context.ConnectionId, idRestaurante);

            var pedidos = await _dominioPedidoService.ListarPedidosCache<Pedido>(Guid.Parse(idRestaurante));

            await _dominioPedidoService.EnviarPedidosRestaurante(Guid.Parse(idRestaurante), pedidos);
            //_connections[Context.ConnectionId] = userConnection;

            //await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", "Heyy", $"{userConnection.User} has joined {userConnection.Room}");
        }

        public async Task JoinClienteRoom(string idCliente)
        {


            await Groups.AddToGroupAsync(Context.ConnectionId, idCliente);

            var pedidos = await _dominioPedidoService.ObterPedidoCache<PedidoProcessado>(Guid.Parse(idCliente));
            if(pedidos != null)
            pedidos.Pedidos = pedidos?.Pedidos.OrderBy(x => x.Status).ToList();

            //var pedidos = new PedidoProcessado();
            await _dominioPedidoService.EnviarPedidosCliente(Guid.Parse(idCliente), pedidos);
            //_connections[Context.ConnectionId] = userConnection;

            //await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", "Heyy", $"{userConnection.User} has joined {userConnection.Room}");
        }

    }
}
