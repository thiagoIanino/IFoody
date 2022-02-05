using IFoody.Domain.Dtos;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.CrossCutting.IoC
{
    public static class DependencyResolver
    {

        public static void AddDependencyResolver(this IServiceCollection services)
        {
            services.AddDependencyResolverApplication();
            services.AddDependencyResolverRepository();
            services.AddDependencyResolverService();
            services.AddDependencyResolverRedis();
            services.AddSignalR();
            services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());
        }
    }
}
