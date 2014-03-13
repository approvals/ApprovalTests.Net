using System;
using System.Collections.Generic;

namespace ApprovalTests.Reporters
{
	public class ReporterEvents
	{
		public static readonly List<Action<string>> CreateNewFileEventListeners = 
			new List<Action<string>>() { };

		public static void CreatedApprovedFile(string approved)
		{
			foreach (var listener in CreateNewFileEventListeners)
			{
				listener(approved);
			}
		}
	}
}