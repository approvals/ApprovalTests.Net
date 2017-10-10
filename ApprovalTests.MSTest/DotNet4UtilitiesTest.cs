using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.MSTest
{
    [TestClass]
    public class DotNet4UtilitiesTest
    {

        [TestMethod]
        [UseReporter(typeof(FileLauncherReporter))]
        public void TestPath()
        {
            string[] paths = new string[] { @"M:\Somepaththatdoesntexist\", @"C:\Windows\system32", @"Z:\Othernonpath" };
            Assert.AreEqual(@"C:\Windows\system32\notepad.exe", DotNet4Utilities.GetFirstWorking("notepad.exe", paths));
        }
    }
}