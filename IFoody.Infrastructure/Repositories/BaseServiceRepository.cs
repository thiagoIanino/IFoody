using IFoody.Domain.Dtos;
using IFoody.Domain.Exceptions;
using IFoody.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static IFoody.Domain.Constantes.Contantes;

namespace IFoody.Infrastructure.Repositories
{
    public class BaseServiceRepository<Entity> : BaseRepository<Entity>
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        public BaseServiceRepository(IHttpClientFactory httpClientFactory, IRedisRepository redisService) : base(redisService)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected async Task<HttpResponseMessage> Get(string nomeUrlBase, string requestUrl, Dictionary<string, string> headers, int? timeOut)
        {
            var client = _httpClientFactory.CreateClient(nomeUrlBase);

            if (timeOut.HasValue)
            {
                client.Timeout = TimeSpan.FromSeconds(timeOut.Value);
            }
            if (headers != null && headers.Any())
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            using var response = await client.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        protected async Task<T> Post<T,TBody>(string nomeUrlBase, string requestUrl, TBody body, Dictionary<string, string> headers, int? timeOut = null)
        {
            var client = _httpClientFactory.CreateClient(nomeUrlBase);

            if (timeOut.HasValue)
            {
                client.Timeout = TimeSpan.FromSeconds(timeOut.Value);
            }
            if (headers != null && headers.Any())
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            using var response = await client.PostAsJsonAsync<TBody>(requestUrl, body);

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<T>(res, options); ;
            }
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}
