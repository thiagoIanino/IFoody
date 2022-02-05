using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Enumeradores;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Services
{
    public class DominioPedidoService : IDominioPedidoService
    {
        private readonly IRedisRepository _redisService; 
        private readonly IWebRepository _webService; 
        public DominioPedidoService(IRedisRepository redisService, IWebRepository webService)
        {
            _redisService = redisService;
            _webService = webService;
        }

        public async Task ValidarRespostaPagamento(Guid idCliente, RespostaPagamentoDto respostaPagamento)
        {
            if (respostaPagamento.Aprovado)
            {
                return ;
            }
            var respostaCliente = new RespostaCLienteDto {
                IdPedido = respostaPagamento.IdPedido,
                StatusPedido = StatusPedido.NaoAprovado 
            };
            await _webService.EnviarRespostaCliente(idCliente, respostaCliente);
            throw new Exception("O pagamento foi recusado");
        }

        public async Task<List<Pedido>> AtualizarPedidoCache(StatusPedido statusNovo, Guid idRestaurante, Guid idPedido)
        {
            var pedidos = new List<Pedido>();

            var pedidosCache = await _redisService.ObterObjetoAssincrono<List<Pedido>>(idRestaurante.ToString());

            foreach (var pedidoCache in pedidosCache)
            {
                pedidos.Add(pedidoCache);
            }

            var pedido = pedidos.FirstOrDefault(x => x.IdPedido == idPedido);
            pedido.Status = statusNovo;

            await _redisService.SalvarObjetoAssincrono<List<Pedido>>(pedidos, idRestaurante.ToString(), null);

            return pedidos;
        }

        public async Task EnviarEAtualizarPedidosParaRestaurantes(PedidoGeralDto pedidoGeral)
        {
            foreach(var pedido in pedidoGeral.Pedidos)
            {
                pedido.Status = StatusPedido.Aberto;
                var pedidosCache = await AdicionarPedidoCache(pedido);

                await _webService.EnviarPedidosRestaurante(pedidosCache);
            }

        }

        public PedidoGeralDto ComporPedidos(List<PratoDto> pratos, Guid idCliente, Guid idCartao, string cvv)
        {
            var pedidos = new List<Pedido>();

            foreach(var prato in pratos)
            {
                var pedidoRestauranteMapeado = pedidos.FirstOrDefault(pedido => pedido.IdRestaurante == prato.IdRestaurante);

                if(pedidoRestauranteMapeado == null)
                {
                    var pedido = new Pedido(prato, idCliente);
                    pedidos.Add(pedido);
                }
                else
                {
                    pedidoRestauranteMapeado.Itens.Add(prato);
                    pedidoRestauranteMapeado.ValorTotal += prato.ValorTotal;
                }
            }

            var pedidoGeral = new PedidoGeralDto
            {
                IdPedidoGeral = Guid.NewGuid(),
                Pedidos = pedidos,
                IdUsuario = idCliente,
                IdCartao = idCartao,
                Cvv = cvv
            };

            return pedidoGeral;
        }

        private async Task<List<Pedido>> AdicionarPedidoCache(Pedido pedido)
        {
            var pedidos = new List<Pedido>();
            pedidos.Add(pedido);

            var pedidosCache = await _redisService.ObterObjetoAssincrono<List<Pedido>>(pedido.IdRestaurante.ToString());

            if(pedidosCache != null)
            {
                foreach (var pedidoCache in pedidosCache)
                {
                    pedidos.Add(pedidoCache);
                }
            }

            await _redisService.SalvarObjetoAssincrono<List<Pedido>>(pedidos, pedido.IdRestaurante.ToString(),null);

            return pedidos;
        }
        public async Task EnviarPedidosRestaurante(Guid idRestaurante, List<Pedido> pedidos)
        {
            if (pedidos.Any())
            {
                await _webService.EnviarPedidosRestaurante(pedidos);
            }
        }

        public async Task<List<Pedido>> ListarPedidosCache(Guid idRestaurante)
        {
            var pedidos = new List<Pedido>();

            var pedidosCache = await _redisService.ObterObjetoAssincrono<List<Pedido>>(idRestaurante.ToString());

            if (pedidosCache != null)
            {
                foreach (var pedidoCache in pedidosCache)
                {
                    pedidos.Add(pedidoCache);
                }
            }

            return pedidos;
        }
    }
}
