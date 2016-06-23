using System;
using System.Collections.Generic;
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
            var rows = csj.SplitLines();
            var headers = rows[0].Split(',').Select(s => s.Unwrap()).ToArray();
            foreach (var row in csj.SplitLines().Skip(1))
            {
                var output = new T();

                var array = JArray.Parse('[' + row + ']');
                for (int i = 0; i < array.Count; i++)
                {
                    var prop = GetProperty(headers[i]);
                    prop.SetValue(output, array[i].ToObject(prop.PropertyType));
                }

                yield return output;
            }

        }

    }
}
