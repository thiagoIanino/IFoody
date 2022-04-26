using IFoody.Domain.Dtos;
using IFoody.Domain.Entities.Restaurantes;
using IFoody.Infrastructure.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoody.Infrastructure.DbMapper
{
    public static class RestauranteDbMapper
    {

        public static List<RestaurantePedidoDto> ToPedidoRestauranteListDto(this IEnumerable<RestaurantePedidoModel> restaurante)
        {
            return restaurante.Select(x => x.ToPedidoRestauranteDto()).ToList();
        }

        public static IEnumerable<RestauranteDto> ToRestauranteListDto(this IEnumerable<RestauranteDbModel> restaurante)
        {
            return restaurante.Select(x => x.ToRestauranteDto());
        }

        public static RestauranteDto ToRestauranteDto(this RestauranteDbModel restaurante)
        {
            return new RestauranteDto
            {
                Id = restaurante.Id,
                NomeRestaurante = restaurante.NomeRestaurante,
                Classificacao = new ClassificacaoDto
                {
                    Nota = restaurante.Nota,
                    Status = restaurante.Status
                },
                Tipo = restaurante.Tipo,
                SubDescricao = restaurante.SubDescricao,
                TempoMedioEntrega = restaurante.TempoMedioEntrega,
                UrlLogo = restaurante.UrlLogo
            };
        } 
        public static RestaurantePedidoDto ToPedidoRestauranteDto(this RestaurantePedidoModel restaurante)
        {
            return new RestaurantePedidoDto
            {
                Id = restaurante.Id,
                NomeRestaurante = restaurante.NomeRestaurante,
                TempoMedioEntrega = restaurante.TempoMedioEntrega,
                UrlLogo = restaurante.UrlLogo
            };
        }
    }
}
