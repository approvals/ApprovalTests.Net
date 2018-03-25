using NUnit.Framework;

namespace ApprovalTests.Tests.Persistence
{
    [TestFixture]
    public class WeatherTest
    {
        [Test]
        public void TestWeather()
        {
            Approvals.Verify(new WeatherLoader("KCASANDI69"));
        }
    }
}