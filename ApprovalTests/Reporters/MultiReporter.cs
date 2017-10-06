using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class MultiReporter : IEnvironmentAwareReporter, IApprovalReporterWithCleanUp
    {
        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return Reporters.OfType<IEnvironmentAwareReporter>().Any(r => r.IsWorkingInThisEnvironment(forFile));
        }

        public void CleanUp(string approved, string received)
        {
            foreach (var cleaner in Reporters.OfType<IApprovalReporterWithCleanUp>())
            {
                cleaner.CleanUp(approved, received);
            }
        }

        public IEnumerable<IApprovalFailureReporter> Reporters { get; }

        public MultiReporter(params IApprovalFailureReporter[] reporters)
        {
            this.Reporters = reporters;
        }

        public MultiReporter(IEnumerable<IApprovalFailureReporter> reporters)
        {
            this.Reporters = reporters;
        }

        public virtual void Report(string approved, string received)
        {
            Exception lastThrown = null;

            foreach (var reporter in Reporters)
            {
                try
                {
                    reporter.Report(approved, received);
                }
                catch (Exception e)
                {
                    lastThrown = e;
                }
            }
            if (lastThrown != null)
            {
                throw lastThrown;
            }
        }
    }
}