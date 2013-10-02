using System;
using System.Linq;

namespace ApprovalTests
{
	public class ScrubberUtils
	{
		public static Func<string, string> Combine(params Func<string, string>[] scrubbers)
		{
			return (inputText) => scrubbers.Aggregate(inputText, (c, s) => s(c));
		}

		public static Func<string, string> NO_SCRUBBER = (s) => s;
	}
}