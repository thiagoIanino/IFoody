using IFoody.Application.Models;
using IFoody.Application.Models.Restaurantes;
using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Entities.Restaurantes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Application.Interfaces
{
    public interface IRestauranteApplication
    {
        Task CadastrarRestaurante(RestauranteInput restaurante);
        Task<RestauranteAutenticacaoOutput> AutenticarRestaurante(string email,string senha);
        Task AvaliarRestaurante(AvaliacaoInput avaliacaoInput);
        Task<IEnumerable<RestauranteDto>> ListarRestaurantesPorTipo(string tipo);
        Task<IEnumerable<RestauranteDto>> ListarRestaurantesPorClassificacao();
        Task<RestauranteOutput> ObterRestaurante(Guid idRestaurante);
    }
}
