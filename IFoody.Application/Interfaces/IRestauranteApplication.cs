using IFoody.Application.Models;
using IFoody.Application.Models.Restaurantes;
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
        Task AutenticarRestaurante(string email,string senha);
        Task AvaliarRestaurante(AvaliacaoInput avaliacaoInput);
        Task<IEnumerable<RestauranteModel>> ListarRestaurantesPorTipo(string tipo);
        Task<IEnumerable<RestauranteModel>> ListarRestaurantesPorClassificacao();
    }
}
