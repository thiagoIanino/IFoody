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
    public class StatusAvaliacaoRepository : BaseRepository<StatusAvaliacao>, IStatusAvaliacaoRepository
    {
        public StatusAvaliacaoRepository(IRedisRepository redisService) : base(redisService) { }

        const string GRAVAR_STATUS_AVALIACAO_RESTAURANTE_EXECUTE = "insert into StatusAvaliacaoRestaurante (idRestaurante,status) values (@idRestaurante,@status)";
        const string ATUALIZAR_STATUS_AVALIACAO_RESTAURANTE_EXECUTE = "update StatusAvaliacaoRestaurante  set idRestaurante = @idRestaurante,status = @status where idRestaurante = @idRestaurante";

        public async Task GravarStatusAvaliacaoRestaurante(StatusAvaliacao statusAvaliacao)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.Add("@idRestaurante", statusAvaliacao.IdRestaurante, DbType.Guid);
            parms.Add("@status", (int)statusAvaliacao.Status, DbType.Int32);

            await ExecutarAsync(GRAVAR_STATUS_AVALIACAO_RESTAURANTE_EXECUTE, parms);
        }

        public async Task AtualizarStatusAvaliacaoRestaurante(StatusAvaliacao statusAvaliacao)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.Add("@id", statusAvaliacao.IdRestaurante, DbType.Guid);
            parms.Add("@status", statusAvaliacao.Status, DbType.Int32);

            await ExecutarAsync(ATUALIZAR_STATUS_AVALIACAO_RESTAURANTE_EXECUTE, parms);
        }


    }
}
