using System.Text.RegularExpressions;
using ApprovalTests.Scrubber;

namespace ApprovalTests
{
	public class StackTraceScrubber
	{
		public static string ScrubAnonymousIds(string source)
		{
			var regex = new Regex(@"\w+__\w+");
			return regex.Replace(source, string.Empty);
		}

		public static string ScrubLineNumbers(string source)
		{
			var regex = new Regex(@":line \d+");
			return regex.Replace(source, string.Empty);
		}

		public static string ScrubPaths(string source)
		{
			var regex = new Regex(@"\w:[\\\w.\s-]+\\");
			return regex.Replace(source, "...\\");
		}
		
		public static string Scrub(string text)
		{
			return ScrubberUtils.Combine(ScrubAnonymousIds, ScrubPaths, ScrubLineNumbers)(text);
		}
	}
}
