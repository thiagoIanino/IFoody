using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Entities.Restaurantes;
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
                return;

            var respostaCliente = new List<PedidoClienteDto> {
                new PedidoClienteDto
                {
                    Status = StatusPedido.NaoAprovado
                }
            };
            var pedido = new PedidoProcessado
            {
                Pedidos = respostaCliente
            };
            await _webService.EnviarRespostaCliente(idCliente, pedido);
            throw new Exception("O pagamento foi recusado");
        }

        private async Task<List<T>> ListarPedidoCache<T>(Guid idUsuario)
        {

            return await _redisService.ObterObjetoAssincrono<List<T>>(idUsuario.ToString());
        }
        public async Task<T> ObterPedidoCache<T>(Guid idUsuario)
        {

            return await _redisService.ObterObjetoAssincrono<T>(idUsuario.ToString());
        }

        public async Task<List<Pedido>> AtualizarPedidoRestauranteCache(StatusPedido statusNovo, Guid idUsuario, Guid idPedido)
        {
            var pedidos = new List<Pedido>();

            var pedidosCache = await ListarPedidoCache<Pedido>(idUsuario);

            foreach (var pedidoCache in pedidosCache)
            {
                pedidos.Add(pedidoCache);
            }

            var pedido = pedidos.FirstOrDefault(x => x.IdPedido == idPedido);
            pedido.Status = statusNovo;

            await _redisService.SalvarObjetoAssincrono(pedidos, idUsuario.ToString(), null);

            return pedidos;
        }

        public async Task<PedidoProcessado> AtualizarPedidoClienteCache(StatusPedido statusNovo, Guid idUsuario, Guid idPedido, Guid idRestaurante)
        {
            var pedidosProcessados = new PedidoProcessado
            {
                Pedidos = new List<PedidoClienteDto>(),
                AvaliacoesPendentes = new List<Avaliacao>()
            };

            if (statusNovo == StatusPedido.Finalizado)
            {
                var avaliacaoPendente = new Avaliacao
                {
                    IdCliente = idUsuario,
                    IdRestaurante = idRestaurante
                };

                pedidosProcessados.AvaliacoesPendentes.Add(avaliacaoPendente);
            }

            var pedidosCache = await ObterPedidoCache<PedidoProcessado>(idUsuario);

            foreach (var pedidoCache in pedidosCache?.Pedidos)
            {
                pedidosProcessados.Pedidos.Add(pedidoCache);
            }

            var pedido = pedidosProcessados.Pedidos.FirstOrDefault(x => x.IdPedido == idPedido);
            pedido.Status = statusNovo;

            await _redisService.SalvarObjetoAssincrono(pedidosProcessados, idUsuario.ToString(), null);

            return pedidosProcessados;
        }

        public async Task<PedidoProcessado> AtualizarAvaliacoesPendentesCache( Guid idUsuario, Guid idRestaurante)
        {
            var pedidos = new PedidoProcessado();

            var pedidosCache = await ObterPedidoCache<PedidoProcessado>(idUsuario);

            pedidos = pedidosCache;

            var AvaliacoesRetorno = pedidos.AvaliacoesPendentes.Where(x => x.IdRestaurante != idRestaurante).ToList();
            pedidos.AvaliacoesPendentes = AvaliacoesRetorno;

            await _redisService.SalvarObjetoAssincrono(pedidos, idUsuario.ToString(), null);

            return pedidos;
        }

        public async Task<PedidoProcessado> MontarESalvarPedidosCliente(List<Pedido> pedidos, Guid idCliente)
        {
            var pedidoProcessado = new PedidoProcessado
            {
                Pedidos = new List<PedidoClienteDto>()
            };

            var pedidosCache = await ObterPedidoCache<PedidoProcessado>(idCliente);
            if (pedidosCache?.Pedidos != null)
                pedidoProcessado.Pedidos = pedidosCache.Pedidos;

            if (pedidosCache?.AvaliacoesPendentes == null)
                pedidoProcessado.AvaliacoesPendentes = new List<Avaliacao>();

            var pedidosCliente = MontarDadosPedidoCliente(pedidos);

            foreach (var pedido in pedidosCliente)
            {
                pedidoProcessado.Pedidos.Add(pedido);
            }

            await _redisService.SalvarObjetoAssincrono(pedidoProcessado, idCliente.ToString(), null);

            return pedidoProcessado;

        }

        private List<PedidoClienteDto> MontarDadosPedidoCliente(List<Pedido> pedidos)
        {
            var pedidosClientes = new List<PedidoClienteDto>();

            foreach (var pedido in pedidos)
            {
                var pedidoCliente = new PedidoClienteDto
                {
                    IdCliente = pedido.IdCliente,
                    IdPedido = pedido.IdPedido,
                    IdRestaurante = pedido.IdRestaurante,
                    NomeRestaurante = pedido.NomeRestaurante,
                    TempoPrevistoEntrega = pedido.TempoPrevistoEntrega,
                    UrlImagemRestaurante = pedido.UrlImagemRestaurante,
                    Status = StatusPedido.Aberto
                };

                pedidosClientes.Add(pedidoCliente);
            }
            return pedidosClientes;
        }

        public async Task EnviarESalvarPedidosParaRestaurantes(PedidoGeralDto pedidoGeral)
        {
            foreach (var pedido in pedidoGeral.Pedidos)
            {
                pedido.Status = StatusPedido.Aberto;
                var pedidosCache = await AdicionarPedidoCache(pedido);

                await _webService.EnviarPedidosRestaurante(pedidosCache);
            }

        }

        public PedidoGeralDto ComporPedidos(List<PratoDto> pratos, Guid idCliente, Guid idCartao, List<RestaurantePedidoDto> restaurantes, EnderecoCliente enderecoCliente)
        {
            var pedidos = new List<Pedido>();

            foreach (var prato in pratos)
            {
                var pedidoRestauranteMapeado = pedidos.FirstOrDefault(pedido => pedido.IdRestaurante == prato.IdRestaurante);

                if (pedidoRestauranteMapeado == null)
                {
                    var pedido = new Pedido(prato, idCliente, enderecoCliente);
                    pedido.AtribuirDadosBasicosRestaurante(restaurantes);
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
                IdCartao = idCartao
            };

            return pedidoGeral;
        }

        private async Task<List<Pedido>> AdicionarPedidoCache(Pedido pedido)
        {
            var pedidos = new List<Pedido>();
            pedidos.Add(pedido);

            var pedidosCache = await _redisService.ObterObjetoAssincrono<List<Pedido>>(pedido.IdRestaurante.ToString());

            if (pedidosCache != null)
            {
                foreach (var pedidoCache in pedidosCache)
                {
                    pedidos.Add(pedidoCache);
                }
            }

            await _redisService.SalvarObjetoAssincrono<List<Pedido>>(pedidos, pedido.IdRestaurante.ToString(), null);

            return pedidos;
        }
        public async Task EnviarPedidosRestaurante(Guid idRestaurante, List<Pedido> pedidos)
        {
            if (pedidos.Any())
            {
                await _webService.EnviarPedidosRestaurante(pedidos);
            }
        }

        public async Task EnviarPedidosCliente(Guid idCliente, PedidoProcessado pedidos)
        {
            if (pedidos?.Pedidos != null && pedidos.Pedidos.Any())
            {
                await _webService.EnviarRespostaCliente(idCliente, pedidos);
            }
        }

        public async Task<List<T>> ListarPedidosCache<T>(Guid idRestaurante)
        {
            var pedidos = new List<T>();

            var pedidosCache = await _redisService.ObterObjetoAssincrono<List<T>>(idRestaurante.ToString());

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
