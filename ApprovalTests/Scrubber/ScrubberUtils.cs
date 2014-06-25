using System;
using System.Linq;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Scrubber
{
	public class ScrubberUtils
	{
		public static Func<string, string> Combine(params Func<string, string>[] scrubbers)
		{
			return (inputText) => scrubbers.Aggregate(inputText, (c, s) => s(c));
		}

		public static Func<string, string> NO_SCRUBBER = (s) => s;

		public static Func<string,string> RemoveLinesContaining(string value)
		{
			return s => s.Split('\n').Where(l => !l.Contains(value)).JoinWith("\n");
		}
	}
}