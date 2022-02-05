using IFoody.Domain.Dtos;
using IFoody.Domain.Entities;
using IFoody.Domain.Entities.Restaurantes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Domain.Repositories.Restaurantes
{
    public interface IRestauranteRepository
    {
        Task GravarRestaurante(Restaurante restaurante);
        Task<int> AutenticarRestaurante(string email, string senha);
        Task<IEnumerable<Restaurante>> ListarRestaurantesPorTipo(string tipo);
        Task<IEnumerable<Restaurante>> ListarRestaurantesPorClassificacao();
        Task<IEnumerable<Restaurante>> ListarRestaurantesPorClassificacaoConsiderandoCache();
    }
}
