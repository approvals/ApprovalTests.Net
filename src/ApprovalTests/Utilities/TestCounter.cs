using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Utilities;

public class TestCounter
{
    public const string passFile = "fakeit.counter.pass.txt";
    public const string failFile = "fakeit.counter.fail.txt";

    public static void IncrementSuccess()
    {
        Increment(passFile);
    }

    public static void IncrementFailure()
    {
        Increment(failFile);
    }

    public static void Increment(string file)
    {
        long count = 0;
        if (File.Exists(file))
        {
            long.TryParse(File.ReadAllText(file), out count);
        }
        count++;
        File.WriteAllText(file, "" + count);
    }

    public static void Reset()
    {
        if (File.Exists(passFile))
        {
            File.Delete(passFile);
        }
        if (File.Exists(failFile))
        {
            File.Delete(failFile);
        }
    }

    public static void ResetAndLaunch(string counterDisplayJar)
    {
        var javaPath = PathUtilities.LocateFileFromEnvironmentPath("javaw.exe").FirstOrDefault();
        ResetAndLaunch(javaPath, counterDisplayJar);
    }

    public static void ResetAndLaunch(string javaPath, string counterDisplayJar)
    {
        Reset();
        Launch(javaPath, counterDisplayJar);
    }

    public static void Launch(string java, string jar)
    {
        Task.Factory.StartNew(
            () => LaunchProgram(java,
                $"-jar {jar} {Path.GetFullPath(passFile)} {Path.GetFullPath(failFile)}"));
    }


    static void LaunchProgram(string program, string arguments)
    {
        try
        {
            Console.WriteLine("Starting: " + program + " " + arguments);
            Process.Start(program, arguments);
        }
        catch (Win32Exception e)
        {
            throw new Exception(
                $"Unable to launch: {program} with arguments {arguments}\nError Message: {e.Message}",
                e);
        }
    }

    public static void Track(Action test)
    {
        try
        {
            test();
            IncrementSuccess();
        }
        catch (Exception)
        {
            IncrementFailure();
            throw;
        }
    }
}