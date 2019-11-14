using System;

namespace ApprovalTests.Namers
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        ReplacementTypeOrMember = nameof(EnvironmentSpecificCleanUp))]
    public class EnviromentSpecificCleanUp : IDisposable
    {
        public void Dispose()
        {
            NamerFactory.Clear();
        }
    }
}