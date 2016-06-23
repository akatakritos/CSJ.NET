using System;
using System.Collections.Generic;
using System.Linq;

using NFluent;

using Xunit;

namespace CSJ.NET.Tests
{
    public class CsjSerializerBasicFormattingTests
    {
        [Fact]
        public void Strings_WrappedInDoubleQuotes()
        {
            var subject = new CsjSerializer();
            var strings = new[] { "foo", "bar" };

            var output = subject.Serialize(new List<object[]> { strings });

            Check.That(output).IsEqualTo("\"foo\",\"bar\"" + Environment.NewLine);
        }

        [Fact]
        public void Strings_AreEscaped()
        {
            var subject = new CsjSerializer();
            var strings = new[] { "foo\nbar", "quote:\"me\"" };

            var output = subject.Serialize(new List<object[]> { strings });

            Check.That(output).IsEqualTo("\"foo\\nbar\",\"quote:\\\"me\\\"\"" + Environment.NewLine);
        }

        [Fact]
        public void Dates_AreISOFormatted()
        {
            var subject = new CsjSerializer();
            var dates = new object[]
            {
                new DateTime(2015, 2, 3, 10, 37, 15, DateTimeKind.Utc),
                new DateTime(2015, 2, 3, 10, 37, 15, DateTimeKind.Local),
            };

            var output = subject.Serialize(new List<object[]> { dates });
            var currentOffset = TimeZoneInfo.Local.BaseUtcOffset;
            var sign = currentOffset.Hours >= 0 ? '+' : '-';

            Check.That(output).IsEqualTo($"\"2015-02-03T10:37:15Z\",\"2015-02-03T10:37:15{sign}{currentOffset:hh':'mm}\"" + Environment.NewLine);
        }

        [Fact]
        public void Null_IsALiteral()
        {
            var subject = new CsjSerializer();
            var input = new object[] { null, null };

            var output = subject.Serialize(new List<object[]> { input });

            Check.That(output).IsEqualTo("null,null" + Environment.NewLine);
        }

        [Fact]
        public void Numbers_AreLiterals()
        {
            var subject = new CsjSerializer();
            var input = new object[] { 42, 15.759 };

            var output = subject.Serialize(new List<object[]> { input });

            Check.That(output).IsEqualTo("42,15.759" + Environment.NewLine);
        }

        [Fact]
        public void NestedArrays()
        {
            var subject = new CsjSerializer();
            var input = new object[] { 42, new[] {"foo","bar"} };

            var output = subject.Serialize(new List<object[]> { input });

            Check.That(output).IsEqualTo("42,[\"foo\",\"bar\"]" + Environment.NewLine);
        }

        private class NestedObject
        {
            public string Foo { get; set; }
            public string Baz { get; set; }
        }

        [Fact]
        public void NestedObjects()
        {
            var subject = new CsjSerializer();
            var input = new object[] { 42, new NestedObject()
            {
                Foo = "foo",
                Baz = "baz"
            }};

            var output = subject.Serialize(new List<object[]> { input });

            Check.That(output).IsEqualTo("42,{\"Foo\":\"foo\",\"Baz\":\"baz\"}" + Environment.NewLine);
        }
    }
}
