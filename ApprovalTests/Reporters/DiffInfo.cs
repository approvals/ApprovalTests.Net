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
            this.DiffProgram = diffProgram;
            this.Parameters = parameters;
            this.FileTypes = fileTypes;
        }

        public DiffInfo(string diffProgram, Func<IEnumerable<string>> fileTypes) : this(diffProgram, GenericDiffReporter.DEFAULT_ARGUMENT_FORMAT, fileTypes)
        {
            
        }

    }
}