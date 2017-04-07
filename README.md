# Sandwych.JsonRpc: A JSON-RPC Client For .NET


## Step 1: Define your JSON-RPC interface:

```csharp
[JsonRpcService("/projects/jayrock/demo.ashx")]
public interface ITestJsonRpcService
{
    [JsonRpcMethod("echo")]
    Task<string> EchoAsync(string text);

    [JsonRpcMethod("total")]
    Task<int> TotalAsync(IEnumerable<int> values);
}
```
