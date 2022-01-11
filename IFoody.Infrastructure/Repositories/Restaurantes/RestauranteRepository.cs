using Dapper;
using IFoody.Domain.Entities;
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
    public class RestauranteRepository : BaseRepository<Restaurante>, IRestauranteRepository
    {

        public RestauranteRepository(IRedisRepository redisService) : base(redisService) { }


        const string GRAVAR_RESTAURANTE_EXECUTE = "insert into Restaurante(id,nomeRestaurante,nomeDonoRestaurante,cnpj,email,senha,tipo) values(@id,@nomeRestaurante,@nomeDonoRestaurante,@cnpj,@email,@senha,@tipo)";
        const string AUTENTICAR_RESTAURANTE_QUERY = "select Count(id) from Restaurante where email = @email and senha = @senha";

        const string LISTAR_RESTAURANTES_POR_TIPO_QUERY = @"select
                                                            r.id as id,
                                                            r.nomeRestaurante as nomeRestaurante,
                                                            r.tipo as tipo,
                                                            s.status as status,
                                                            AVG(a.nota) as nota
                                                            from Restaurante r 
                                                            join Avaliacao a 
                                                            on r.id = a.idRestaurante
                                                            join StatusAvaliacaoRestaurante s
                                                            on r.id = s.idRestaurante
                                                            where r.tipo = @tipo 
                                                            group by r.id,nomeRestaurante,tipo,status
                                                            order by nota Desc";

        const string LISTAR_RESTAURANTES_POR_CLASSIFICACAO_QUERY = @"select
                                                            r.id as id,
                                                            r.nomeRestaurante as nomeRestaurante,
                                                            r.tipo as tipo,
                                                            s.status as status,
                                                            AVG(a.nota) as nota
                                                            from Restaurante r 
                                                            join Avaliacao a 
                                                            on r.id = a.idRestaurante
                                                            join StatusAvaliacaoRestaurante s
                                                            on r.id = s.idRestaurante
                                                            group by r.id,nomeRestaurante,tipo,status
                                                            order by nota Desc";

        public async Task<int> AutenticarRestaurante(string email, string senha)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.Add("@email", email, DbType.AnsiString);
            parms.Add("@senha", senha, DbType.AnsiString);

            return await ObterAsync<int>(AUTENTICAR_RESTAURANTE_QUERY, parms);
        }

        public async Task GravarRestaurante(Restaurante restaurante)
        {
            var guid = Guid.NewGuid();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("@id", restaurante.Id, DbType.Guid);
            parms.Add("@nomeRestaurante", restaurante.NomeRestaurante, DbType.AnsiString);
            parms.Add("@nomeDonoRestaurante", restaurante.NomeDonoRestaurante, DbType.AnsiString);
            parms.Add("@cnpj", restaurante.CNPJ, DbType.AnsiStringFixedLength);
            parms.Add("@email", restaurante.Email, DbType.AnsiString);
            parms.Add("@senha", restaurante.Senha, DbType.AnsiString);
            parms.Add("@tipo", restaurante.Tipo, DbType.AnsiString);

            await ExecutarAsync(GRAVAR_RESTAURANTE_EXECUTE, parms);
        }


        public async Task<IEnumerable<Restaurante>> ListarRestaurantesPorTipo(string tipo)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.Add("@tipo", tipo, DbType.AnsiString);

            return await ListarAsync(LISTAR_RESTAURANTES_POR_TIPO_QUERY, parms);
         
        }

        public async Task<IEnumerable<Restaurante>> ListarRestaurantesPorClassificacaoConsiderandoCache()
        {
            var chave = "top_15_classificados";
            var tempoExp = new TimeSpan(01, 00, 00);
            return await ObterOuSalvarAsync(chave,async () => await ListarRestaurantesPorClassificacao(), tempoExp);
        }

        public async Task<IEnumerable<Restaurante>> ListarRestaurantesPorClassificacao()
        {
            return await ListarAsync(LISTAR_RESTAURANTES_POR_CLASSIFICACAO_QUERY, null);

        }
    }
}
