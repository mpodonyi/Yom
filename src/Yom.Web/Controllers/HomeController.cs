using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Yom.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Membership.GetUser() != null)
                return RedirectToAction("Details");

            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Details()
        {
            

            return View();
        }

        
    }
}
