using System;
using System.Collections.Generic;

namespace ApprovalTests.Reporters
{
    public class DiffInfo
    {
        public Func<IEnumerable<string>> FileTypes { get; set; }
        public string DiffProgram { get; set; }
        public string Parameters { get; set; }

        public DiffInfo(string diffProgram, string parameters, Func<IEnumerable<string>> fileTypes)
        {
            this.DiffProgram = ResolveWindowsPath(diffProgram);
            this.Parameters = parameters;
            this.FileTypes = fileTypes;
        }

        private static string ResolveWindowsPath(string diffProgram)
        {
            var tag = "{ProgramFiles}";
            if (diffProgram.StartsWith(tag))
            {
                diffProgram = DotNet4Utilities.GetPathInProgramFilesX86(diffProgram.Substring(tag.Length));
            }
            return diffProgram;
        }

        public DiffInfo(string diffProgram, Func<IEnumerable<string>> fileTypes) : this(diffProgram, GenericDiffReporter.DEFAULT_ARGUMENT_FORMAT, fileTypes)
        {

        }

    }
}