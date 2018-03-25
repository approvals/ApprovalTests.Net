using System;

namespace ApprovalTests.Namers
{
    public class EnviromentSpecificCleanUp : IDisposable
    {
        public void Dispose()
        {
            NamerFactory.Clear();
        }
    }
}