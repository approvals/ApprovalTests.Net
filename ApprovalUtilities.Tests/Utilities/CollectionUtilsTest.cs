using System.Collections.Generic;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalUtilities.Tests.Utilities
{
    public class CollectionUtilsTest
    {
        [Test]
        public void TestDictionary()
        {
            var d = new Dictionary<int, string> {{1, "one"}};
            Assert.AreEqual("one", d.GetValueOrDefault(1));
            Assert.AreEqual(null, d.GetValueOrDefault(2));

        }
    }
}
