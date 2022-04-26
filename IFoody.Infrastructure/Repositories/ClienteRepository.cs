using Dapper;
using IFoody.Domain.Entities;
using IFoody.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Infrastructure.Repositories
{
   public class ClienteRepository : BaseServiceRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(IHttpClientFactory httpClientFactory, IRedisRepository redisService) : base(httpClientFactory,redisService) { }

        const string BUSCAR_CLIENTE_POR_EMAIL_E_SENHA_QUERY = "Select c.id as id,c.nome as nome,c.email as email,c.idStripe as idStripe,c.role as role from Cliente c where email = @email and senha = @senha";
        const string BUSCAR_CLIENTE_QUERY = "Select id as id,nome as nome,email as email, idStripe as idStripe, role as role from Cliente where id = @id";
        const string GRAVAR_CLIENTE_EXECUTE = "Insert into Cliente(id,nome,email,senha,idStripe) values (@id,@nome,@email,@senha,@idStripe)";
        public async Task<Cliente> ObterClientePorEmailESenha(string email, string senha)
        {
            DynamicParameters parametros = new DynamicParameters();
            // ansiString == varchar / ansiStringfixedLength == char
            parametros.Add("@email", email, DbType.AnsiString);
            parametros.Add("@senha", senha, DbType.AnsiString);

           return await ObterAsync<Cliente>(BUSCAR_CLIENTE_POR_EMAIL_E_SENHA_QUERY, parametros);
        }

        public async Task GravarCliente(Cliente cliente)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@id", cliente.Id, DbType.Guid);
            parametros.Add("@nome", cliente.Nome, DbType.AnsiString);
            parametros.Add("@email", cliente.Email, DbType.AnsiString);
            parametros.Add("@senha", cliente.Senha, DbType.AnsiString);
            parametros.Add("@idStripe", cliente.IdStripe, DbType.AnsiString);

            await  ExecutarAsync(GRAVAR_CLIENTE_EXECUTE, parametros);
        }

        public async Task<Cliente> BuscarCliente(Guid id)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Guid);


            return await ObterAsync<Cliente>(BUSCAR_CLIENTE_QUERY, parametros);
        }
    }
}
