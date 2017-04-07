using System;
using System.Collections.Generic;
using System.Text;

namespace Sandwych.JsonRpc
{
    internal interface IJsonRpcServiceInfo
    {
        Uri Endpoint { get; }
    }
}
