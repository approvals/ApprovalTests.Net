using System.Web.UI;

namespace ApprovalTests.Asp
{
	public class AspTestingUtils
	{
		public static bool DivertTestCall(Page aspxPage)
		{
			var methodName = aspxPage.Page.ClientQueryString;
			if (methodName.StartsWith("Test"))
			{
				var methodInfo = aspxPage.GetType().GetMethod(methodName);
				if (methodInfo != null)
				{
					methodInfo.Invoke(aspxPage, null);
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}
	}
}