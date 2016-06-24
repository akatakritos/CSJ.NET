using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json.Linq;

namespace CSJ.NET
{
    public class CsjDeserializer<T> where T:new()
    {
        private readonly PropertyInfo[] _columns;
        public CsjDeserializer()
        {
            _columns = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        private PropertyInfo GetProperty(string propName) => _columns.First(p => p.Name == propName);

        public IEnumerable<T> Deserialize(string csj)
        {
            var reader = new StringReader(csj);
            return Deserialize(reader);
        }

        public IEnumerable<T> Deserialize(TextReader reader)
        {
            var firstLine = reader.ReadLine();
            if (firstLine == null)
            {
                return Enumerable.Empty<T>();
            }

            var headers = firstLine.Split(',').Select(s => s.Unwrap()).ToArray();
            return DeserializeStream(reader, headers);
        }

        private IEnumerable<T> DeserializeStream(TextReader reader, IReadOnlyList<string> headers)
        {
            foreach(var line in ReadLines(reader))
            {
                var output = new T();
                var array = JArray.Parse('[' + line + ']');
                for (int i = 0; i < array.Count; i++)
                {
                    var prop = GetProperty(headers[i]);
                    prop.SetValue(output, array[i].ToObject(prop.PropertyType));
                }

                yield return output;
            }
        }

        private IEnumerable<string> ReadLines(TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

    }
}
