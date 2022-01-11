using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFoody.CrossCutting.IoC
{
    public static class DependencyResolverRedis
    {

        public static void AddDependencyResolverRedis(this IServiceCollection services)
        {
            var connRedis = "localhost:6379, name=redis, Password=, abortConnect=false";
            services.AddSingleton((escoped) => new Lazy<ConnectionMultiplexer>(()=>  ConnectionMultiplexer.Connect(connRedis)));
        }
    }
}
