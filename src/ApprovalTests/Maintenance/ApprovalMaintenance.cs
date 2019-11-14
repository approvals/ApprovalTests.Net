using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ApprovalUtilities.CallStack;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Maintenance
{
    public static class ApprovalMaintenance
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
            var list = FindAbandonedFiles(path, assembly);
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

        private static List<FileInfo> FindAbandonedFiles(string path, Assembly assembly)
        {
            var searchPattern = "*.approved.*";
            var approvedFile = Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories);
            return approvedFile.Select(f => new FileInfo(f))
                .Where(f => IsAbandoned(f, assembly))
                .ToList();
        }

        private static bool IsAbandoned(FileInfo approvedFile, Assembly assembly)
        {
            var rootTypes = assembly.GetTypes();
            var parts = approvedFile.Name.Split('.');
            var className = parts[0];
            var methodName = parts[1];
            var types = rootTypes
                .Where(t => t.Name == className);
            var methods = types.SelectMany(t => t.GetMethods())
                .Where(m => m.Name == methodName);
            if (methods.Any())
            {
                return false;
            }

            var nestedClassName = parts[1];
            var nestedMethodName = parts[2];
            var nestedTypes = rootTypes
                .Where(t => t.Name == className)
                .SelectMany(x => x.GetNestedTypes())
                .Where(t => t.Name == nestedClassName);
            methods = nestedTypes.SelectMany(t => t.GetMethods())
                .Where(m => m.Name == nestedMethodName);
            if (methods.Any())
            {
                return false;
            }

            return true;
        }

        public static void VerifyNoAbandonedFiles(params string[] ignore)
        {
            var path = PathUtilities.GetDirectoryForCaller(1);
            var assembly = new Caller().Methods.First().Module.Assembly;
            var files = FindAbandonedFiles(path, assembly)
                .Where(f => !ignore.Any(p => f.FullName.Contains(p)))
                .ToArray();
            if (files.Any())
            {
                throw new Exception("The following files have been abandoned:\n" + files.ToReadableString().Replace(",", "\n"));
            }
        }
    }
}