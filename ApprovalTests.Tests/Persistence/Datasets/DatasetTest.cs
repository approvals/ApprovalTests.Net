using System;
using System.Data;
using System.Reflection;
using ApprovalTests.Namers;
using ApprovalTests.Persistence.DataSets;
using ApprovalTests.RdlcReports;
using ApprovalTests.Reporters;
using ApprovalTests.Tests.Asp;
using ApprovalUtilities.Utilities;
using Asp.Net.Demo;
using NUnit.Framework;
using ReportingDemo;

namespace ApprovalTests.Tests.Persistence.Datasets
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter), typeof(AllFailingTestsClipboardReporter))]
    public class DatasetTest : ServerDependentTest
    {
        private const string ReportName = "ReportingDemo.InsultsReport.rdlc";

        public DatasetTest()
            : base(Global.Directory, 1358)
        {
        }

        [SetUp]
        public void NamerSetUp()
        {
            ApprovalResults.UniqueForMachineName();
        }

        [Test]
        public void TestDataSourceNames()
        {
            NamerFactory.Clear();
            var exception =
                ExceptionUtilities.GetException(
                    () => RdlcApprovals.VerifyReport(ReportName, GetAssembly(), "purposelyMisspelt", GetDefaultData()));
            Approvals.Verify(exception.Message);
        }

        [Test]
        public void TestExtrenalImage()
        {
            VerifyDefaultReport("ReportingDemo.ExternalImage.rdlc");
        }

        [Test]
        public void TestReport()
        {
            RdlcApprovals.VerifyReport(ReportName, GetAssembly(), Tuple.Create("Model", GetDefaultData()));
        }

        [Test]
        public void TestReportWithDataPair()
        {
            RdlcApprovals.VerifyReport(ReportName, GetAssembly(), new DataPairs { { "Model", GetDefaultData() } });
        }

        [Test]
        public void TestSimpleReportExplict()
        {
            RdlcApprovals.VerifyReport(ReportName, GetAssembly(), "Model", GetDefaultData());
        }

        [Test]
        public void TestSimpleReportWith1Dataset()
        {
            VerifyDefaultReport(ReportName);
        }

        [Test]
        public void TestSimpleReportWithDatasetInAssembly()
        {
            RdlcApprovals.VerifyReport(ReportName, "Model", GetDefaultData());
        }

        private static Assembly GetAssembly()
        {
            return typeof(InsultsDataSet).Assembly;
        }

        private static DataTable GetDefaultData()
        {
            return new InsultsDataSet.InsultsDataTable().AddTestDataRows();
        }

        private static void VerifyDefaultReport(string name)
        {
            using (var data = GetDefaultData())
            {
                RdlcApprovals.VerifyReport(name, data);
            }
        }
    }
}