using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sandwych.JsonRpc
{
    public interface IRpcClient
    {
        Task<T> InvokeFuncWithErrorDataAsync<T, TErrorData>(Uri path, string method, object args);
        Task<T> InvokeFuncAsync<T>(Uri path, string method, object args);
        Task InvokeActionAsync(Uri path, string method, object args);
        Task InvokeActionWithErrorDataAsync<TErrorData>(Uri path, string method, object args);
        Task InvokeNotificationAsync(Uri endpoint, string method, object args);
    }
}
