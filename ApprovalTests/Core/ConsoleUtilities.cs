using System;
using System.Diagnostics;

namespace ApprovalTests.Core
{
	public class ConsoleUtilities
	{
		public static void WriteLine(string warning)
		{
			Console.WriteLine(warning);
			Debug.WriteLine(warning);
		}
	}
}