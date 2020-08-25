using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApprovalUtilities.Utilities
{
    public static class StringUtils
    {
        /// <summary>
        ///     A better string formatter for enumerables.
        /// </summary>
        public static string ToReadableString(this IEnumerable list)
        {
            if (list == null)
            {
                return "[]";
            }

            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var l in list)
            {
                sb.Append(l + ", ");
            }
            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            sb.Append("]");
            return sb.ToString();
        }

        public static string DisplayGrid(int width, int height, Func<int, int, string> func)
        {
            var b = new StringBuilder("  ");
            for (var x = 0; x < width; x++)
            {
                b.Append($"{x:0} ");
            }
            b.AppendLine();
            for (var y = 0; y < height; y++)
            {
                b.Append($"{y:0} ");
                for (var x = 0; x < width; x++)
                {
                    b.Append(func(x, y) + " ");
                }
                b.AppendLine();
            }
            return b.ToString();
        }

        public static string FormatFrame(char frameMarker, params string[] lines)
        {
            var sb = new StringBuilder();
            const int totalWidth = 86;
            var lineBreakOut = "".PadLeft(totalWidth, frameMarker);
            var lineBreakIn = string.Format("{0}{1}{0}", frameMarker, "".PadLeft(totalWidth - 2, ' '));
            sb.AppendLine(lineBreakOut);
            sb.AppendLine(lineBreakIn);
            foreach (var line in lines)
            {
                sb.AppendLine(string.Format("{1} {0}",line.Replace(Environment.NewLine, $"{Environment.NewLine}{frameMarker} "), frameMarker));
            }
            sb.AppendLine(lineBreakIn);
            sb.AppendLine(lineBreakOut);
            return sb.ToString().Trim();
        }

        public static string Write<T>(this IEnumerable<T> enumerable, string label)
        {
            return Write(enumerable, label, s => "" + s);
        }

        public static string Write<T>(this IEnumerable<T> enumerable, string label, Func<T, string> formatter)
        {
            return enumerable.Write((i, s) => $"{label}[{i}] = {formatter(s)}\n", $"{label} is empty");
        }

        public static string Write<T>(this IEnumerable<T> enumerable, Func<T, string> formatter)
        {
            return enumerable.Write((i, s) => formatter(s) + Environment.NewLine, "Empty");
        }

        public static string Write<T>(this IEnumerable<T> enumerable, Func<int, T, string> formatterWithIndex, string emptyMessage)
        {
            var list = new List<T>(enumerable ?? Enumerable.Empty<T>());

            if (list.Count == 0)
                return emptyMessage;

            var sb = new StringBuilder();
            var i = 0;
            list.ForEach(item => sb.Append(formatterWithIndex(i++, item)));

            return sb.ToString();
        }

        public static string WritePropertiesToString<T>(this T value)
        {
            return WriteObjectToString(value, WriteProperties);
        }

        private static void WriteProperties<T>(T value, StringBuilder sb, Type t)
        {
            foreach (var p in t.GetProperties())
            {
                if (p.CanRead)
                {
                    var propertyValue = p.GetValue(value, new object[0]) ?? "NULL";
                    sb.AppendFormat("\t{0}: {1}", p.Name, propertyValue).AppendLine();
                }
            }
        }

        public static string WriteFieldsToString<T>(this T value)
        {
            return WriteObjectToString(value, WriteFields);
        }

        private static void WriteFields<T>(T value, StringBuilder sb, Type t)
        {
            foreach (var f in t.GetFields())
            {
                if (f.IsPublic)
                {
                    var propertyValue = f.GetValue(value) ?? "NULL";
                    sb.AppendFormat("\t{0}: {1}", f.Name, propertyValue).AppendLine();
                }
            }
        }

        private static string WriteObjectToString<T>(T value, Action<T, StringBuilder, Type> writer)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var t = typeof (T);
            var sb = new StringBuilder();
            sb.AppendLine(t.Name);
            sb.AppendLine("{");
            writer(value, sb, t);

            sb.AppendLine("}");

            return sb.ToString();
        }

        public static string JoinWith<T>(this IEnumerable<T> elements, string separator)
        {
            return string.Join(separator, elements);
        }

        public static string RemoveIndentation(this string indentedText)
        {

            var firstRemoved = indentedText.Split('\n').Skip(1).ToArray();
            var lastRemoved = firstRemoved.Take(firstRemoved.Length - 1);
            var space = FindSpaces(lastRemoved);
            var dedented = lastRemoved.Select( l => RemoveSpaces(space, l)).JoinWith("\n");

            return dedented;
        }

        private static string RemoveSpaces(int space, string s)
        {
            return space < s.Length ?  s.Substring(space) : "";
        }

        private static int FindSpaces(IEnumerable<string> lines)
        {
            return lines.Where(l => !string.IsNullOrWhiteSpace(l)).Min(l => l.Length - l.TrimStart().Length);
        }
    }
}