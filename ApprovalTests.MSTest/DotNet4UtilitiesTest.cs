using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using ApprovalTests;
using ApprovalTests.Reporters;
namespace ApprovalTests.Tests.Reporters
{
    [TestClass]
    public class DotNet4UtilitiesTest
    {
        [TestMethod]
        [ApprovalTests.Reporters.UseReporter(typeof(FileLauncherReporter))]
        public void TestPath()
        {
            string[] paths = new string[] { @"M:\Somepaththatdoesntexist\", @"C:\Windows\system32", @"Z:\Othernonpath" };
            Assert.AreEqual(@"C:\Windows\system32\notepad.exe", DotNet4Utilities.GetFirstWorking("notepad.exe", paths));
        }
    }
}
