using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ApprovalTests.Utilities
{
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
                Int64.TryParse(File.ReadAllText(file), out count);
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

        public static void ResetAndLaunch(string javaPath, string counterDisplay)
        {
            Reset();
            Launch(javaPath, counterDisplay);
        }

        public static void Launch(string java, string jar)
        {
            Task.Factory.StartNew(
                () => LaunchProgram(java,
                    $"-jar {jar} {Path.GetFullPath(passFile)} {Path.GetFullPath(failFile)}"));
        }


        private static void LaunchProgram(string program, string arguments)
        {
            try
            {
                Console.WriteLine("Starting: " + program + " " + arguments);
                Process.Start(program, arguments);
            }
            catch (Win32Exception e)
            {
                throw new Exception(
                    String.Format("Unable to launch: {0} with arguments {1}\nError Message: {2}",
                        program,
                        arguments,
                        e.Message),
                    e);
            }
        }

        public static void Track(Action test)
        {
            try
            {
                test();
                TestCounter.IncrementSuccess();
            }
            catch (Exception)
            {
                TestCounter.IncrementFailure();
                throw;
            }
        }
    }
}