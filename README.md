# Sandwych.JsonRpc: A JSON-RPC Client For .NET

[![NuGet Stats](https://img.shields.io/nuget/v/Sandwych.JsonRpc.svg)](https://www.nuget.org/packages/Sandwych.JsonRpc) 
[![Build status](https://ci.appveyor.com/api/projects/status/smxfipheeacvlvyb/branch/master?svg=true)](https://ci.appveyor.com/project/oldrev/jsonrpc/branch/master)

Sandwych.JsonRpc is a JSON-RPC protocol client for .NET.

WARNING: This library should be considered *PRE-BETA* software - the API can and will change frequently, do not use it for production.

## Getting Started

### Prerequisites

* Visual Studio 2017: This project is written in C# 7.0 using Microsoft Visual Studio 2017 Community Edition.

### Supported Platform

* .NET Framework 4.5+
* .NET Standard 1.3+ (including .NET Core, Xamarin and others)


### Installation

Sandwych.JsonRpc can be installed from [NuGet](https://www.nuget.org/packages/Sandwych.JsonRpc) 

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

### Step 2: Call your JSON-RPC using a dynamic proxy approach:

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
