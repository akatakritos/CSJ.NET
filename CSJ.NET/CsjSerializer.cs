using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace CSJ.NET
{
    public class CsjSerializer
    {
        private string JsonEncode<T>(T o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public string Serialize(IEnumerable<object[]> rows)
        {
            var buffer = new StringBuilder();
            foreach (var row in rows)
            {
                var encoded = JsonEncode(row);
                buffer.AppendLine(encoded.Substring(1, encoded.Length - 2));
            }

            return buffer.ToString();
        }
    }
}
