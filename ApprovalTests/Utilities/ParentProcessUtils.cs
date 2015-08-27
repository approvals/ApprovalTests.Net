using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ApprovalTests.Utilities
{
	static class ParentProcessUtils
	{
		public static Process GetParentProcess(Process currentProcess)
		{
			return currentProcess.ParentProcess();
		}

		public static IEnumerable<Process> CurrentProcessWithAncestors()
		{
			return GetSelfAndAncestors(Process.GetCurrentProcess());
		}

		public static IEnumerable<Process> GetSelfAndAncestors(Process self)
		{
			var processIds = new HashSet<int>();

			var process = self;
			while (process != null)
			{
				yield return process;

				if (processIds.Contains(process.Id))
				{
					// loop detected (parent id have been re-allocated to a child process!)
					yield break;
				}
				processIds.Add(process.Id);

				process = process.ParentProcess();
			}
		}


		private static Process ParentProcess(this Process process)
		{
#if __MonoCS__
			// TODO: find a way to implement this in mono on a mac/linux machine
			return null;
#else
			var parentPid = 0;
			var processPid = process.Id;
			const uint TH32_CS_SNAPPROCESS = 2;
			// Take snapshot of processes
			var hSnapshot = CreateToolhelp32Snapshot(TH32_CS_SNAPPROCESS, 0);
			if (hSnapshot == IntPtr.Zero)
			{
				return null;
			}

			var procInfo = new PROCESSENTRY32 { dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32)) };


			// Read first
			if (Process32First(hSnapshot, ref procInfo) == false)
			{
				return null;
			}

			// Loop through the snapshot
			do
			{
				// If it's me, then ask for my parent.
				if (processPid == procInfo.th32ProcessID)
				{
					parentPid = (int)procInfo.th32ParentProcessID;
				}
			}

			while (parentPid == 0 && Process32Next(hSnapshot, ref procInfo)); // Read next

			if (parentPid <= 0)
			{
				return null;
			}

			try
			{
				return Process.GetProcessById(parentPid);
			}
			catch (ArgumentException)
			{
				//Process with an Id of X is not running
				return null;
			}
#endif
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessId);

		[DllImport("kernel32.dll")]
		private static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);


		[DllImport("kernel32.dll")]
		private static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		[StructLayout(LayoutKind.Sequential)]
		private struct PROCESSENTRY32
		{
			public uint dwSize;
			public uint cntUsage;
			public uint th32ProcessID;
			public IntPtr th32DefaultHeapID;
			public uint th32ModuleID;
			public uint cntThreads;
			public uint th32ParentProcessID;
			public int pcPriClassBase;
			public uint dwFlags;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szExeFile;
		};
	}
}
