using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
    public class GenericDiffReporter : IEnvironmentAwareReporter
    {
        public const string DEFAULT_ARGUMENT_FORMAT = "\"{0}\" \"{1}\"";
        public static readonly string[] TEXT_FILE_TYPES = new[] {".txt", ".csv", ".htm", ".html", ".xml", ".eml",".cs",".css"};
				public static readonly string[] IMAGE_FILE_TYPES = { ".png", ".gif", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };

        protected string arguments;
        protected string diffProgram;
        protected string diffProgramNotFoundMessage;
        protected string[] fileTypes = TEXT_FILE_TYPES;

        public GenericDiffReporter(string diffProgram, string diffProgramNotFoundMessage)
            : this(diffProgram, DEFAULT_ARGUMENT_FORMAT, diffProgramNotFoundMessage)
        {
        }

        public GenericDiffReporter(string diffProgram, string argumentsFormat, string diffProgramNotFoundMessage)
            : this(diffProgram, argumentsFormat, diffProgramNotFoundMessage, TEXT_FILE_TYPES)
        {
        }

        public GenericDiffReporter(string diffProgram, string argumentsFormat, string diffProgramNotFoundMessage,
                                   string[] allowedFileTypes)
        {
            if (diffProgram == null)
            {
                throw new NullReferenceException(
                    @"Illegal arguments for {0} (diffProgam, argumentsFormat, diffProgramNotFoundMessage)
Recieved {0} ({1}, {2}, {3})"
                        .FormatWith(GetType().Name, diffProgram, argumentsFormat, diffProgramNotFoundMessage));
            }

            this.diffProgram = GetActualProgramFile(diffProgram);
            arguments = argumentsFormat;
            this.diffProgramNotFoundMessage = diffProgramNotFoundMessage;
            fileTypes = allowedFileTypes;
        }

    	public static string GetActualProgramFile(string fullPath)
    	{
				if (File.Exists(fullPath))
				{
					return fullPath;
				}
				var toFind = Path.GetFileName(fullPath);
				var output = PathUtilities.LocateFileFromEnviormentPath(toFind).FirstOrDefault();
    		return string.IsNullOrEmpty(output) ? fullPath: output;
    	}

    	
        public virtual void Report(string approved, string received)
        {
            if (!File.Exists(diffProgram))
            {
                throw new Exception(diffProgramNotFoundMessage);
            }
            FileUtilities.EnsureFileExists(approved);
            DiffReporter.Launch(GetLaunchArguments(approved, received));
        }

		

	    public virtual bool IsWorkingInThisEnvironment(string forFile)
        {
            return File.Exists(diffProgram) && IsFileOneOf(forFile, fileTypes);
        }


        public LaunchArgs GetLaunchArguments(string approved, string received)
        {
            return new LaunchArgs(diffProgram, arguments.FormatWith(received, approved));
        }


        public static bool IsTextFile(string forFile)
        {
            return IsFileOneOf(forFile, TEXT_FILE_TYPES);
        }

        public static bool IsFileOneOf(string forFile, string[] filetypes)
        {
            return filetypes.Any(ext => forFile.EndsWith(ext));
        }
    }
}