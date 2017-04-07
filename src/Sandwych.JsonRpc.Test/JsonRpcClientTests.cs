using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xunit;

namespace Sandwych.JsonRpc.Test
{


    public class JsonRpcClientTests
    {
        private readonly static HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://www.raboof.com")
        };


        [Fact]
        public async Task InvokeEchoShouldBeOkAsync()
        {
            var settings = new Settings();

            var client = new JsonRpcClient(_client, settings);

            var echoResult = await client.InvokeFuncAsync<string>(new Uri("/projects/jayrock/demo.ashx", UriKind.Relative), "echo", new[] { "hello" });
            Assert.Equal("hello", echoResult);
        }
    }
}
