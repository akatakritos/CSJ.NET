using System;
using System.Collections.Generic;
using System.IO;
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

        public CsjSerializer()
        {
            _columns = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        public string Serialize(IEnumerable<T> objects)
        {
            var output = new StringWriter();
            Serialize(output, objects);
            return output.ToString();
        }

        public void Serialize(TextWriter writer, IEnumerable<T> objects)
        {
            writer.WriteLine(Utils.JsonEncode(_columns.Select(p => p.Name)).Unwrap());

            foreach (var row in objects)
            {
                writer.WriteLine(Utils.JsonEncode(_columns.Select(c => c.GetValue(row))).Unwrap());
            }
        }

    }

}
