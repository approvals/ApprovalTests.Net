namespace ApprovalTests.Asserts
{
    public class StringAssert
    {
        /// <summary>
        /// Verifies that two strings are equivalent.
        /// </summary>
        /// <param name="expected">The expected string value.</param>
        /// <param name="actual">The actual string value.</param>
        /// <exception cref="StringEqualException">Thrown when the strings are not equivalent.</exception>
        public static void Equal(string expected, string actual)
        {
            Equal(expected, actual, false);
        }

        /// <summary>
        /// Verifies that two strings are equivalent.
        /// </summary>
        /// <param name="expected">The expected string value.</param>
        /// <param name="actual">The actual string value.</param>
        /// <param name="ignoreCase">If set to <c>true</c>, ignores cases differences. The invariant culture is used.</param>
        /// <param name="ignoreLineEndingDifferences">If set to <c>true</c>, treats \r\n, \r, and \n as equivalent.</param>
        /// <param name="ignoreWhiteSpaceDifferences">If set to <c>true</c>, treats spaces and tabs (in any non-zero quantity) as equivalent.</param>
        /// <exception cref="StringEqualException">Thrown when the strings are not equivalent.</exception>
        public static void Equal(string expected, string actual, bool ignoreCase = false, bool ignoreLineEndingDifferences = false, bool ignoreWhiteSpaceDifferences = false)
        {
            // Start out assuming the one of the values is null
            var expectedIndex = -1;
            var actualIndex = -1;
            var expectedLength = 0;
            var actualLength = 0;

            if (expected == null)
            {
                if (actual == null)
                    return;
            }
            else if (actual != null)
            {
                // Walk the string, keeping separate indices since we can skip variable amounts of
                // data based on ignoreLineEndingDifferences and ignoreWhiteSpaceDifferences.
                expectedIndex = 0;
                actualIndex = 0;
                expectedLength = expected.Length;
                actualLength = actual.Length;

                while (expectedIndex < expectedLength && actualIndex < actualLength)
                {
                    var expectedChar = expected[expectedIndex];
                    var actualChar = actual[actualIndex];

                    if (ignoreLineEndingDifferences && IsLineEnding(expectedChar) && IsLineEnding(actualChar))
                    {
                        expectedIndex = SkipLineEnding(expected, expectedIndex);
                        actualIndex = SkipLineEnding(actual, actualIndex);
                    }
                    else if (ignoreWhiteSpaceDifferences && IsWhiteSpace(expectedChar) && IsWhiteSpace(actualChar))
                    {
                        expectedIndex = SkipWhitespace(expected, expectedIndex);
                        actualIndex = SkipWhitespace(actual, actualIndex);
                    }
                    else
                    {
                        if (ignoreCase)
                        {
                            expectedChar = char.ToUpperInvariant(expectedChar);
                            actualChar = char.ToUpperInvariant(actualChar);
                        }

                        if (expectedChar != actualChar)
                        {
                            break;
                        }

                        expectedIndex++;
                        actualIndex++;
                    }
                }
            }

            if (expectedIndex < expectedLength || actualIndex < actualLength)
            {
                throw new StringEqualException(expected, actual, expectedIndex, actualIndex);
            }
        }

        static bool IsLineEnding(char c)
        {
            return c == '\r' || c == '\n';
        }

        static bool IsWhiteSpace(char c)
        {
            return c == ' ' || c == '\t';
        }

        static int SkipLineEnding(string value, int index)
        {
            if (value[index] == '\r')
            {
                ++index;
            }

            if (index < value.Length && value[index] == '\n')
            {
                ++index;
            }

            return index;
        }

        static int SkipWhitespace(string value, int index)
        {
            while (index < value.Length)
            {
                switch (value[index])
                {
                    case ' ':
                    case '\t':
                        index++;
                        break;

                    default:
                        return index;
                }
            }

            return index;
        }
    }
}
