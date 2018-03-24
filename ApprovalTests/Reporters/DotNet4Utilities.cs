using System;
using System.Collections.Generic;
using System.Linq;
using Alphaleonis.Win32.Filesystem;

namespace ApprovalTests.Reporters
{
    public class DotNet4Utilities
    {
        public static string GetFirstWorking(string path, string[] paths)
        {
            string fullPath = path;
            foreach (var p in paths)
            {
                fullPath = p + Path.DirectorySeparatorChar + path;
                if (File.Exists(fullPath))
                {
                    break;
                }
            }

            return fullPath;
        }

        public static string GetPathInProgramFilesX86(string path)
        {
            string[] paths = GetProgramFilesPaths();
            return GetFirstWorking(path, paths);
        }

        public static string[] GetProgramFilesPaths()
        {
            var paths = new HashSet<string>
            {
                Environment.GetEnvironmentVariable("ProgramFiles(x86)"),
                Environment.GetEnvironmentVariable("ProgramFiles"),
                Environment.GetEnvironmentVariable("ProgramW6432")
            };
            return paths.Where(p => p != null).ToArray();
        }
    }
}