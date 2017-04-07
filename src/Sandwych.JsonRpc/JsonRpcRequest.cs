using System;
using System.Threading;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Sandwych.JsonRpc;

namespace Sandwych.JsonRpc
{

    [JsonObject, Serializable]
    public sealed class JsonRpcRequest
    {
        private readonly Settings _settings;

        public JsonRpcRequest(Settings settings, string method, object args)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentNullException(nameof(method));
            }

            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            this.Method = method;
            this.Params = args;
            this.Id = _settings.GenerateRequestIdMethod();
        }

        [JsonProperty("method")]
        public string Method { get; private set; }

        [JsonProperty("params")]
        public object Params { get; private set; }

        [JsonProperty("id")]
        public string Id { get; private set; }

        [JsonProperty("jsonrpc")]
        public JsonRpcVersion? Version => JsonRpcVersion.Version2;

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, _settings.JsonSerializerSettings);
        }

    }
}
