using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Reflection;
using Castle.DynamicProxy;

namespace Sandwych.JsonRpc
{
    public static class RpcProxyFactory
    {
        public static T CreateProxyInstance<T>(HttpClient client, Settings settings) where T : class
        {
            return CreateProxyInstance(client, typeof(T), settings) as T;
        }

        public static object CreateProxyInstance(HttpClient client, Type rpcServiceInterfaceType, Settings settings)
        {
            if (rpcServiceInterfaceType == null)
            {
                throw new ArgumentNullException(nameof(rpcServiceInterfaceType));
            }

            var ti = rpcServiceInterfaceType.GetTypeInfo();

            if (!ti.IsInterface)
            {
                throw new NotSupportedException();
            }

            var serviceInfo = ti.GetCustomAttribute<JsonRpcServiceAttribute>() as IJsonRpcServiceInfo;
            if (serviceInfo == null)
            {
                throw new InvalidOperationException();
            }

            var generator = new ProxyGenerator();
            var rpcClient = new JsonRpcClient(client, settings);
            var interceptor = new RpcMethodInterceptor(rpcClient, rpcServiceInterfaceType, serviceInfo);
            return generator.CreateInterfaceProxyWithoutTarget(rpcServiceInterfaceType, interceptor);
        }

    }
}
