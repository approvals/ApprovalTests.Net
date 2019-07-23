using System.IO;
using System.Linq;
using System.Text;
using ApprovalTests.Core;
using ApprovalTests.Namers;

namespace ApprovalTests.Reporters
{
    public class MachineSpecificReporter : IEnvironmentAwareReporter
    {
        public bool IsWorkingInThisEnvironment(string forFile)
        {
            var info = ApprovalsFilename.Parse(forFile);
            return info.IsMachineSpecific && info.ForApproved().IsEmptyFile();
        }

        public void Report(string approved, string received)
        {
            if (IsWorkingInThisEnvironment(approved))
            {
                var info = ApprovalsFilename.Parse(approved);
                var nearest = info.GetOtherMachineSpecificFiles().OrderByDescending(f => f.LastWriteTimeUtc).FirstOrDefault();
                if (nearest != null)
                {
                    var text = File.ReadAllText(nearest.FullName);
                    File.WriteAllText(approved, $"Copied from: {nearest.Name}\n{text}", Encoding.UTF8);
                }
            }
            DiffReporter.INSTANCE.Report(approved, received);
        }
    }
}