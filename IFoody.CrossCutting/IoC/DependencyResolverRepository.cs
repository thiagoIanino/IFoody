using IFoody.Application;
using IFoody.Application.Interfaces;
using IFoody.Domain.Repositories;
using IFoody.Domain.Repositories.Restaurantes;
using IFoody.Infrastructure.Repositories;
using IFoody.Infrastructure.Repositories.Restaurantes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.CrossCutting.IoC
{
    public static class DependencyResolverRepository
    {
        public static void AddDependencyResolverRepository(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IRestauranteRepository, RestauranteRepository>();
            services.AddScoped<IPratoRespository, PratoRespository>();
            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IStatusAvaliacaoRepository, StatusAvaliacaoRepository>();
            services.AddScoped<IRedisRepository, RedisRepository>();
            services.AddScoped<ICartaoCreditoRepository, CartaoCreditoRepository>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<IWebRepository, WebRepository>();

        }
    }
}
