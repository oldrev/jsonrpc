using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sandwych.JsonRpc.Test
{
    [JsonRpcService("/projects/jayrock/demo.ashx")]
    public interface ITestJsonRpcService
    {

        [JsonRpcMethod("echo")]
        Task<string> EchoAsync(string text);

        [JsonRpcMethod("total")]
        Task<int> TotalAsync(IEnumerable<int> values);
    }
}
