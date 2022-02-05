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
    public class CartaoCreditoRepository : BaseRepository<CartaoCredito>, ICartaoCreditoRepository
    {
        const string GRAVAR_CARTAO_EXECUTE = "insert into Cartao (idCliente,idCartao,numero,numeroMascarado,validade,nomeTitular,cpf,idStripe) values (@idCliente,@idCartao,@numero,@numeroMascarado,@validade,@nomeTitular,@cpf,@idStripe)";
        const string LISTAR_CARTOES_CLIENTE_QUERY = "select idCliente,idCartao,numeroMascarado from Cartao where idCliente = @idCliente";

        public CartaoCreditoRepository(IRedisRepository redisService) : base(redisService) { }

        public async Task CadastrarCartao(CartaoCredito cartao)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.Add("@idCliente", cartao.IdCliente, DbType.Guid);
            parms.Add("@idCartao", cartao.IdCartao, DbType.Guid);
            parms.Add("@numero", cartao.Numero, DbType.AnsiString);
            parms.Add("@numeroMascarado", cartao.NumeroMascarado, DbType.AnsiString);
            parms.Add("@validade", cartao.Validade, DbType.DateTime);
            parms.Add("@nomeTitular", cartao.NomeTitular, DbType.AnsiString);
            parms.Add("@cpf", cartao.Cpf, DbType.AnsiString);
            parms.Add("@idStripe", cartao.IdStripe, DbType.AnsiString);

            await ExecutarAsync(GRAVAR_CARTAO_EXECUTE, parms);
        }

        public async Task<IEnumerable<CartaoCredito>> ListarCartoesCliente(Guid idCliente)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@idCliente", idCliente, DbType.Guid);

            return await ListarAsync(LISTAR_CARTOES_CLIENTE_QUERY, parms);
        }
    }
}
