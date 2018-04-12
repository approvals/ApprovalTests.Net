using System.Collections.Generic;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalUtilities.Tests.Utilities
{
    [TestClass]
    public class CollectionUtilsTest
    {
        [TestMethod]
        public void TestDictionary()
        {
            var d = new Dictionary<int, string> {{1, "one"}};
            Assert.AreEqual("one", d.GetValueOrDefault(1));
            Assert.AreEqual(null, d.GetValueOrDefault(2));

        }
    }
}