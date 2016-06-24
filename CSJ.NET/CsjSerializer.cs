using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSJ.NET
{
    public class CsjSerializer
    {
        public string Serialize(IEnumerable<object[]> rows)
        {
            var buffer = new StringBuilder();
            foreach (var row in rows)
            {
                buffer.AppendLine(Utils.JsonEncode(row).Unwrap());
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
            buffer.AppendLine(Utils.JsonEncode(_columns.Select(p => p.Name)).Unwrap());

            foreach (var row in objects)
            {
                buffer.AppendLine(Utils.JsonEncode(_columns.Select(c => c.GetValue(row))).Unwrap());
            }

            return buffer.ToString();
        }

    }

}
