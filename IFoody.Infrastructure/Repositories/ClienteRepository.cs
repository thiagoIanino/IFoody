using Dapper;
using IFoody.Domain.Entities;
using IFoody.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Infrastructure.Repositories
{
   public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(IRedisRepository redisService) : base(redisService) { }

        const string AUTENTICAR_CLIENTE_QUERY = "Select id as id,nome as nome,email as email from Cliente where email = @email and senha = @senha";
        const string GRAVAR_CLIENTE_EXECUTE = "Insert into Cliente(id,nome,email,senha) values (@id,@nome,@email,@senha)";
        public async Task<Cliente> AutenticarCliente(string email, string senha)
        {
            DynamicParameters parametros = new DynamicParameters();
            // ansiString == varchar / ansiStringfixedLength == char
            parametros.Add("@email", email, DbType.AnsiString);
            parametros.Add("@senha", senha, DbType.AnsiString);

           return await ObterAsync<Cliente>(AUTENTICAR_CLIENTE_QUERY, parametros);
        }

        public async Task GravarCliente(Cliente cliente)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@id", cliente.Id, DbType.Guid);
            parametros.Add("@nome", cliente.Nome, DbType.AnsiString);
            parametros.Add("@email", cliente.Email, DbType.AnsiString);
            parametros.Add("@senha", cliente.Senha, DbType.AnsiString);

            await  ExecutarAsync(GRAVAR_CLIENTE_EXECUTE, parametros);
        }
    }
}
