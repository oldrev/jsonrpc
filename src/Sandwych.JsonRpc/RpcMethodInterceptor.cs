using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using System.Reflection;

namespace Sandwych.JsonRpc
{

    /// <summary>
    /// The interceptor for user's service interface
    /// </summary>
    internal class RpcMethodInterceptor : IInterceptor
    {
        private static readonly MethodInfo _handleAsyncMethodInfo =
            typeof(RpcMethodInterceptor)
            .GetMethod(nameof(RpcMethodInterceptor.HandleAsyncWithResult), BindingFlags.Static | BindingFlags.NonPublic);

        private static readonly MethodInfo _invokeAsyncGenericMethodInfo =
            typeof(IRpcClient)
            .GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .Single(mi => mi.Name == nameof(IRpcClient.InvokeFuncAsync) && mi.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));

        private readonly IRpcClient _rpcClient;
        private readonly IJsonRpcServiceInfo _serviceInfo;

        public RpcMethodInterceptor(IRpcClient rpcClient, Type interfaceType, IJsonRpcServiceInfo serviceInfo)
        {
            _rpcClient = rpcClient;
            _serviceInfo = serviceInfo;
        }

        public void Intercept(IInvocation invocation)
        {
            var jsonRpcMethodInfo = invocation.Method.GetCustomAttribute<JsonRpcMethodAttribute>() as IJsonRpcMethodInfo;
            if (jsonRpcMethodInfo == null)
            {
                throw new InvalidOperationException();
            }

            var delegateType = this.GetDelegateType(invocation);
            switch (delegateType)
            {
                case MethodAsyncType.AsyncFunction:
                    if (jsonRpcMethodInfo.IsNotification)
                    {
                        throw new NotSupportedException();
                    }
                    var resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
                    var invokeGenericAsync = _invokeAsyncGenericMethodInfo.MakeGenericMethod(resultType);
                    var genericTask = invokeGenericAsync.Invoke(_rpcClient, new object[] { _serviceInfo.Endpoint, jsonRpcMethodInfo.Name, invocation.Arguments });
                    var asyncMi = _handleAsyncMethodInfo.MakeGenericMethod(resultType);
                    invocation.ReturnValue = asyncMi.Invoke(this, new[] { genericTask });
                    break;

                case MethodAsyncType.AsyncAction:
                    if (jsonRpcMethodInfo.IsNotification)
                    {
                        var task = _rpcClient.InvokeNotificationAsync(_serviceInfo.Endpoint, jsonRpcMethodInfo.Name, invocation.Arguments);
                        invocation.ReturnValue = HandleAsync(task);
                    }
                    else
                    {

                        var task = _rpcClient.InvokeActionAsync(_serviceInfo.Endpoint, jsonRpcMethodInfo.Name, invocation.Arguments);
                        invocation.ReturnValue = HandleAsync(task);
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }

        }

        private static async Task HandleAsync(Task task)
        {
            await task;
        }

        private static async Task<T> HandleAsyncWithResult<T>(Task<T> task)
        {
            return await task;
        }

        private MethodAsyncType GetDelegateType(IInvocation invocation)
        {
            var returnType = invocation.Method.ReturnType;
            if (returnType == typeof(Task))
            {
                return MethodAsyncType.AsyncAction;
            }

            if (returnType.IsConstructedGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                return MethodAsyncType.AsyncFunction;
            }

            return MethodAsyncType.Synchronous;
        }

        private enum MethodAsyncType
        {
            Synchronous,
            AsyncAction,
            AsyncFunction
        }
    }

}
