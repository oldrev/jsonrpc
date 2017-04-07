using System;
using System.Collections.Generic;
using System.Text;

namespace Sandwych.JsonRpc
{
    internal interface IJsonRpcMethodInfo
    {
        string Name { get; }
        Type ErrorDataType { get; }
        bool IsNotification { get; }
    }
}
