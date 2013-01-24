using System;
using System.Web.Mvc;
using ApprovalUtilities.CallStack;

namespace ApprovalUtilities.Asp.Mvc
{
    public static class MvcUtilites
    {
        public static ViewResult CallViewResult<T>(Func<T, ActionResult> call, T parameter)
        {
            var actionResult = (ViewResult) call(parameter);
            actionResult.ViewName = call.Method.Name;
            return actionResult;
        }

        public static ViewResult Explicit(this ViewResult view)
        {
            view.ViewName = new Caller().Method.Name;
            return view;
        }
    }
}