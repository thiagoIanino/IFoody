using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Repositories;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static IFoody.Domain.Constantes.Contantes;

namespace IFoody.Infrastructure.Repositories
{
    public class PagamentoRepository  : BaseServiceRepository<Pedido>, IPagamentoRepository
    {
        public PagamentoRepository(IHttpClientFactory httpClientFactory,IRedisRepository redisService) :base(httpClientFactory,redisService)
        {

        }

        public void EnviarCobrancaFila(PedidoGeralDto pedido)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Cria Fila
                channel.QueueDeclare(queue: "cobrancaPedidosFila",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(pedido);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "cobrancaPedidosFila",
                                     basicProperties: null,
                                     body: body);
            }

        }

        public async Task<string> CadastrarUsuarioStripe(UsuarioStripeDto restaurante)
        {
            var nomeClient = Pagamento.NOME_API_PAGAMENTO;
            var endpoint = "http://localhost:41354/api/usuarios";

            var resposta = await Post<UsuarioStripeOutput, UsuarioStripeDto>(nomeClient, endpoint, restaurante, null);

            return resposta.IdUsuario;
        }

        public async Task<string> CadastrarCartaoStripe(CartaoStripeDto cartao)
        {
            var nomeClient = Pagamento.NOME_API_PAGAMENTO;
            var endpoint = "http://localhost:41354/api/usuarios/cartao";

            var resposta = await Post<CartaoStripeOutput, CartaoStripeDto>(nomeClient, endpoint, cartao, null);

            return resposta.IdCartaoStripe;
        }

    }
}
