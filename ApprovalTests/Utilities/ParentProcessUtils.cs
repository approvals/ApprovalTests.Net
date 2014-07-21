using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ApprovalTests.Utilities
{
	static class ParentProcessUtils
	{
		public static Process GetParentProcess(Process currentProcess)
		{
			try
			{
				var pc = new PerformanceCounter("Process", "Creating Process Id", currentProcess.ProcessName);
				using (pc)
					return Process.GetProcessById((int)pc.RawValue);
			}
			catch (ArgumentException)
			{
				return null;
			}
		}

		public static IEnumerable<Process> CurrentProcessWithAncestors()
		{
			return GetSelfAndAncestors(Process.GetCurrentProcess());
		}

		public static IEnumerable<Process> GetSelfAndAncestors(Process process)
		{
			while (process != null)
			{
				yield return process;
				process = GetParentProcess(process);
			}
		}
	}
}
