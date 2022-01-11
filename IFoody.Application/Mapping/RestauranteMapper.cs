using IFoody.Application.Models.Restaurantes;
using IFoody.Domain.Entities.Restaurantes;
using IFoody.Domain.Enumeradores.Avaliacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFoody.Application.Mapping
{
    public static class RestauranteMapper
    {
        public static IEnumerable<RestauranteModel> ToRestauranteListModel(this IEnumerable<Restaurante> restaurantes)
        {
            var restaurantesModel = restaurantes.Select(r => r.ToRestauranteModel());

            return restaurantesModel;
        }

        public static RestauranteModel ToRestauranteModel(this Restaurante restaurante)
        {
            return new RestauranteModel(restaurante);
        }
    }
}
