using IFoody.Domain.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
 
namespace IFoody.Infrastructure.Repositories
{
    public class RedisRepository : IRedisRepository
    {
        private readonly Lazy<IDatabase> _cache;
         public RedisRepository(Lazy<ConnectionMultiplexer> connectionMultiplexer)
         {
            _cache = new Lazy<IDatabase>(() => connectionMultiplexer.Value.GetDatabase(1));
         }
        
        public async Task SalvarObjetoAssincrono<T>(T valor, string chave, TimeSpan? tempoExpiracao)
        {
            await _cache.Value.StringSetAsync(chave, JsonSerializer.Serialize(valor), tempoExpiracao);
        }

        public async Task<T> ObterObjetoAssincrono<T>(string chave)
        {
            var value = await Obter(chave);
            return value == default ? default : JsonSerializer.Deserialize<T>(value);
        }

        public async Task<RedisValue> Obter(string chave)
        {
            return await _cache.Value.StringGetAsync(chave);
        }

        public async Task<T> ObterOuSalvarAsync<T>(string chave, Func<Task<T>> func, TimeSpan tempoExpiracao)
        {
            var retorno = await ObterObjetoAssincrono<T>(chave); 

            if(object.Equals(retorno, default(T)))
            {
                retorno = await func();
                await SalvarObjetoAssincrono<T>(retorno, chave, tempoExpiracao);
            }
            return retorno;
        }
    }
}
