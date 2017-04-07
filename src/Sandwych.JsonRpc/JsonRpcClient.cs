using System;
using System.Net;
using System.Threading;
using System.Dynamic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Concurrent;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sandwych.JsonRpc
{
    public class JsonRpcClient : IRpcClient
    {
        private readonly HttpClient _client;
        private readonly Settings _settings;

        public JsonRpcClient(HttpClient client, Settings settings)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<T> InvokeFuncWithErrorDataAsync<T, TErrorData>(Uri endpoint, string method, object args)
        {
            var jreq = new JsonRpcRequest(_settings, method, args);
            var response = await this.PostJsonRequestForCallingAsync<T, TErrorData>(jreq, endpoint);
            return response.Result;
        }

        public async Task InvokeActionAsync(Uri endpoint, string method, object args)
        {
            var jreq = new JsonRpcRequest(_settings, method, args);
            var response = await this.PostJsonRequestForCallingAsync<object, object>(jreq, endpoint);
        }

        public async Task InvokeActionWithErrorDataAsync<TErrorData>(Uri endpoint, string method, object args)
        {
            var jreq = new JsonRpcRequest(_settings, method, args);
            var response = await this.PostJsonRequestForCallingAsync<object, TErrorData>(jreq, endpoint);
        }

        public async Task<T> InvokeFuncAsync<T>(Uri endpoint, string method, object args)
        {
            return await this.InvokeFuncWithErrorDataAsync<T, object>(endpoint, method, args);
        }

        public async Task InvokeNotificationAsync(Uri endpoint, string method, object args)
        {
            var jreq = new JsonRpcRequest(_settings, method, args);
            await this.PostJsonRequestForNotifyAsync(jreq, endpoint);
        }

        private async Task<JsonRpcResponse<T, TErrorData>> PostJsonRequestForCallingAsync<T, TErrorData>(JsonRpcRequest request, Uri endpoint)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrEmpty(request.Id))
            {
                throw new ArgumentOutOfRangeException(nameof(request));
            }

            var requestJson = request.ToJson();
            using (var requestContent = new StringContent(requestJson, _settings.RequestEncoding, _settings.JsonMediaType))
            using (var httpResponse = await _client.PostAsync(endpoint, requestContent))
            {
                httpResponse.EnsureSuccessStatusCode();
                string responseJson = await httpResponse.Content.ReadAsStringAsync();
                var rpcResponse = JsonRpcResponse<T, TErrorData>.FromJson(responseJson, _settings.JsonSerializerSettings);

                if (string.IsNullOrEmpty(rpcResponse.Id) || request.Id != rpcResponse.Id)
                {
                    throw new InvalidOperationException("Different request-response IDs");
                }

                if (rpcResponse.Error != null)
                {
                    throw new JsonRpcException<TErrorData>("Server side error", rpcResponse.Error);
                }
                return rpcResponse;
            }
        }

        private async Task PostJsonRequestForNotifyAsync(JsonRpcRequest request, Uri endpoint)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var requestJson = request.ToJson();
            using (var requestContent = new StringContent(requestJson, _settings.RequestEncoding, _settings.JsonMediaType))
            using (var httpResponse = await _client.PostAsync(endpoint, requestContent))
            {
                httpResponse.EnsureSuccessStatusCode();
            }
        }

    }
}
