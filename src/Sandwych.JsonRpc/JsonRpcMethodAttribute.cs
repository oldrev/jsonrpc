using System;
using System.Collections.Generic;
using System.Text;

namespace Sandwych.JsonRpc
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class JsonRpcMethodAttribute : Attribute, IJsonRpcMethodInfo
    {
        public JsonRpcMethodAttribute(string name, bool isNotification = false)
        {
            this.Name = name;
            this.IsNotification = isNotification;
        }

        public string Name { get; set; }

        public Type ErrorDataType { get; set; }

        public bool IsNotification { get; set; }
    }

}
