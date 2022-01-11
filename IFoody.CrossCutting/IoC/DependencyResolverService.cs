using IFoody.Application;
using IFoody.Application.Interfaces;
using IFoody.Domain.Interfaces.Services;
using IFoody.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.CrossCutting.IoC
{
    public static class DependencyResolverService
    {
        public static void AddDependencyResolverService(this IServiceCollection services)
        {
            services.AddScoped<IDominioClienteService, DominioClienteService>();
            services.AddScoped<IDominioRestauranteService, DominioRestauranteService>();
            services.AddScoped<IDominioPratoService, DominioPratoService>();
            services.AddScoped<IDominioPedidoService, DominioPedidoService>();
        }
    }
}
