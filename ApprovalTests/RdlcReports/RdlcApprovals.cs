using System;
using System.Collections.Generic;
using System.Reflection;
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;
using Microsoft.Reporting.WinForms;

namespace ApprovalTests.RdlcReports
{
	public class RdlcApprovals
	{
		public static void VerifyReport(string reportname, object data)
		{
			Action<ReportDataSourceCollection, IList<string>> populateDataSources =
				(ds, validNames) =>
				{
					if (validNames.Count != 1)
					{
						throw new Exception(
							"The Cannot use a 'default' Datasource Name for {0},\r\nLegal Matches are: {1}"
								.FormatWith(reportname, validNames.ToReadableString()));
					}
					ds.Add(new ReportDataSource(validNames[0], data));
				};
			VerifyRdlcReport(reportname, data.GetType().Assembly, populateDataSources);
		}

		public static void VerifyReport(string reportname, string datasourceName, object data)
		{
			VerifyReport(reportname, data.GetType().Assembly, datasourceName, data);
		}

		public static void VerifyReport(string reportname, Assembly assembly, string datasourceName, object data)
		{
			VerifyReport(reportname, assembly, Tuple.Create(datasourceName, data));
		}

		public static void VerifyReport<T>(string reportname, Assembly assembly, params Tuple<string, T>[] dataInfo)
		{
			Action<ReportDataSourceCollection, IList<string>> populateDataSources =
				(ds, validNames) =>
				{
					foreach (var info in dataInfo)
					{
						if (!validNames.Contains(info.Item1))
						{
							throw new Exception(
								"The Datasource Name '{0}'\r\nis not a legal match for {1},\r\nLegal Matches are: {2}"
									.FormatWith(info.Item1, reportname, validNames.ToReadableString()));
						}

						ds.Add(new ReportDataSource(info.Item1, info.Item2));
					}
				};
			VerifyRdlcReport(reportname, assembly, populateDataSources);
		}
		public static void VerifyReport(string reportname, Assembly assembly, IDictionary<string,object> dataPairs)
		{
			Action<ReportDataSourceCollection, IList<string>> populateDataSources =
				(ds, validNames) =>
				{
					foreach (var info in dataPairs)
					{
						if (!validNames.Contains(info.Key))
						{
							throw new Exception(
								"The Datasource Name '{0}'\r\nis not a legal match for {1},\r\nLegal Matches are: {2}"
									.FormatWith(info.Value, reportname, validNames.ToReadableString()));
						}

						ds.Add(new ReportDataSource(info.Key, info.Value));
					}
				};
			VerifyRdlcReport(reportname, assembly, populateDataSources);
		}

		public static void VerifyRdlcReport(string reportname, Assembly assembly,
																				Action<ReportDataSourceCollection, IList<string>> populateDataSources)
		{
			const string warning =
				@"Please Note: there is a slight variation between the Page size of a PDF and a multipage Tiff. 
If your report is very tight to the page, the page rendering might be different.";
			ConsoleUtilities.WriteLine(warning);

			using (var report = new ReportViewer())
			{
				var method = typeof(LocalReport).GetMethod("SetEmbeddedResourceAsReportDefinition",
																									 BindingFlags.NonPublic | BindingFlags.Instance);
				method.Invoke(report.LocalReport, new object[] {reportname, assembly});
				report.LocalReport.EnableExternalImages = true;
				populateDataSources(report.LocalReport.DataSources, report.LocalReport.GetDataSourceNames());
				var bytes = RenderReport(report.LocalReport, "IMAGE");
				ApprovalTests.Approvals.VerifyBinaryFile(bytes, "tiff");
			}
		}

		public static byte[] RenderReport(LocalReport localReport, string format)
		{
			string fileNameExtension;
			string[] streams;
			Warning[] warnings;
			string mimeType;
			string encoding;
			return localReport.Render(format, null, out mimeType, out encoding, out fileNameExtension,
																out streams, out warnings);
		}
	}

	public class DataPairs:Dictionary<string,object>
	{
		
	}

}