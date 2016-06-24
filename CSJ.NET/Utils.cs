using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace CSJ.NET
{
    internal static class Utils
    {
        public static string JsonEncode<T>(T o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}