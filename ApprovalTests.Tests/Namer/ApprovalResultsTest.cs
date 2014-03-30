using System;
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
        public void WtihScenarioData()
        {
            using (ApprovalResults.ForScenario("scenarioname"))
            {

                var name = Approvals.GetDefaultNamer().Name;
                Assert.AreEqual("AdditionalInformationTests.WtihScenarioData.ForScenario.scenarioname", name);
            }
        }

        [Test]
        public void WtihScenarioDataReplacesSpacesWithUnderscore()
        {
            using (ApprovalResults.ForScenario("Scenario with spaces"))
            {

                var name = Approvals.GetDefaultNamer().Name;
                Assert.AreEqual(
                    "AdditionalInformationTests.WtihScenarioDataReplacesSpacesWithUnderscore.ForScenario.Scenario_with_spaces",
                    name);
            }
        }

        [Test]
        public void WtihScenarioDataScrubsInvalidChars()
        {
            using (ApprovalResults.ForScenario("invalid/chars"))
            {

                var name = Approvals.GetDefaultNamer().Name;
                Assert.AreEqual(
                    "AdditionalInformationTests.WtihScenarioDataScrubsInvalidChars.ForScenario.invalid_chars", name);
            }
        }

        [Test]
        public void WithMultiplePartScenarioData()
        {
            using (ApprovalResults.ForScenario("foo", "bar"))
            {
                var name = Approvals.GetDefaultNamer().Name;
                Assert.AreEqual("AdditionalInformationTests.WithMultiplePartScenarioData.ForScenario.foo.bar", name);
            }
        }
    }
}