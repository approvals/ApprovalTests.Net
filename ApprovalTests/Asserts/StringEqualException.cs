using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ApprovalTests.Asserts
{
    public class StringEqualException : Exception
    {
        static readonly Dictionary<char, string> Encodings = new Dictionary<char, string>
        {
            {'\r', "\\r"},
            {'\n', "\\n"},
            {'\t', "\\t"},
            {'\0', "\\0"}
        };

        /// <summary>
        /// Gets the actual value.
        /// </summary>
        public string Actual { get; }

        /// <summary>
        /// Gets the expected value.
        /// </summary>
        public string Expected { get; }

        private string message;

        /// <summary>
        /// Creates a new instance of the <see cref="StringEqualException"/> class for string comparisons.
        /// </summary>
        /// <param name="expected">The expected string value</param>
        /// <param name="actual">The actual string value</param>
        /// <param name="expectedIndex">The first index in the expected string where the strings differ</param>
        /// <param name="actualIndex">The first index in the actual string where the strings differ</param>
        public StringEqualException(string expected, string actual, int expectedIndex, int actualIndex)
        {
            Actual = actual;
            ActualIndex = actualIndex;
            Expected = expected;
            ExpectedIndex = expectedIndex;
        }

        private string UserMessage => "The string are not equal";

        /// <summary>
        /// Gets the index into the actual value where the values first differed.
        /// Returns -1 if the difference index points were not provided.
        /// </summary>
        public int ActualIndex { get; }

        /// <summary>
        /// Gets the index into the expected value where the values first differed.
        /// Returns -1 if the difference index points were not provided.
        /// </summary>
        public int ExpectedIndex { get; }

        /// <inheritdoc/>
        public override string Message => message ?? (message = CreateMessage());

        string CreateMessage()
        {
            if (ExpectedIndex == -1)
                return base.Message;

            var printedExpected = ShortenAndEncode(Expected, ExpectedIndex, '↓');
            var printedActual = ShortenAndEncode(Actual, ActualIndex, '↑');

            return string.Format(
                CultureInfo.CurrentCulture,
                "{1}{0}          {2}{0}Expected: {3}{0}Actual:   {4}{0}          {5}",
                Environment.NewLine,
                UserMessage,
                printedExpected.Item2,
                printedExpected.Item1 ?? "(null)",
                printedActual.Item1 ?? "(null)",
                printedActual.Item2
            );
        }

        static Tuple<string, string> ShortenAndEncode(string value, int position, char pointer)
        {
            var start = Math.Max(position - 20, 0);
            var end = Math.Min(position + 41, value.Length);
            var printedValue = new StringBuilder(100);
            var printedPointer = new StringBuilder(100);

            if (start > 0)
            {
                printedValue.Append("···");
                printedPointer.Append("   ");
            }

            for (var idx = start; idx < end; ++idx)
            {
                var c = value[idx];
                var paddingLength = 1;

                if (Encodings.TryGetValue(c, out var encoding))
                {
                    printedValue.Append(encoding);
                    paddingLength = encoding.Length;
                }
                else
                    printedValue.Append(c);

                if (idx < position)
                    printedPointer.Append(' ', paddingLength);
                else if (idx == position)
                    printedPointer.AppendFormat("{0} (pos {1})", pointer, position);
            }

            if (value.Length == position)
                printedPointer.AppendFormat("{0} (pos {1})", pointer, position);

            if (end < value.Length)
                printedValue.Append("···");

            return new Tuple<string, string>(printedValue.ToString(), printedPointer.ToString());
        }
    }
}