using System;
using System.Collections.Generic;
using System.Linq;

namespace CSJ.NET
{
    internal static class StringExtensions
    {
        private static readonly string[] LineEndings = { "\n", "\r\n" };

        public static string Unwrap(this string s) => s.Substring(1, s.Length - 2);

        public static string[] SplitLines(this string s) => s.Split(LineEndings, StringSplitOptions.RemoveEmptyEntries);

    }
}