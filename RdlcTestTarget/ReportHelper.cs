using Microsoft.Reporting.WinForms;

namespace RdlcTestTarget
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
			string reportname = "RdlcTestTarget.InsultsReport.rdlc";
			reportViewer.LocalReport.ReportEmbeddedResource = reportname;
		}
	}
}