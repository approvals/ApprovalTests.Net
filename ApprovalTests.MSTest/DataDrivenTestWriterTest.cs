using ApprovalTests.Reporters;
using ApprovalTests.Writers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace ApprovalTests.MSTest
{
    [TestClass]
    public class DataDrivenTestWriterTest
    {
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            string sampleDataFile = Assembly.GetExecutingAssembly().Location;
            for (int i = 0; i < 4; i++)
            {
                sampleDataFile = Path.GetDirectoryName(sampleDataFile);
            }
            sampleDataFile = Path.Combine(sampleDataFile, "ApprovalTests.MSTest", "SampleData", "data.xlsx");

            var directory = Path.GetDirectoryName(sampleDataFile);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            File.Copy(sampleDataFile, SampleFile, overwrite: true);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            if (File.Exists(SampleFile)) File.Delete(SampleFile);
        }

        [TestMethod]
        [Owner(Ackara)]
        [DataSource(ODBC, ExcelConnectionString, "Sheet1$", DataAccessMethod.Random)]
        public void GetReceivedFilename_returns_an_unique_filename_for_each_row_within_test_context_when_called()
        {
            // Arrange
            string rowNumber = Convert.ToString(TestContext.DataRow[0]);
            var expectedResult = String.Format("{0}[{1}].received.txt", TestContext.TestName, rowNumber);

            // Act
            var sut = new DataDrivenTestWriter(rowNumber, TestContext);
            string result = sut.GetReceivedFilename(TestContext.TestName);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [Owner(Ackara)]
        [DataSource(ODBC, ExcelConnectionString, "Sheet1$", DataAccessMethod.Random)]
        public void GetApprovalFilename_returns_an_unique_filename_for_each_row_within_test_context_when_called()
        {
            // Arrange
            string rowNumber = Convert.ToString(TestContext.DataRow[0]);
            var expectedResult = String.Format("{0}[{1}].approved.txt", TestContext.TestName, rowNumber);

            // Act
            var sut = new DataDrivenTestWriter(rowNumber, TestContext);
            string result = sut.GetApprovalFilename(TestContext.TestName);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [Owner(Ackara)]
        [UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
        [DataSource(ODBC, ExcelConnectionString, "Sheet1$", DataAccessMethod.Random)]
        public void DataDrivenTestWriterTest_demonstration()
        {
            string data = Convert.ToString(TestContext.DataRow[1]);
            Approvals.Verify(new DataDrivenTestWriter(data, TestContext));
        }

        #region Private Members

        private const string
            Ackara = "ackara.dev@outlook.com",

            ODBC = "System.Data.Odbc",
            SampleFile = @"C:\Users\Public\delete_this.approvalTests.xlsx",
            ExcelConnectionString = @"Dsn=Excel Files;dbq=" + SampleFile;

        #endregion Private Members
    }
}