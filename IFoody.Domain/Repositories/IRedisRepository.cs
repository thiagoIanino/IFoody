using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Repositories
{
    public interface IRedisRepository
    {
        Task SalvarObjetoAssincrono<T>(T valor, string chave, TimeSpan tempoExpiracao);
        Task<T> ObterOuSalvarAsync<T>(string chave, Func<Task<T>> func, TimeSpan tempoExpiracao);
    }
}
