using System;
using System.Data;
using System.Reflection;
using ApprovalTests.Namers;
using ApprovalTests.Persistence.DataSets;
using ApprovalTests.RdlcReports;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportingDemo;

namespace ApprovalTests.MachineSpecific.Tests.Persistence.Datasets
{
    [TestClass]
//    [UseReporter(typeof (AllFailingTestsClipboardReporter))]
    public class DatasetTest
    {
        private const string ReportName = "ReportingDemo.InsultsReport.rdlc";

        [TestMethod]
        public void TestExtrenalImage()
        {
            RdlcApprovals.VerifyReport("ReportingDemo.ExternalImage.rdlc", GetDefaultData());
        }

        [TestMethod]
        public void TestSimpleReportWith1Dataset()
        {
            RdlcApprovals.VerifyReport(ReportName, GetDefaultData());
        }

        [TestMethod]
        public void TestSimpleReportWithDatasetInAssembly()
        {
            RdlcApprovals.VerifyReport(ReportName, "Model", GetDefaultData());
        }

        [TestMethod]
        public void TestReport()
        {
            RdlcApprovals.VerifyReport(ReportName, GetAssembly(), Tuple.Create("Model", GetDefaultData()));
        }

        [TestMethod]
        public void TestReportWithDataPair()
        {
            RdlcApprovals.VerifyReport(ReportName, GetAssembly(), new DataPairs {{"Model", GetDefaultData()}});
        }

        [TestMethod]
        public void TestSimpleReportExplict()
        {
            RdlcApprovals.VerifyReport(ReportName, GetAssembly(), "Model", GetDefaultData());
        }

        [TestMethod]
        public void TestDataSourceNames()
        {
            NamerFactory.Clear();
            var exception =
                ExceptionUtilities.GetException(
                    () => RdlcApprovals.VerifyReport(ReportName, GetAssembly(), "purposelyMisspelt", GetDefaultData()));
            Approvals.Verify(exception.Message);
        }

        [TestInitialize]
        public void NamerSetUp()
        {
            ApprovalResults.UniqueForMachineName();
        }

        private static DataTable GetDefaultData()
        {
            return new InsultsDataSet.InsultsDataTable().AddTestDataRows(1);
        }

        private static Assembly GetAssembly()
        {
            return typeof (InsultsDataSet).Assembly;
        }
    }
}