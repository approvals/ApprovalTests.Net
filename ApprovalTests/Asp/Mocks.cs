using System.Collections.Specialized;
using System.Web;

namespace ApprovalTests.Asp
{
	public class MockContextBase : HttpContextBase
	{
		private readonly string url;

		public MockContextBase(string url)
		{
			this.url = url;
		}

		public override HttpRequestBase Request
		{
			get { return new MockHttpRequest(url); }
		}
		public override HttpResponseBase Response
		{
			get
			{
				return new MockHttpResponse();
			}
		}
	}

	public class MockHttpResponse : HttpResponseBase
	{
		public override string ApplyAppPathModifier(string virtualPath)
		{
			return virtualPath;
		}
	}

	public class MockHttpRequest : HttpRequestBase
	{
		private readonly string url;

		public MockHttpRequest(string url)
		{
			this.url = url;
		}

		public override string AppRelativeCurrentExecutionFilePath
		{
			get { return url; }
		}

		public override string ApplicationPath
		{
			get
			{
				return url.Substring(1);
			}
		}

		public override string PathInfo
		{
			get
			{
				return "";
			}
		}
		public override System.Collections.Specialized.NameValueCollection ServerVariables
		{
			get
			{
				return new NameValueCollection();
			}
		}

	}
}