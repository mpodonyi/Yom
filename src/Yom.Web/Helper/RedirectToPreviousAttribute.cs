using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yom.Web.Helper
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class RedirectToPreviousAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string ret = filterContext.HttpContext.Request["ret"] as string ?? filterContext.HttpContext.Session["ret"] as string;

            if (ret != null)
            {
                if (filterContext.Result is RedirectToRouteResult)
                {
                    RedirectResult result = new RedirectResult(ret);
                    filterContext.Result = result;
                    filterContext.HttpContext.Session.Remove("ret");
                }
                else
                {
                    filterContext.HttpContext.Session["ret"] = ret;
                }
            }
        }
    }
}