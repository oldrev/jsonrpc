using System;

namespace Sandwych.JsonRpc
{

    [Serializable]
    public sealed class JsonRpcException<T> : Exception
    {
        public JsonRpcException(string msg, JsonRpcError<T> error)
            : base(msg)
        {
            this.Error = error;
        }

        public JsonRpcError<T> Error { get; private set; }

        public override string ToString()
        {
            return this.Error.ToString();
        }
    }
}
