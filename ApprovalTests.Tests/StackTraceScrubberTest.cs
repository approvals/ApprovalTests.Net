using NUnit.Framework;

namespace ApprovalTests.Tests
{
    [TestFixture]
    public class StackTraceScrubberTest
    {
        [Test]
        public void ScrubPath()
        {
            var scrubPaths = StackTraceScrubber.ScrubPaths(@"c:\temp\path.with-separators and spaces\result.txt");
            Assert.AreEqual(@"...\result.txt", scrubPaths);
        }
    }
}