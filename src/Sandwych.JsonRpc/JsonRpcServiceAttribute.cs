using System;
using System.Collections.Generic;
using System.Text;

namespace Sandwych.JsonRpc
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class JsonRpcServiceAttribute : Attribute, IJsonRpcServiceInfo
    {
        public JsonRpcServiceAttribute(string endpoint)
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            this.Endpoint = new Uri(endpoint, UriKind.Relative);
        }

        public JsonRpcServiceAttribute(Uri endpoint)
        {
            this.Endpoint = endpoint;
        }

        public Uri Endpoint { get; private set; }
    }
}
