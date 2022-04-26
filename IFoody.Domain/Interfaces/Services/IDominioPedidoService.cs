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
        PedidoGeralDto ComporPedidos(List<PratoDto> pratos, Guid idUsuario, Guid idCartao, List<RestaurantePedidoDto> restaurantes, EnderecoCliente enderecoCliente);
        Task ValidarRespostaPagamento(Guid idCliente, RespostaPagamentoDto respostaPagamento);
        Task EnviarESalvarPedidosParaRestaurantes(PedidoGeralDto pedidoGeral);
        Task<PedidoProcessado> AtualizarPedidoClienteCache(StatusPedido statusNovo, Guid idUsuario, Guid idPedido, Guid idRestaurante);
        Task<List<Pedido>> AtualizarPedidoRestauranteCache(StatusPedido statusNovo, Guid idUsuario, Guid idPedido);
        Task EnviarPedidosRestaurante(Guid idRestaurante, List<Pedido> pedidos);
        Task<List<T>> ListarPedidosCache<T>(Guid idRestaurante);
        Task<PedidoProcessado> MontarESalvarPedidosCliente(List<Pedido> pedidos, Guid idCliente);
        Task EnviarPedidosCliente(Guid idCliente, PedidoProcessado pedidos);
        Task<PedidoProcessado> AtualizarAvaliacoesPendentesCache( Guid idUsuario, Guid idRestaurante);
        Task<T> ObterPedidoCache<T>(Guid idUsuario);
    }
}
