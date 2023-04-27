using System.Diagnostics;

namespace ApprovalUtilities.Utilities;

public static class PathUtilities
{
    public static string GetDirectoryForCaller() =>
        GetDirectoryForCaller(1);

    public static string GetDirectoryForCaller(int callerStackDepth)
    {
        var stackFrame = new StackTrace(true).GetFrame(callerStackDepth + 1);
        return GetDirectoryForStackFrame(stackFrame);
    }

    public static string GetDirectoryForStackFrame(StackFrame stackFrame) =>
        new FileInfo(stackFrame.GetFileName()).Directory.FullName + Path.DirectorySeparatorChar;

    public static string ScrubPath(this string text, string path) =>
        text?.Replace(path, "..." + Path.DirectorySeparatorChar);

    public static string GetAdjacentFile(string relativePath) =>
        GetDirectoryForCaller(1) + relativePath;

    public static IEnumerable<string> LocateFileFromEnvironmentPath(string toFind)
    {
        var results = new List<string>();
        if (File.Exists(toFind))
        {
            results.Add(Path.GetFullPath(toFind));
        }
        if (OsUtils.IsUnixOs())
        {
            if (0 <= toFind.IndexOf(".exe"))
            {
                var trimmedToFind = toFind.Substring(0, toFind.Length - 4);
                results.AddRange(FindProgramOnPath(trimmedToFind));
            }
        }

        results.AddRange(FindProgramOnPath(toFind));
        return results.Distinct();
    }

    static IList<string> EnvironmentPaths;

    static IEnumerable<string> FindProgramOnPath(string programName)
    {
        if (EnvironmentPaths == null)
        {
            EnvironmentPaths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator).ToList();
            if (OsUtils.IsUnixOs())
            {
                // not sure why this path is not included in the environment variables
                // but couldn't find find p4merge without it.
                EnvironmentPaths.Add("/usr/local/bin");
            }
        }
        return EnvironmentPaths.Select(path => Path.Combine(path, programName)).Where(File.Exists);
    }
}