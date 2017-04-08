# Sandwych.JsonRpc: A JSON-RPC Client For .NET

[![NuGet Stats](https://img.shields.io/nuget/v/Sandwych.JsonRpc.svg)](https://www.nuget.org/packages/Sandwych.JsonRpc) 

Sandwych.JsonRpc is a JSON-RPC protocol client for .NET.

WARNING: This library should be considered *PRE-BETA* software - the API can and will change frequently, do not use it for production.

## Getting Started

### Prerequisites

* Visual Studio 2017: This project is written in C# 7.0 using Microsoft Visual Studio 2017 Community Edition.

### Installation

Sandwych.JsonRpc can be installed from [NuGet](https://www.nuget.org/packages/Sandwych.JsonRpc

or type following commands in the NuGet Console:

```
PM> Install-Package Sandwych.JsonRpc
```

## Demo & Usage:

### Step 1: Define your JSON-RPC interface:

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

### Step 2: Call your JSON-RPC using dynamic proxy approach:

```csharp
var settings = new Settings(); //Use default settings
var client = new HttpClient { BaseAddress = new Uri("http://www.raboof.com") };
var service = RpcProxyFactory.CreateProxyInstance<ITestJsonRpcService>(client, settings); //Create a dynamic proxy for the interface ITestJsonRpcService 
var echoResult = await service.EchoAsync("hello");
Console.WriteLine(echoResult); //Should print "hello"
```

And done!


# License

Author: Wei Li <oldrev(at)gmail.com>

This software is licensed in 3-clause BSD License.

Copyright (C) 2017-TODAY BinaryStars Technologies Yunnan LLC.
