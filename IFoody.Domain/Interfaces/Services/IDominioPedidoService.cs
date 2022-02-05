using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Interfaces.Services
{
    public interface IDominioPedidoService
    {
        PedidoGeralDto ComporPedidos(List<PratoDto> pratos, Guid idUsuario, Guid idCartao, string cvv);
        Task ValidarRespostaPagamento(Guid idCliente, RespostaPagamentoDto respostaPagamento);
        Task EnviarEAtualizarPedidosParaRestaurantes(PedidoGeralDto pedidoGeral);
        Task<List<Pedido>> AtualizarPedidoCache(StatusPedido statusNovo, Guid idRestaurante, Guid idPedido);
        Task EnviarPedidosRestaurante(Guid idRestaurante, List<Pedido> pedidos);
        Task<List<Pedido>> ListarPedidosCache(Guid idRestaurante);
    }
}
