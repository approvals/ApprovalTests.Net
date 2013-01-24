using System.Collections;
using System.Text;

namespace ApprovalTests
{
    public static class StringUtils
    {
        public static string ToReadableString(this IEnumerable list)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (object l in list)
            {
                sb.Append(l + ", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");
            return sb.ToString();
        }

			public static string FormatWith(this string mask, params object[] parameters)
			{
				return string.Format(mask, parameters);
			}
    }
}