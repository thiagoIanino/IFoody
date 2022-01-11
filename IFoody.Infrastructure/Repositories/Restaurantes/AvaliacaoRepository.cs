using Dapper;
using IFoody.Domain.Entities.Restaurantes;
using IFoody.Domain.Repositories;
using IFoody.Domain.Repositories.Restaurantes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Infrastructure.Repositories.Restaurantes
{
    public class AvaliacaoRepository : BaseRepository<Avaliacao>, IAvaliacaoRepository
    {
        public AvaliacaoRepository(IRedisRepository redisService) : base(redisService) { }

        const string GRAVAR_AVALIACAO_EXECUTE = "insert into Avaliacao (id,nota,descricao,data,idRestaurante, idCliente) values (@id,@nota,@descricao,@data,@idRestaurante,@idCliente)";
        const string GRAVAR_AVALIACAO_REGISTRO_RESTAURANTE_EXECUTE = "insert into Avaliacao (id,nota,descricao,data,idRestaurante, idCliente) values (@id,1,NULL,@data,@idRestaurante,@idCliente)";
        const string ID_ADM_AFOODY = "79e0480a-a50c-4b76-aef9-f1cb29f67a7e";

        public async Task AvaliarRestaurante(Avaliacao avaliacao)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.Add("@id", avaliacao.Id, DbType.Guid);
            parms.Add("@nota", avaliacao.Nota, DbType.Int32);
            parms.Add("@descricao", avaliacao.Descricao, DbType.AnsiString);
            parms.Add("@data", avaliacao.Data, DbType.DateTime);
            parms.Add("@idRestaurante", avaliacao.IdRestaurante, DbType.Guid);
            parms.Add("@idCliente", avaliacao.IdCliente, DbType.Guid);

            await ExecutarAsync(GRAVAR_AVALIACAO_EXECUTE, parms);
        }


        public async Task RegistrarInicioRestauranteAvaliacao(Avaliacao avaliacao)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.Add("@id", avaliacao.Id, DbType.Guid);
            parms.Add("@data", avaliacao.Data, DbType.DateTime);
            parms.Add("@idRestaurante", avaliacao.IdRestaurante, DbType.Guid);
            parms.Add("@idCliente", Guid.Parse(ID_ADM_AFOODY), DbType.Guid);

            await ExecutarAsync(GRAVAR_AVALIACAO_REGISTRO_RESTAURANTE_EXECUTE, parms);
        }
    }
}
