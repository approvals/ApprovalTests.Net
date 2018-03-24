using Microsoft.Reporting.WinForms;

namespace ReportingDemo
{
    public class ReportHelper
    {
        public static ReportViewer CreateReport()
        {
            var reportViewer = new ReportViewer();
            AddReport(reportViewer);

            return reportViewer;
        }

        public static void AddReport(ReportViewer reportViewer)
        {
            string reportname = "ReportingDemo.InsultsReport.rdlc";
            reportViewer.LocalReport.ReportEmbeddedResource = reportname;
        }
    }
}