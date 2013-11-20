using System;
using System.Web.Security;

namespace Asp.Net.Demo.Account
{
	public partial class Register : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
		}

		protected void RegisterUser_CreatedUser(object sender, EventArgs e)
		{
			FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

			string continueUrl = RegisterUser.ContinueDestinationPageUrl;
			if (String.IsNullOrEmpty(continueUrl))
			{
				continueUrl = "~/";
			}
			Response.Redirect(continueUrl);
		}

	}
}
