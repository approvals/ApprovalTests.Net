using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;
using EmptyFiles;

namespace ApprovalTests.Reporters
{
    public class GenericDiffReporter : IEnvironmentAwareReporter
    {
        public const string DEFAULT_ARGUMENT_FORMAT = "{0} {1}";

        public static IEnumerable<string> GetTextAndImageFileTypes()
        {
            var all = new HashSet<string>();
            all.AddAll(Extensions.TextExtensions);
            all.AddAll(AllFiles.ImageExtensions);
            return all;
        }

        protected string arguments;
        protected string originalDiffProgram;
        protected string actualDiffProgram;
        protected string diffProgramNotFoundMessage;
        protected Func<IEnumerable<string>> fileTypes;

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = "EmptyFiles.Extensions.TextExtensions")]
        public static HashSet<string> GetTextFileTypes()
        {
            return new HashSet<string>(Extensions.TextExtensions);
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = "EmptyFiles.AllFiles.ImageExtensions")]
        public static HashSet<string> GetImageFileTypes()
        {
            return new HashSet<string>(AllFiles.ImageExtensions);
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = "EmptyFiles.Extensions.AddTextExtensions")]
        public static void RegisterTextFileTypes(params string[] extensionsWithDots)
        {
            Extensions.AddTextExtensions(extensionsWithDots);
        }

        [ObsoleteEx(
            TreatAsErrorFromVersion = "4.0",
            ReplacementTypeOrMember = "EmptyFiles.AllFiles.UseFile")]
        public static void RegisterImageFileTypes(params string[] extensionsWithDots)
        {
            throw new NotImplementedException();
        }

        public GenericDiffReporter(string diffProgram)
            : this(diffProgram, DEFAULT_ARGUMENT_FORMAT, $"Couldn't find: {diffProgram}")
        {
        }

        public GenericDiffReporter(string diffProgram, string diffProgramNotFoundMessage)
            : this(diffProgram, DEFAULT_ARGUMENT_FORMAT, diffProgramNotFoundMessage)
        {
        }

        public GenericDiffReporter(string diffProgram, string argumentsFormat, string diffProgramNotFoundMessage)
            : this(diffProgram, argumentsFormat, diffProgramNotFoundMessage, () => Extensions.TextExtensions)
        {
        }

        public GenericDiffReporter(string diffProgram, string argumentsFormat, string diffProgramNotFoundMessage,
            Func<IEnumerable<string>> allowedFileTypes)
        {
            if (diffProgram == null)
            {
                throw new NullReferenceException(
                    string.Format(@"Illegal arguments for {0} (diffProgram, argumentsFormat, diffProgramNotFoundMessage)
Received {0} ({1}, {2}, {3})", GetType().Name, diffProgram, argumentsFormat, diffProgramNotFoundMessage));
            }

            originalDiffProgram = diffProgram;
            arguments = argumentsFormat;
            this.diffProgramNotFoundMessage = diffProgramNotFoundMessage;
            fileTypes = allowedFileTypes;
        }

        protected GenericDiffReporter(DiffInfo info) : this(info.DiffProgram, info.Parameters, $"Unable to find program at {info.DiffProgram}", info.FileTypes)
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
            return string.IsNullOrEmpty(output) ? fullPath : output;
        }

        public string GetDiffProgram()
        {
            return actualDiffProgram ??= GetActualProgramFile(originalDiffProgram);
        }

        public virtual void Report(string approved, string received)
        {
            if (!File.Exists(GetDiffProgram()))
            {
                throw new Exception(diffProgramNotFoundMessage);
            }

            EnsureFileExists(approved);
            Launch(GetLaunchArguments(approved, received));
        }

        public static void EnsureFileExists(string approved)
        {
            if (!File.Exists(approved))
            {
                if (AllFiles.TryGetPathFor(approved, out var emptyFile))
                {
                    File.Copy(emptyFile, approved, true);
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
            return new LaunchArgs(GetDiffProgram(), string.Format(arguments, new[] {WrapPath(received), WrapPath(approved)}));
        }

        private string WrapPath(string path)
        {
            return '"' + path + '"';
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = "EmptyFiles.Extensions.IsText")]
        public static bool IsTextFile(string forFile)
        {
            return Extensions.IsText(forFile);
        }

        public static bool IsFileOneOf(string forFile, IEnumerable<string> fileTypes)
        {
            return fileTypes.Any(forFile.EndsWith);
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = nameof(Launch))]
        public static void LaunchAsync(LaunchArgs launchArgs)
        {
             Launch(launchArgs);
        }

        public static void Launch(LaunchArgs launchArgs)
        {
            try
            {
                Process.Start(launchArgs.Program, launchArgs.Arguments);
            }
            catch (Win32Exception e)
            {
                throw new Exception($"Unable to launch: {launchArgs.Program} with arguments {launchArgs.Arguments}\nError Message: {e.Message}", e);
            }
        }
    }
}