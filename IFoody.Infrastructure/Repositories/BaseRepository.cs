using Dapper;
using IFoody.Domain.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Infrastructure.Repositories
{
   public class BaseRepository<T>
    {
        private readonly IRedisRepository _redisService;
        public BaseRepository(IRedisRepository redisService)
        {
            _redisService = redisService;
        }

        const string CONNECTION_STRING = "Server=localhost,1433;Database=IFoody;User Id=sa;Password=1q2w3e4r@#$";
        
        protected async Task<IEnumerable<T>> ListarAsync(string sqlCommand, object parms)
        {
            using(var conn = new SqlConnection(CONNECTION_STRING))
            {
                return await conn.QueryAsync<T>(sqlCommand, parms);
            }
        }  
        protected async Task<T> ObterAsync<T>(string sqlCommand, object? parms)
        {
            using (var conn = new SqlConnection(CONNECTION_STRING))
            {
                return (await conn.QueryAsync<T>(sqlCommand,parms)).FirstOrDefault();
            }
        }
        protected async Task<int> ExecutarAsync(string sqlCommand, object? parms)
        {
            using(var conn = new SqlConnection(CONNECTION_STRING))
            {
               return await conn.ExecuteAsync(sqlCommand, parms);
            }
        }
        protected int Executar(string sqlCommand, object? parms)
        {
            using (var conn = new SqlConnection(CONNECTION_STRING))
            {
                return  conn.Execute(sqlCommand, parms);
            }
        }

        protected Task<T> ObterOuSalvarAsync<T>(string chave, Func<Task<T>> func, TimeSpan tempoExpiracao)
        {
            return _redisService.ObterOuSalvarAsync(chave, func, tempoExpiracao);
        }

    }
}
