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
    public class EnderecoRepository : BaseRepository<EnderecoCliente>, IEnderecoRepository
    {

        private const string INSERIR_ENDERECO_CLIENTE_EXECUTE = "insert into EnderecoCliente(id,idCliente,primeiraLinhaEnd,segundaLinhaEnd) values (@id,@idCliente,@linha1End,@linha2End)";
        private const string BUSCAR_ENDERECOS_CLIENTE_QUERY = "select id , idCliente ,primeiraLinhaEnd ,segundaLinhaEnd from EnderecoCliente where idCliente = @idCliente";
        private const string OBTER_ENDERECO_CLIENTE_QUERY = "select id as id, idCliente as idCliente,primeiraLinhaEnd as primeiraLinhaEnd,segundaLinhaEnd as segundaLinhaEnd from EnderecoCliente where id = @id";


        public EnderecoRepository( IRedisRepository redisService) : base(redisService)
        {

        }

        public async Task CadastrarEndereco (EnderecoCliente endereco)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@id", endereco.Id, DbType.Guid);
            parms.Add("@idCliente", endereco.IdCliente, DbType.Guid);
            parms.Add("@linha1End", endereco.PrimeiraLinhaEnd, DbType.AnsiString);
            parms.Add("@linha2End", endereco.SegundaLinhaEnd, DbType.AnsiString);

            await ExecutarAsync(INSERIR_ENDERECO_CLIENTE_EXECUTE, parms);
        }

        public async Task<IEnumerable<EnderecoCliente>> ObterEnderecos(Guid idCliente)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@idCliente", idCliente, DbType.Guid);

           return await ListarAsync(BUSCAR_ENDERECOS_CLIENTE_QUERY, parms);
        }

        public async Task<EnderecoCliente> ObterEndereco(Guid idEndereco)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@id", idEndereco, DbType.Guid);

            return await ObterAsync(OBTER_ENDERECO_CLIENTE_QUERY, parms);
        }


    }
}
