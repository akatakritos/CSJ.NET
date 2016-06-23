using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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

    internal static class StringExtensions
    {
        public static string StripArray(this string s)
        {
            return s.Substring(1, s.Length - 2);
        }
    }

    public class CsjSerializer
    {
        public string Serialize(IEnumerable<object[]> rows)
        {
            var buffer = new StringBuilder();
            foreach (var row in rows)
            {
                buffer.AppendLine(Utils.JsonEncode(row).StripArray());
            }

            return buffer.ToString();
        }
    }

    public class CsjSerializer<T>
    {
        private readonly PropertyInfo[] _columns;
        private readonly Type _type;

        public CsjSerializer()
        {
            _type = typeof(T);
            _columns = _type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        public string Serialize(IEnumerable<T> objects)
        {
            var buffer = new StringBuilder();
            buffer.AppendLine(Utils.JsonEncode(_columns.Select(p => p.Name)).StripArray());

            foreach (var row in objects)
            {
                buffer.AppendLine(Utils.JsonEncode(_columns.Select(c => c.GetValue(row))).StripArray());
            }

            return buffer.ToString();
        }

    }

}
