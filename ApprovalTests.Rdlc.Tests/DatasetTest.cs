using System;
using System.Data;
using System.Reflection;
using ApprovalTests.Namers;
using ApprovalTests.Persistence.DataSets;
using ApprovalTests.RdlcReports;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;
using ReportingDemo;

namespace ApprovalTests.MachineSpecific.Tests.Persistence.Datasets
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter), typeof(AllFailingTestsClipboardReporter))]
    public class DatasetTest
    {
        private const string ReportName = "ReportingDemo.InsultsReport.rdlc";

        [SetUp]
        public void Setup()
        {
            ApprovalResults.UniqueForMachineName();
        }

        [Test]
        public void TestExternalImage()
        {
            RdlcApprovals.VerifyReport("ReportingDemo.ExternalImage.rdlc", GetDefaultData());
        }

        [Test]
        public void TestSimpleReportWith1Dataset()
        {
            RdlcApprovals.VerifyReport(ReportName, GetDefaultData());
        }

        [Test]
        public void TestSimpleReportWithDatasetInAssembly()
        {
            RdlcApprovals.VerifyReport(ReportName, "Model", GetDefaultData());
        }

        [Test]
        public void TestReport()
        {
            RdlcApprovals.VerifyReport(ReportName, GetAssembly(), Tuple.Create("Model", GetDefaultData()));
        }

        [Test]
        public void TestReportWithDataPair()
        {
            RdlcApprovals.VerifyReport(ReportName, GetAssembly(), new DataPairs {{"Model", GetDefaultData()}});
        }

        [Test]
        public void TestSimpleReportExplicit()
        {
            RdlcApprovals.VerifyReport(ReportName, GetAssembly(), "Model", GetDefaultData());
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

        private static DataTable GetDefaultData()
        {
            return new InsultsDataSet.InsultsDataTable().AddTestDataRows(1);
        }

        private static Assembly GetAssembly()
        {
            return typeof(InsultsDataSet).Assembly;
        }
    }
}