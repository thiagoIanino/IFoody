using IFoody.Application;
using IFoody.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.CrossCutting.IoC
{
    public static class DependencyResolverApplication
    {
        public static void AddDependencyResolverApplication(this IServiceCollection services)
        {
            services.AddScoped<IClienteApplication, ClienteApplication>();
            services.AddScoped<IRestauranteApplication, RestauranteApplication>();
            services.AddScoped<IPratoApplication, PratoApplication>();
            services.AddScoped<IPedidoApplication, PedidoApplication>();
        }
    }
}
