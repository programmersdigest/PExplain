using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PExplain.Output
{
    internal static class FormatUtils
    {
        public static string ToHex(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace('-', ' ');
        }

        public static string Escape(this string input)
        {
            if (input == null)
            {
                return null;
            }

            var literal = new StringBuilder(input.Length);
            foreach (var c in input)
            {
                switch (c)
                {
                    case '\'':
                        literal.Append(@"\'");
                        break;
                    case '\"':
                        literal.Append("\\\"");
                        break;
                    case '\\':
                        literal.Append(@"\\");
                        break;
                    case '\0':
                        literal.Append(@"\0");
                        break;
                    case '\a':
                        literal.Append(@"\a");
                        break;
                    case '\b':
                        literal.Append(@"\b");
                        break;
                    case '\f':
                        literal.Append(@"\f");
                        break;
                    case '\n':
                        literal.Append(@"\n");
                        break;
                    case '\r':
                        literal.Append(@"\r");
                        break;
                    case '\t':
                        literal.Append(@"\t");
                        break;
                    case '\v':
                        literal.Append(@"\v");
                        break;
                    default:
                        if (char.GetUnicodeCategory(c) != UnicodeCategory.Control)
                        {
                            literal.Append(c);
                        }
                        else
                        {
                            literal.Append(@"\u");
                            literal.Append(((ushort)c).ToString("x4"));
                        }
                        break;
                }
            }
            return literal.ToString();
        }

        public static IEnumerable<string> Split(this string value, int partLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                yield return null;
            }

            for (var i = 0; i < value.Length; i += partLength)
            {
                var length = Math.Min(partLength, value.Length - i);
                yield return value.Substring(i, length);
            }
        }
    }
}
