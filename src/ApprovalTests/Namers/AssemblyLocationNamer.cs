using System.IO;
using System.Linq;

namespace ApprovalTests.Namers
{
    public class AssemblyLocationNamer : UnitTestFrameworkNamer
    {
        private string AssemblyDirectory
        {
            get
            {
                var currentTestCaller = Approvals.CurrentCaller.NonLambdaCallers
                    .First(c =>
                        c.Class.Namespace.StartsWith("ApprovalTests") == false &&
                        c.Class.Namespace.StartsWith("ApprovalUtilities") == false);
                return Path.GetDirectoryName(currentTestCaller.Class.Assembly.Location);
            }
        }

        public override string SourcePath => Path.Combine(AssemblyDirectory, Subdirectory);
    }
}
