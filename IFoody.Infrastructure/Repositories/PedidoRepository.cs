using IFoody.Domain.Entities;
using IFoody.Domain.Repositories;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IFoody.Infrastructure.Repositories
{
    public class PedidoRepository  : BaseRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(IRedisRepository redisService) :base(redisService)
        {

        }

        public void EnviarCobrancaFila()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Cria Fila
                channel.QueueDeclare(queue: "PrimeiraFila2",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                var obj = new {Texto = "Ahhhh" };
                string message = JsonSerializer.Serialize(obj);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "PrimeiraFila2",
                                     basicProperties: null,
                                     body: body);
            }

        }


    }
}
