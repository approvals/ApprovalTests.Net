using ApprovalTests.Asp;
using CassiniDev;
using NUnit.Framework;

namespace ApprovalTests.Tests.Asp
{
    public abstract class ServerDependentTest : CassiniDevServer
    {
        private readonly string _applicationPath;

        protected ServerDependentTest(string applicationPath, int port)
        {
            _applicationPath = applicationPath;
            PortFactory.AspPort = PortFactory.MvcPort = port + 1;
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            StopServer();
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            StartServer(_applicationPath, PortFactory.AspPort, "/", "localhost");
        }
    }
}