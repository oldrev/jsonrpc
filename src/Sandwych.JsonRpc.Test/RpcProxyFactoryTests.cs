using System;
using System.Net;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;

namespace Sandwych.JsonRpc.Test
{

    public class RpcProxyFactoryTests
    {
        private readonly static HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://www.raboof.com")
        };

        [Fact]
        public async Task EchoAsyncShouldBeOkAsync()
        {
            var settings = new Settings();
            var service = RpcProxyFactory.CreateProxyInstance<ITestJsonRpcService>(_client, settings);

            var echoResult = await service.EchoAsync("hello");

            Assert.Equal("hello", echoResult);
        }


        [Fact]
        public async Task ArrayArgumentShouldBeOkAsync()
        {
            var settings = new Settings();
            var service = RpcProxyFactory.CreateProxyInstance<ITestJsonRpcService>(_client, settings);

            var totalResult = await service.TotalAsync(new[] { 1, 2, 3, 4, 5 });

            Assert.Equal(15, totalResult);
        }

    }


}
