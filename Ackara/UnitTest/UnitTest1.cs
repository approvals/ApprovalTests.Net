using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApprovalTests.Reporters;
using ApprovalTests;

namespace Ackara.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [UseReporter(typeof(FileLauncherReporter))]
        public void TestMethod1()
        {
            
        }
    }
}
