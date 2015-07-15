using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Routing;
using ApprovalTests.Html;
using ApprovalTests.Scrubber;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Asp
{
	public static class AspApprovals
	{
		/// <summary>
		///   Uses PortFactory.AspPort
		/// </summary>
		public static void VerifyAspPage(Action testMethod)
		{
			VerifyAspPage(testMethod, ScrubberUtils.NO_SCRUBBER);
		}

		public static void VerifyAspPage(Action testMethod, Func<string, string> htmlScrubber)
		{
			var port = PortFactory.AspPort;
			VerifyAspPage(testMethod, port, htmlScrubber);
		}

		public static void VerifyAspPage(Action testMethod, int port)
		{
			VerifyAspPage(testMethod, port, ScrubberUtils.NO_SCRUBBER);
		}

		private static void VerifyAspPage(Action testMethod, int port, Func<string, string> htmlScrubber)
		{
			var url = GetUrl(testMethod, "http://localhost:{0}".FormatWith(port));
			VerifyUrl(url, htmlScrubber);
		}

		private static string GetUrl(Action testMethod, string host)
		{
			var type = testMethod.Method.DeclaringType;
			var clazz = type.Name;
			var path = type.Namespace.Substring(type.Assembly.GetName().Name.Length);
			path = path.Replace('.', '/');
			var method = testMethod.Method.Name;
			return "{0}{1}/{2}.aspx?{3}".FormatWith(host, path, clazz, method);
		}

		public static void VerifyUrl(string url)
		{
			VerifyUrl(url, ScrubberUtils.NO_SCRUBBER);
		}

		public static void VerifyUrl(string url, Func<string, string> htmlScrubber)
		{
			string html = GetUrlContents(url);
			var scrubbedHtml = htmlScrubber(html);
			HtmlApprovals.VerifyHtml(scrubbedHtml);
		}

		public static string GetUrlContents(string url)
		{
			try
			{
				url = ResolveUrl(url);
				using (var client = new WebClient())
				{
					client.Encoding = Encoding.UTF8;
					var baseUrl = url.Substring(0, url.LastIndexOf("/"));
					var html = client.DownloadString(url);
					if (!html.Contains("<base"))
					{
						html = html.Replace("<head>", "<head><base href=\"{0}\">".FormatWith(baseUrl));
					}

					return html;
				}
			}
			catch (Exception e)
			{
				throw new Exception(
					"The following error occured while connecting to:\r\n{0}\r\nError:\r\n{1}".FormatWith(url, e.Message), e);
			}
		}

		public static string ResolveUrl(string rawUrl)
		{
			rawUrl = string.IsNullOrWhiteSpace(rawUrl) ? "/" : rawUrl.TrimStart('~');
			return rawUrl.StartsWith("/") ? "http://localhost:{0}{1}".FormatWith(PortFactory.AspPort, rawUrl) : rawUrl;
		}


		public static void VerifyRouting(Action<RouteCollection> registerRoutesMethod, params string[] urls)
		{
			var routes = new RouteCollection();
			registerRoutesMethod(routes);
			var sb = new StringBuilder();
			foreach (var url in urls)
			{
				var correctedUrl = url.StartsWith("~") ? url : '~' + url;
				HttpContextBase httpContext = new MockContextBase(correctedUrl);
				var route = routes.GetRouteData(httpContext);
                var routeText = route == null ? "404: Not found" : route.Values.ToReadableString();
			    sb.AppendFormat("{0} => {1} \r\n", url, routeText);
			}
			Approvals.Verify(sb.ToString());
		}
	}
}