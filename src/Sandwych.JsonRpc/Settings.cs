using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Sandwych.JsonRpc
{
    public sealed class Settings
    {
        public Settings()
        {
            this.RequestEncoding = Encoding.UTF8;

            this.JsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
            };

            this.JsonMediaType = JsonRpcProtocolDefinition.JsonMediaType;

            this.GenerateRequestIdMethod = HandleGenerateRequestId;
        }

        public Encoding RequestEncoding { get; set; }

        public JsonSerializerSettings JsonSerializerSettings { get; private set; }

        public string JsonMediaType { get; set; }

        public Func<string> GenerateRequestIdMethod { get; set; }

        private static string HandleGenerateRequestId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
