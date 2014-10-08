using ApprovalTests.Namers;
using NUnit.Framework;

namespace ApprovalTests.Tests.Namer
{
    [TestFixture]
    public class ApprovalResultsTest
    {
        [Test]
        public void TestEasyNames()
        {
            Assert.AreEqual("Windows 7", ApprovalResults.TransformEasyOsName("Microsoft Windows 7 Professional N"));
        }
    }

    [TestFixture]
    public class AdditionalInformationTests
    {
        [Test]
        public void ExitsBeforeNamerIsCalled()
        {
            using (ApprovalResults.UniqueForMachineName())
            {
            }
        }

        [Test]
        public void WithoutExtraInfo()
        {
            var name = Approvals.GetDefaultNamer().Name;
            Assert.AreEqual("AdditionalInformationTests.WithoutExtraInfo", name);
        }

        [Test]
        public void WithScenarioData()
        {
            using (ApprovalResults.ForScenario("scenarioname"))
            {
                var name = Approvals.GetDefaultNamer().Name;
                Assert.AreEqual("AdditionalInformationTests.WithScenarioData.ForScenario.scenarioname", name);
            }
        }


        [Test]
        public void WithScenarioDataScrubsInvalidChars()
        {
            using (ApprovalResults.ForScenario("invalid/chars"))
            {
                var name = Approvals.GetDefaultNamer().Name;
                Assert.AreEqual(
                    "AdditionalInformationTests.WithScenarioDataScrubsInvalidChars.ForScenario.invalid_chars", name);
            }
        }

        [Test]
        [TestCase("foo", "bar")]
        public void WithMultiplePartScenarioData(string a, string b)
        {
            using (ApprovalResults.ForScenario(a, b))
            {
                var name = Approvals.GetDefaultNamer().Name;
                Assert.AreEqual("AdditionalInformationTests.WithMultiplePartScenarioData.ForScenario.foo.bar", name);
            }
        }

        [Test]
        public void TestMultipleNames()
        {
            using (ApprovalResults.ForScenario("scenario"))
            {
                using (ApprovalResults.ForScenario("machineName"))
                {
                    string name = Approvals.GetDefaultNamer().Name;
                    Assert.AreEqual(name,
                        "AdditionalInformationTests.TestMultipleNames.ForScenario.scenario.ForScenario.machineName");
                }
            }
        }
    }
}