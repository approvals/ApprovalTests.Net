using System.Web.Mvc;
using ApprovalUtilities.Asp.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
	public partial class CoolController : Controller
	{
		//
		// GET: /Cool/

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult SaveName(Person person)
		{
			return View(person);
		}


	}

#if DEBUG
	public partial class CoolController
	{
		public ActionResult TestName()
		{
			return MvcUtilites.CallViewResult(SaveName, new Person { Name = "Henrik" });
		}
	}
#endif
}
