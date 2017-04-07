using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace Sandwych.JsonRpc
{

    [JsonObject("error"), Serializable]
    public sealed class JsonRpcError<T>
    {
        public JsonRpcError()
        {
        }

        public JsonRpcError(string code, string message, T data)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }

            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        [JsonProperty("data", Required = Required.Default)]
        public T Data { get; private set; }

        [JsonProperty("code", Required = Required.Always)]
        public string Code { get; private set; }

        [JsonProperty("message", Required = Required.Always)]
        public string Message { get; private set; }


    }
}
