using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NFluent;

using Xunit;
using Xunit.Abstractions;

namespace CSJ.NET.Tests
{
    public class RoundTrip
    {
        private readonly ITestOutputHelper _output;

        public RoundTrip(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CanRoundTripLargeCollectionToFile()
        {
            var samples = GenerateSamples().Take(100).ToArray();
            var serializer = new CsjSerializer<Sample>();
            var deserializer = new CsjDeserializer<Sample>();

            var filename = Path.GetTempFileName();

            using (var writer = new StreamWriter(File.OpenWrite(filename), Encoding.UTF8))
            {
                serializer.Serialize(writer, samples);
            }

            using (var reader = new StreamReader(File.OpenRead(filename), Encoding.UTF8))
            {
                var result = deserializer.Deserialize(reader).ToArray();
                for (var i = 0; i < samples.Length; i++)
                {
                    Check.That(result[i]).HasFieldsWithSameValues(samples[i]);
                }
            }

            File.Delete(filename);
        }

        private sealed class Sample
        {
            public int Integer { get; set; }
            public decimal Decimal { get; set; }
            public string String { get; set; }
            public double Double { get; set; }
            public float Float { get; set; }
            public bool Boolean { get; set; }
            public Guid Guid { get; set; }
            public NestedSample Nested { get; set; }
            public string[] NestedStrings { get; set; }
        }

        public sealed class NestedSample
        {
            public int NestedInt { get; set; }
            public string NestedString { get; set; }
        }

        private IEnumerable<Sample> GenerateSamples()
        {
            int i = 0;
            while (i < 1000000)
            {
                i++;

                yield return new Sample
                {
                    Integer = i,
                    Decimal = (decimal)Math.Sin(i),
                    String = $"string-{i}",
                    Double = Math.Cos(i),
                    Float = (float)Math.Cos(i),
                    Boolean = i % 2 == 0,
                    Guid = Guid.NewGuid(),
                    Nested = new NestedSample
                    {
                        NestedInt = i + 10,
                        NestedString = $"nested-{i}"
                    },
                    NestedStrings = new[]
                    {
                        $"nested-sample-{i}",
                        "foobar"
                    }
                };
            }
        }
    }
}
