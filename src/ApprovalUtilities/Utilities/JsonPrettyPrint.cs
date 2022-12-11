using System.Text;

namespace ApprovalUtilities.Utilities;

public static class JsonPrettyPrint
{
    const string INDENT_STRING = "    ";

    public static string FormatJson(this string str)
    {
        var indent = 0;
        var quoted = false;
        var builder = new StringBuilder();
        for (var i = 0; i < str.Length; i++)
        {
            var ch = str[i];
            switch (ch)
            {
                case '{':
                    builder.Append(ch);
                    if (!quoted)
                    {
                        if (str[i + 1] != '}')
                        {
                            builder.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(_ => builder.Append(INDENT_STRING));
                        }
                    }

                    break;
                case '[':
                    builder.Append(ch);
                    if (!quoted)
                    {
                        if (str[i + 1] != ']')
                        {
                            builder.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(_ => builder.Append(INDENT_STRING));
                        }
                    }

                    break;
                case '}':
                    if (!quoted)
                    {
                        if (str[i - 1] != '{')
                        {
                            builder.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(_ => builder.Append(INDENT_STRING));
                        }
                    }

                    builder.Append(ch);
                    break;
                case ']':
                    if (!quoted)
                    {
                        if (str[i - 1] != '[')
                        {
                            builder.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(_ => builder.Append(INDENT_STRING));
                        }
                    }

                    builder.Append(ch);
                    break;
                case '"':
                    builder.Append(ch);
                    var escaped = false;
                    var index = i;
                    while (index > 0 && str[--index] == '\\')
                    {
                        escaped = !escaped;
                    }

                    if (!escaped)
                    {
                        quoted = !quoted;
                    }

                    break;
                case ',':
                    builder.Append(ch);
                    if (!quoted)
                    {
                        builder.AppendLine();
                        Enumerable.Range(0, indent).ForEach(_ => builder.Append(INDENT_STRING));
                    }

                    break;
                case ':':
                    builder.Append(ch);
                    if (!quoted)
                    {
                        builder.Append(" ");
                    }

                    break;
                default:
                    builder.Append(ch);
                    break;
            }
        }

        return builder.ToString();
    }
}