using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ApprovalUtilities.CallStack;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Maintenance
{
	public class ApprovalMaintenance
	{
		/// <summary>
		/// ** Warning : use at your own risk **
		/// Deletes any files that may have been abandoned.
		/// </summary>
		/// <returns> List of deleted files</returns>
		public static IEnumerable<FileInfo> CleanUpAbandonedFiles()
		{
			var assembly = new Caller().Methods.First().Module.Assembly; 
			var path = PathUtilities.GetDirectoryForCaller(1);
			var list = ApprovalMaintenance.FindAbandonedFiles(path,assembly);
			foreach (var fileInfo in list)
			{
				fileInfo.Delete();
			}
			return list;
		}
		public static IEnumerable<FileInfo> FindAbandonedFiles(string path)
		{
			var assembly = new Caller().Methods.First().Module.Assembly;
			return FindAbandonedFiles(path, assembly);
		}

		private static IEnumerable<FileInfo> FindAbandonedFiles(string path, Assembly assembly)
		{
			string searchPattern = "*.approved.*";
			var approvedFile = Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories);
			return approvedFile.Select(f => new FileInfo(f)).Where(f => IsAbandoned(f, assembly
				                                                            )).ToArray();
		}

		private static bool IsAbandoned(FileInfo approvedFile, Assembly assembly)
		{
			var parts = approvedFile.Name.Split('.');
			var className = parts[0];
			var methodName = parts[1];
			var types = assembly.GetTypes().Where(t => t.Name == className);
			var methods = types.SelectMany(t => t.GetMethods()).Where(m => m.Name == methodName);
			return !methods.Any();
		}

		
		public static void VerifyNoAbandonedFiles()
		{
			var path = PathUtilities.GetDirectoryForCaller(1);
			var assembly = new Caller().Methods.First().Module.Assembly;
			var files = FindAbandonedFiles(path, assembly);
			if (files.Any())
			{
				throw new Exception("The following files have been abandoned:\r\n" + files.ToReadableString().Replace(",","\r\n"));
			}
		}
	}
}