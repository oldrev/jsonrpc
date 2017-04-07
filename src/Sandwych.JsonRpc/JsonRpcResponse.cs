/* JSON-RPC Portable Library For .NET 4.5+
 * Copyright (C) 2016 By Wei Li  <oldrev@gmail.com>
 * Changelog:
 * 1.0: A init impl.
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Sandwych.JsonRpc
{
    [JsonObject, Serializable]
    public sealed class JsonRpcResponse<T, TErrorData>
    {
        private JsonRpcResponse()
        {
        }

        [JsonProperty("jsonrpc", Required = Required.Default)]
        public JsonRpcVersion? Version { get; set; }

        [JsonProperty("result", Required = Required.Default)]
        public T Result { get; set; }

        [JsonProperty("error", Required = Required.Default)]
        public JsonRpcError<TErrorData> Error { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        public static JsonRpcResponse<T, TErrorData> FromJson(string json)
        {
            return FromJson(json, JsonConvert.DefaultSettings());
        }

        public static JsonRpcResponse<T, TErrorData> FromJson(string json, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<JsonRpcResponse<T, TErrorData>>(json, settings);
        }
    }
}
