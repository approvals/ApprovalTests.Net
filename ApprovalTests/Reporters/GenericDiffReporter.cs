using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alphaleonis.Win32.Filesystem;
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
    public class GenericDiffReporter : IEnvironmentAwareReporter
    {
        public const string DEFAULT_ARGUMENT_FORMAT = "{0} {1}";

        private static readonly HashSet<string> TEXT_FILE_TYPES = new HashSet<string>
        {
            ".txt",
            ".csv",
            ".htm",
            ".html",
            ".xml",
            ".eml",
            ".cs",
            ".css",
            ".sql",
            ".json"
        };

        private static readonly HashSet<string> IMAGE_FILE_TYPES = new HashSet<string>
        {
            ".png",
            ".gif",
            ".jpg",
            ".jpeg",
            ".bmp",
            ".tif",
            ".tiff"
        };

        protected string arguments;
        protected string originalDiffProgram;
        protected string actualDiffProgram;
        protected string diffProgramNotFoundMessage;
        protected Func<IEnumerable<string>> fileTypes = GetTextFileTypes;

        public static HashSet<string> GetTextFileTypes()
        {
            return TEXT_FILE_TYPES;
        }


        public static HashSet<string> GetImageFileTypes()
        {
            return IMAGE_FILE_TYPES;
        }

        public static void RegisterTextFileTypes(params string[] extensionsWithDots)
        {
            AssertDots(extensionsWithDots);
            TEXT_FILE_TYPES.AddAll(extensionsWithDots);
        }

        public static void RegisterImageFileTypes(params string[] extensionsWithDots)
        {
            AssertDots(extensionsWithDots);
            IMAGE_FILE_TYPES.AddAll(extensionsWithDots);
        }


        private static void AssertDots(string[] extensionsWithDots)
        {
            var wrong = extensionsWithDots.Where(s => !s.StartsWith("."));
            if (wrong.Count() > 0)
            {
                throw new ArgumentException("The following extensions don't start with dots: " +
                                            wrong.ToReadableString());
            }
        }

        public GenericDiffReporter(string diffProgram, string diffProgramNotFoundMessage)
            : this(diffProgram, DEFAULT_ARGUMENT_FORMAT, diffProgramNotFoundMessage)
        {
        }

        public GenericDiffReporter(string diffProgram, string argumentsFormat, string diffProgramNotFoundMessage)
            : this(diffProgram, argumentsFormat, diffProgramNotFoundMessage, GetTextFileTypes)
        {
        }

        public GenericDiffReporter(string diffProgram, string argumentsFormat, string diffProgramNotFoundMessage,
            Func<IEnumerable<string>> allowedFileTypes)
        {
            if (diffProgram == null)
            {
                throw new NullReferenceException(
                    @"Illegal arguments for {0} (diffProgam, argumentsFormat, diffProgramNotFoundMessage)
Recieved {0} ({1}, {2}, {3})"
                        .FormatWith(GetType().Name, diffProgram, argumentsFormat, diffProgramNotFoundMessage));
            }

            this.originalDiffProgram = diffProgram;
            this.arguments = argumentsFormat;
            this.diffProgramNotFoundMessage = diffProgramNotFoundMessage;
            fileTypes = allowedFileTypes;
        }

        protected GenericDiffReporter(DiffInfo info) : this(info.DiffProgram, info.Parameters, "Unable to find program at {0}".FormatWith( info.DiffProgram), info.FileTypes)
        {

        }

        public static string GetActualProgramFile(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                return fullPath;
            }
            var toFind = Path.GetFileName(fullPath);
            var output = PathUtilities.LocateFileFromEnvironmentPath(toFind).FirstOrDefault();
            return String.IsNullOrEmpty(output) ? fullPath : output;
        }

        public string GetDiffProgram()
        {
            if (actualDiffProgram == null)
            {
                actualDiffProgram = GetActualProgramFile(originalDiffProgram);
            }
            return actualDiffProgram;
        }

        public virtual void Report(string approved, string received)
        {
            if (!File.Exists(GetDiffProgram()))
            {
                throw new Exception(diffProgramNotFoundMessage);
            }
            EnsureFileExists(approved);
            LaunchAsync(GetLaunchArguments(approved, received));
        }

        public static void EnsureFileExists(string approved)
        {
            if (!File.Exists(approved))
            {
                var fileType = new FileInfo(approved).Extension;
                if (IMAGE_FILE_TYPES.Contains(fileType))
                {
                    using (var bitmap = new Bitmap(1, 1))
                    {
                        bitmap.SetResolution(96, 96);
                        bitmap.Save(approved);
                    }
                }
                else
                {
                    File.WriteAllText(approved, " ", Encoding.UTF8);
                }
                ReporterEvents.CreatedApprovedFile(approved);
            }
        }


        public virtual bool IsWorkingInThisEnvironment(string forFile)
        {
            return File.Exists(GetDiffProgram()) && IsValidFileType(forFile);
        }

        public bool IsValidFileType(string forFile)
        {
            return IsFileOneOf(forFile, fileTypes());
        }


        public LaunchArgs GetLaunchArguments(string approved, string received)
        {
            return new LaunchArgs(GetDiffProgram(), arguments.FormatWith( WrapPath(received), WrapPath(approved)));
        }

        private string WrapPath(string path)
        {
            return '"' + path + '"';
        }


        public static bool IsTextFile(string forFile)
        {
            return IsFileOneOf(forFile, GetTextFileTypes());
        }

        public static bool IsFileOneOf(string forFile, IEnumerable<string> filetypes)
        {
            return filetypes.Any(ext => forFile.EndsWith(ext));
        }

        public static void LaunchAsync(LaunchArgs launchArgs)
        {
            if (IsMsTest())
            {
                Launch(launchArgs, true);
            }
            else
            {
                Task.Factory.StartNew(() => Launch(launchArgs, false));
            }
        }

        private static bool IsMsTest()
        {
           return MsTestReporter.INSTANCE.IsFrameworkUsed();
        }

        private static void Launch(LaunchArgs launchArgs, bool waitForExit)
        {
            try
            {
                Process process = Process.Start(launchArgs.Program, launchArgs.Arguments);
                if (waitForExit)
                {
                    process.WaitForExit();
                }
            }
            catch (Win32Exception e)
            {
                throw new Exception(
                    "Unable to launch: {0} with arguments {1}\nError Message: {2}".FormatWith(
                        launchArgs.Program,
                        launchArgs.Arguments,
                        e.Message),
                    e);
            }
        }
    }
}