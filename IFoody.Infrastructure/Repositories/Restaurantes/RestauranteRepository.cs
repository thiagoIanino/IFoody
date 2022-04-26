using Dapper;
using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Entities.Restaurantes;
using IFoody.Domain.Repositories;
using IFoody.Domain.Repositories.Restaurantes;
using IFoody.Infrastructure.DbMapper;
using IFoody.Infrastructure.DbModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Infrastructure.Repositories.Restaurantes
{
    public class RestauranteRepository : BaseServiceRepository<Restaurante>, IRestauranteRepository
    {

        public RestauranteRepository(IHttpClientFactory httpClientFactory,IRedisRepository redisService) : base(httpClientFactory,redisService) { }


        const string GRAVAR_RESTAURANTE_EXECUTE = "insert into Restaurante(id,nomeRestaurante,nomeDonoRestaurante,cnpj,email,senha,tipo,idStripe,role,tempoMedio,subDescricao,urlLogo) values(@id,@nomeRestaurante,@nomeDonoRestaurante,@cnpj,@email,@senha,@tipo,@idStripe,@role,@tempoMedio,@subDescricao,@urlLogo)";
        const string OBTER_RESTAURANTE_POR_EMAIL_E_SENHA = "select r.id as id,r.nomeRestaurante as nomeRestaurante,r.cnpj as cnpj, r.email as email,r.idStripe as idStripe,r.role as role from Restaurante r where email = @email and senha = @senha";

        const string LISTAR_RESTAURANTES_POR_TIPO_QUERY = @"select
                                                            r.id as Id,
                                                            r.nomeRestaurante as NomeRestaurante,
                                                            r.tipo as Tipo,
                                                            s.status as Status,
                                                            AVG(a.nota) as Nota,
                                                            r.tempoMedio as TempoMedioEntrega,
                                                            r.subDescricao as SubDescricao,
                                                            r.urlLogo as UrlLogo
                                                            from Restaurante r 
                                                            join Avaliacao a 
                                                            on r.id = a.idRestaurante
                                                            join StatusAvaliacaoRestaurante s
                                                            on r.id = s.idRestaurante
                                                            where r.tipo = @tipo
                                                            group by r.id,nomeRestaurante,tipo,status,
                                                            tempoMedio,subDescricao,urlLogo";
        const string LISTAR_RESTAURANTES_NOVOS_QUERY = @"select
                                                            r.id as Id,
                                                            r.nomeRestaurante as NomeRestaurante,
                                                            r.tipo as Tipo,
                                                            s.status as Status,
                                                            AVG(a.nota) as Nota,
                                                            r.tempoMedio as TempoMedioEntrega,
                                                            r.subDescricao as SubDescricao,
                                                            r.urlLogo as UrlLogo
                                                            from Restaurante r 
                                                            join Avaliacao a 
                                                            on r.id = a.idRestaurante
                                                            join StatusAvaliacaoRestaurante s
                                                            on r.id = s.idRestaurante
                                                            where s.status = 0
                                                            group by r.id,nomeRestaurante,tipo,status,
                                                            tempoMedio,subDescricao,urlLogo";

        const string LISTAR_RESTAURANTES_POR_CLASSIFICACAO_QUERY = @"select
                                                            r.id as Id,
                                                            r.nomeRestaurante as NomeRestaurante,
                                                            r.tipo as Tipo,
                                                            s.status as Status,
                                                            AVG(a.nota) as Nota,
                                                            r.tempoMedio as TempoMedioEntrega,
                                                            r.subDescricao as SubDescricao,
                                                            r.urlLogo as UrlLogo
                                                            from Restaurante r 
                                                            join Avaliacao a 
                                                            on r.id = a.idRestaurante
                                                            join StatusAvaliacaoRestaurante s
                                                            on r.id = s.idRestaurante
                                                            group by r.id,nomeRestaurante,tipo,status,
                                                            tempoMedio,subDescricao,urlLogo
                                                            order by nota Desc";

        const string OBTER_RESTAURANTE_POR_ID =             @"select
                                                            r.id as Id,
                                                            r.nomeRestaurante as NomeRestaurante,
                                                            r.tipo as Tipo,
                                                            s.status as Status,
                                                            AVG(a.nota) as Nota,
                                                            r.tempoMedio as TempoMedioEntrega,
                                                            r.subDescricao as SubDescricao,
                                                            r.urlLogo as UrlLogo
                                                            from Restaurante r 
                                                            join Avaliacao a 
                                                            on r.id = a.idRestaurante
                                                            join StatusAvaliacaoRestaurante s
                                                            on r.id = s.idRestaurante
                                                            where r.id = @id
                                                            group by r.id,nomeRestaurante,tipo,status,
                                                            tempoMedio,subDescricao,urlLogo";

        const string LISTAR_DADOS_BASICOS_RESTAURANTES_QUERY = @"select
                                                            r.id as Id,
                                                            r.nomeRestaurante as NomeRestaurante,
                                                            r.tempoMedio as TempoMedioEntrega,
                                                           r.urlLogo as UrlLogo
                                                            from Restaurante r 
                                                            where r.id in (
                                                            ";

        public async Task<Restaurante> ObterClientePorEmailESenha(string email, string senha)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.Add("@email", email, DbType.AnsiString);
            parms.Add("@senha", senha, DbType.AnsiString);

            return await ObterAsync<Restaurante>(OBTER_RESTAURANTE_POR_EMAIL_E_SENHA, parms);
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
            parms.Add("@idStripe", restaurante.IdStripe, DbType.AnsiString);
            parms.Add("@role", restaurante.Role, DbType.AnsiString);
            parms.Add("@tempoMedio", restaurante.TempoMedioEntrega, DbType.Int16);
            parms.Add("@subDescricao", restaurante.SubDescricao, DbType.AnsiString);
            parms.Add("@urlLogo", restaurante.UrlLogo, DbType.AnsiString);

            await ExecutarAsync(GRAVAR_RESTAURANTE_EXECUTE, parms);
        }


        public async Task<IEnumerable<RestauranteDto>> ListarRestaurantesPorTipo(string tipo)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.Add("@tipo", tipo, DbType.AnsiString);

            var restaurantesModel = await ListarAsync<RestauranteDbModel>(LISTAR_RESTAURANTES_POR_TIPO_QUERY, parms);
            return restaurantesModel.ToRestauranteListDto();
         
        }
        public async Task<IEnumerable<RestauranteDto>> ListarRestaurantesNovos()
        {
            DynamicParameters parms = new DynamicParameters();

            var restaurantesModel = await ListarAsync<RestauranteDbModel>(LISTAR_RESTAURANTES_NOVOS_QUERY,null);
            return restaurantesModel.ToRestauranteListDto();

        }

        public async Task<IEnumerable<RestauranteDto>> ListarRestaurantesPorClassificacaoConsiderandoCache()
        {
            var chave = "top_15_classificados";
            var tempoExp = new TimeSpan(01, 00, 00);
            var restaurantesModel =  await ObterOuSalvarAsync(chave,async () => await ListarRestaurantesPorClassificacao(), tempoExp);
            return restaurantesModel.ToRestauranteListDto();
        }
        
        public async Task<List<RestaurantePedidoDto>> ListarDadosPedidoRestaurantes(List<Guid> idsRestaurantes)
        {
            DynamicParameters parms = new DynamicParameters();
            int parmsCount = 1;
            string valoresIn = "";

            foreach (Guid idRestaurante in idsRestaurantes)
            {
                parms.Add($"@idRestaurante{parmsCount}", idRestaurante, DbType.Guid);

                if (parmsCount == idsRestaurantes.ToArray().Length)
                {
                    valoresIn = string.Concat(valoresIn, $"@idRestaurante{parmsCount})");
                }
                else
                {
                    valoresIn = string.Concat(valoresIn, $"@idRestaurante{parmsCount},");
                }

                parmsCount++;
            }
            var query = string.Concat(LISTAR_DADOS_BASICOS_RESTAURANTES_QUERY, valoresIn);

            var restaurantes = await ListarAsync<RestaurantePedidoModel>(query, parms);

            return restaurantes.ToPedidoRestauranteListDto();

        }

        private async Task<IEnumerable<RestauranteDbModel>> ListarRestaurantesPorClassificacao()
        {
            return await ListarAsync<RestauranteDbModel>(LISTAR_RESTAURANTES_POR_CLASSIFICACAO_QUERY, null);

        }
        public async Task<RestauranteDto> ObterRestaurante(Guid id)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@id", id, DbType.Guid);
            var restaurante = await ObterAsync<RestauranteDbModel>(OBTER_RESTAURANTE_POR_ID, parms);
            return restaurante.ToRestauranteDto();
        }
    }
}
