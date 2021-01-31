using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using PaymentsGateway.Extensions;

namespace PaymentsGateway.HttpClients
{
    public abstract class BaseHttpClient
    {
        private readonly HttpClient _httpClient;

        protected BaseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected Task<HttpResponseMessage> GetAsync(string path, Action<HttpRequestHeaders> configureHeaders = null,
            CancellationToken token = default)
        {
            configureHeaders?.Invoke(_httpClient.DefaultRequestHeaders);
            return _httpClient.GetAsync(path, token);
        }
        
        protected Task<HttpResponseMessage> PutAsync<T>(string path, T payload, Action<HttpRequestHeaders> configureHeaders = null,
            CancellationToken token = default)
        {
            configureHeaders?.Invoke(_httpClient.DefaultRequestHeaders);
            return _httpClient.PutAsync(path, payload.ToJsonStringContent(), token);
        }
    }
}