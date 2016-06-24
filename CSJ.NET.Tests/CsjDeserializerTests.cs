using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NFluent;

using Xunit;

namespace CSJ.NET.Tests
{
    public class CsjDeserializerTests
    {
        private sealed class SimpleObject
        {
            public int Integer { get; set; }
            public double Double { get; set; }
            public string String { get; set; }
            public DateTime Date { get; set; }
        }

        [Fact]
        public void DeserializeSimpleObject()
        {
            var subject = new CsjDeserializer<SimpleObject>();
            const string csj = @"""Integer"",""Double"",""String"",""Date""
2,42.23,""foobar"",""2015-02-03T10:37:15Z""
15,12.43,""bazbox"",""2015-02-03T10:37:15Z""";

            var results = subject.Deserialize(csj).ToArray();

            Check.That(results).HasSize(2);
            Check.That(results[0]).HasFieldsWithSameValues(new SimpleObject()
            {
                Integer = 2,
                Double = 42.23,
                String = "foobar",
                Date = new DateTime(2015, 2, 3, 10, 37, 15, DateTimeKind.Utc),
            });
            Check.That(results[1]).HasFieldsWithSameValues(new SimpleObject()
            {
                Integer = 15,
                Double = 12.43,
                String = "bazbox",
                Date = new DateTime(2015, 2, 3, 10, 37, 15, DateTimeKind.Utc),
            });
        }

        [Fact]
        public void DeserializesFromStream()
        {
            var subject = new CsjDeserializer<SimpleObject>();
            const string csj = @"""Integer"",""Double"",""String"",""Date""
2,42.23,""foobar"",""2015-02-03T10:37:15Z""
15,12.43,""bazbox"",""2015-02-03T10:37:15Z""";
            var stream = new StringReader(csj);

            var results = subject.Deserialize(stream).ToArray();

            Check.That(results).HasSize(2);
            Check.That(results[0]).HasFieldsWithSameValues(new SimpleObject()
            {
                Integer = 2,
                Double = 42.23,
                String = "foobar",
                Date = new DateTime(2015, 2, 3, 10, 37, 15, DateTimeKind.Utc),
            });
            Check.That(results[1]).HasFieldsWithSameValues(new SimpleObject()
            {
                Integer = 15,
                Double = 12.43,
                String = "bazbox",
                Date = new DateTime(2015, 2, 3, 10, 37, 15, DateTimeKind.Utc),
            });
        }

        [Fact]
        public void AllowsReorderingOfHeaders()
        {
            var subject = new CsjDeserializer<SimpleObject>();
            const string csj = @"""Date"",""String"",""Double"",""Integer""
""2015-02-03T10:37:15Z"",""foobar"",42.23,2
""2015-02-03T10:37:15Z"",""bazbox"",12.43,15";

            var results = subject.Deserialize(csj).ToArray();

            Check.That(results).HasSize(2);
            Check.That(results[0]).HasFieldsWithSameValues(new SimpleObject()
            {
                Integer = 2,
                Double = 42.23,
                String = "foobar",
                Date = new DateTime(2015, 2, 3, 10, 37, 15, DateTimeKind.Utc),
            });
            Check.That(results[1]).HasFieldsWithSameValues(new SimpleObject()
            {
                Integer = 15,
                Double = 12.43,
                String = "bazbox",
                Date = new DateTime(2015, 2, 3, 10, 37, 15, DateTimeKind.Utc),
            });
        }

        public class NestedArrayExample
        {
            public string Name { get; set; }
            public string[] Tags { get; set; }
        }

        [Fact]
        public void DeserializesNestedArrays()
        {
            var subject = new CsjDeserializer<NestedArrayExample>();
            const string csj = @"""Name"",""Tags""
""Foo"",[""three"",""fake""]";

            var results = subject.Deserialize(csj).ToArray();

            Check.That(results).HasSize(1);
            Check.That(results[0]).HasFieldsWithSameValues(new NestedArrayExample()
            {
                Name = "Foo",
                Tags = new[] { "three", "fake" }
            });
        }

    }
}
