using System;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Net;
using System.Text;
using System.Web.Mvc;
using ApprovalTests.ExceptionalExceptions;
using ApprovalTests.Html;
using ApprovalTests.Scrubber;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Asp.Mvc
{
	public class MvcApprovals
	{
		public static string GetUrlPostContents(string url, NameValueCollection nameValueCollection)
		{
			try
			{

				using (WebClient webClient = new WebClient())
				{
					string str1 = url.Substring(0, url.LastIndexOf("/"));
                    webClient.Encoding = Encoding.UTF8;
					byte[] resp = webClient.UploadValues(url, "POST", nameValueCollection);
					string str2 = Encoding.UTF8.GetString(resp);

					if (!str2.Contains("<base"))
					{

						str2 = str2.Replace("<head>", "<head><base href=\"{0}\">".FormatWith(str1));
					}
					return str2;
				}
			}
			catch (Exception ex)
			{
				throw Exceptional.Create<Exception>("The following error occured while connecting to:\n{0}\nError:\n{1}", url, ex.Message);
			}
		}
		public static void VerifyMvcViaPost<T>(Func<T, ActionResult> func, NameValueCollection nameValueCollection)
		{
			string clazz = func.Target.GetType().Name.Replace("Controller", String.Empty);
			string action = func.Method.Name;
			VerifyUrlViaPost("http://localhost:{0}/{1}/{2}".FormatWith(PortFactory.MvcPort, clazz, action), nameValueCollection);
		}
		public static void VerifyMvcViaPost<T>(Func<T, ActionResult> func, T value)
		{
			NameValueCollection pieces = new NameValueCollection();
			foreach (var property in value.GetType().GetProperties())
			{
				pieces.Add(property.Name, "" + property.GetValue(value, null));
			}

			VerifyMvcViaPost(func, pieces);
		}
		public static void VerifyMvcPage(Func<ActionResult> func)
		{
			string clazz = func.Target.GetType().Name.Replace("Controller", String.Empty);
			string action = func.Method.Name;
		    var url = "http://localhost:{0}/{1}/{2}".FormatWith(PortFactory.MvcPort, clazz, action);
		    AspApprovals.VerifyUrl(url, HtmlScrubbers.ScrubMvc);
		}
		public static void VerifyUrlViaPost(string url, NameValueCollection nameValueCollection)
		{
			HtmlApprovals.VerifyHtml(GetUrlPostContents(url, nameValueCollection), HtmlScrubbers.ScrubMvc);
		}
	}
}