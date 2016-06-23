using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NFluent;

using Xunit;

namespace CSJ.NET.Tests
{
    public class CsjSerializerObjectTests
    {
        private sealed class MovieExample
        {
            public string Slug { get; set; }
            public string Title { get; set; }
            public int Length { get; set; }
            public DateTime Released { get; set; }
            public string[] Tags { get; set; }
            public int? WatchedTimes { get; set; }
        }

        [Fact]
        public void SerializesHeader()
        {
            var subject = new CsjSerializer<MovieExample>();

            var result = subject.Serialize(Enumerable.Empty<MovieExample>());

            Check.That(result).IsEqualTo(@"""Slug"",""Title"",""Length"",""Released"",""Tags"",""WatchedTimes""" + Environment.NewLine);
        }

        [Fact]
        public void SerializesRow()
        {
            var subject = new CsjSerializer<MovieExample>();

            var result = subject.Serialize(new[]
            {
                new MovieExample
                {
                    Slug = "t1",
                    Title = "Terminator",
                    Length = 127,
                    Released = new DateTime(1984, 10, 26, 0, 0, 0, DateTimeKind.Utc),
                    Tags = new[] { "adventure", "action" },
                    WatchedTimes = null
                }
            });

            var firstRow = result.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Last();
            Check.That(firstRow)
                .IsEqualTo(@"""t1"",""Terminator"",127,""1984-10-26T00:00:00Z"",[""adventure"",""action""],null");
        }
    }
}
