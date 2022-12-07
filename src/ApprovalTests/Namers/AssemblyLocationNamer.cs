using System.IO;
using System.Linq;

namespace ApprovalTests.Namers;

public class AssemblyLocationNamer : UnitTestFrameworkNamer
{
    static string AssemblyDirectory
    {
        get
        {
            var currentTestCaller = Approvals.CurrentCaller.NonLambdaCallers
                .First(c =>
                {
                    var classNamespace = c.Class.Namespace ?? "";
                    return !classNamespace.StartsWith("ApprovalTests") &&
                           !classNamespace.StartsWith("ApprovalUtilities");
                });
            return Path.GetDirectoryName(currentTestCaller.Class.Assembly.Location);
        }
    }

    public override string SourcePath => Path.Combine(AssemblyDirectory, Subdirectory);
}