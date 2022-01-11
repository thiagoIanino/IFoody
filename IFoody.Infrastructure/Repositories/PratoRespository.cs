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
    public class PratoRespository : BaseRepository<Prato>, IPratoRespository
    {
        public PratoRespository(IRedisRepository redisService) : base(redisService) { }

        const string GRAVAR_PRATO_EXECUTE = "insert into Prato(id,nomePrato,descricao,urlImagem,valor, idRestaurante) values(@id,@nomePrato,@descricao,@urlImagem,@valor,@idRestaurante)";
        const string LISTAR_PRATOS_RESTAURANTE = "select p.id as id,p.nomePrato as nomePrato,p.descricao as descricao,p.urlImagem as urlImagem,p.valor as valor,p.idRestaurante as idRestaurante from Prato p join Restaurante r on p.idRestaurante = r.id where r.id = @idRestaurante";
        const string LISTAR_PRATOS_PARA_PEDIDO = "select p.id as id,p.nomePrato as nomePrato,p.descricao as descricao,p.urlImagem as urlImagem,p.valor as valor,p.idRestaurante as idRestaurante from Prato p where id in (";
     
        public async Task GravarPrato(Prato prato)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@id", prato.Id, DbType.Guid);
            parms.Add("@nomePrato", prato.NomePrato, DbType.AnsiString);
            parms.Add("@descricao", prato.Descricao, DbType.AnsiString);
            parms.Add("@urlImagem", prato.UrlImagem, DbType.AnsiString);
            parms.Add("@valor", prato.Valor, DbType.Double);
            parms.Add("@idRestaurante", prato.IdRestaurante, DbType.Guid);

            await ExecutarAsync(GRAVAR_PRATO_EXECUTE, parms);

        }

        public async Task<IEnumerable<Prato>> ListarPratosRestaurante(Guid idRestaurante)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@idRestaurante", idRestaurante, DbType.Guid);

            var pratos = await ListarAsync(LISTAR_PRATOS_RESTAURANTE, parms);
            return pratos;
        }

        public async Task<IEnumerable<Prato>> ListarPratosPedido(List<Guid> IdsPratos)
        {
            DynamicParameters parms = new DynamicParameters();
            int parmsCount = 1;
            string valoresIn = "";

            foreach(Guid idPrato in IdsPratos)
            {
                parms.Add($"@idPrato{parmsCount}", idPrato, DbType.Guid);

                if(parmsCount == IdsPratos.ToArray().Length)
                {
                    valoresIn = string.Concat(valoresIn, $"@idPrato{parmsCount})");
                    
                }
                else
                {
                    valoresIn = string.Concat(valoresIn, $"@idPrato{parmsCount},");
                }
                
               parmsCount++;
            }
            var query = string.Concat(LISTAR_PRATOS_PARA_PEDIDO, valoresIn);

            var pratos = await ListarAsync(query, parms);
            return pratos;
        }
    }
}
