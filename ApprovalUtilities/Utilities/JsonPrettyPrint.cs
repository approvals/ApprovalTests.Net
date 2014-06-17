using System.Linq;
using System.Text;

namespace ApprovalUtilities.Utilities

{
	public static class JsonPrettyPrint
	{
		private const string INDENT_STRING = "    ";

		public static string FormatJson(this string str)
		{
			var indent = 0;
			var quoted = false;
			var sb = new StringBuilder();
			for (var i = 0; i < str.Length; i++)
			{
				var ch = str[i];
				switch (ch)
				{
					case '{':
						sb.Append(ch);
						if (!quoted)
						{
							if (str[i + 1] != '}')
							{
								sb.AppendLine();
								Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
							}
						}
						break;
					case '[':
						sb.Append(ch);
						if (!quoted)
						{
							if (str[i + 1] != ']')
							{
								sb.AppendLine();
								Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
							}
						}
						break;
					case '}':
						if (!quoted)
						{
							if (str[i - 1] != '{')
							{
								sb.AppendLine();
								Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
							}
						}
						sb.Append(ch);
						break;
					case ']':
						if (!quoted)
						{
							if (str[i - 1] != '[')
							{
								sb.AppendLine();
								Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
							}
						}
						sb.Append(ch);
						break;
					case '"':
						sb.Append(ch);
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
						sb.Append(ch);
						if (!quoted)
						{
							sb.AppendLine();
							Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
						}
						break;
					case ':':
						sb.Append(ch);
						if (!quoted)
						{
							sb.Append(" ");
						}
						break;
					default:
						sb.Append(ch);
						break;
				}
			}
			return sb.ToString();
		}
	}
}