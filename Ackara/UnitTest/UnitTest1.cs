using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApprovalTests.Reporters;
using ApprovalTests;

namespace Ackara.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public const string

            ODBC = "System.Data.Odbc",
            ExcelConnectionString = @"Dsn=Excel Files;dbq=C:\Users\Ackeem\OneDrive\Projects\Libraries\C#\ApprovalTests.Net\Ackara\Data\sampleData.xlsx";

        public TestContext TestContext { get; set; }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        [DataSource(ODBC, ExcelConnectionString, "Sheet1$", DataAccessMethod.Random)]
        public void TestMethod1()
        {
            var sample = Convert.ToString(TestContext.DataRow[0]);
            var table = TestContext.DataRow.Table;
            var idx = new DDTWriter("", TestContext).GetRowNumber();
            System.Diagnostics.Debug.WriteLine(idx + " " + sample);

            Approvals.Verify(new DDTWriter(sample, TestContext));
        }
    }
}
