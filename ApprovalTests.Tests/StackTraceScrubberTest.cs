using ApprovalTests.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
    [TestFixture]
    public class StackTraceScrubberTest
    {
        [Test]
        public void TestDashedPath()
        {
            const string Path = @"C:\code\ApprovalTests - Net\Persistence\Datasets\DatasetTest.cs";
            Assert.AreEqual("...\\DatasetTest.cs", StackTraceScrubber.ScrubPaths(Path));
        }

        [Test]
        public void TestDashedPathOnMac()
        {
            const string Path = @"/Users/approver/code/ApprovalTests - Net/Persistence/Datasets/DatasetTest.cs";
            Assert.AreEqual(".../DatasetTest.cs", StackTraceScrubber.ScrubPaths(Path));
        }

        [Test]
        public void TestKeyValuePair()
        {
            const string Value = @"name: File.foo";
            Assert.AreEqual(Value, StackTraceScrubber.ScrubPaths(Value));
        }
    }
}