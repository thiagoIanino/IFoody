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
        Task<Restaurante> ObterClientePorEmailESenha(string email, string senha);
        Task<IEnumerable<RestauranteDto>> ListarRestaurantesPorTipo(string tipo);
        Task<IEnumerable<RestauranteDto>> ListarRestaurantesPorClassificacaoConsiderandoCache();
        Task<RestauranteDto> ObterRestaurante(Guid id);
        Task<IEnumerable<RestauranteDto>> ListarRestaurantesNovos();
        Task<List<RestaurantePedidoDto>> ListarDadosPedidoRestaurantes(List<Guid> idsRestaurantes);
    }
}
