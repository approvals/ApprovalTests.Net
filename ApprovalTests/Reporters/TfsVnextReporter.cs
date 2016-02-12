using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class TfsVnextReporter : IEnvironmentAwareReporter
    {
      public static readonly TfsVnextReporter INSTANCE = new TfsVnextReporter();

      public void Report(string approved, string received)
      {
          // does nothing
      }

      public bool IsWorkingInThisEnvironment(string forFile)
      {
          return Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT") != null;
      }
  }
}
