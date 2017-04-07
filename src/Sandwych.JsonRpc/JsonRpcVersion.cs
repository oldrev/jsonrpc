using System;
using System.Runtime.Serialization;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Sandwych.JsonRpc
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum JsonRpcVersion
	{
		[EnumMember(Value = "1.0")]
		Version1 = 1,

		[EnumMember(Value = "2.0")]
		Version2 = 2
	}
}

