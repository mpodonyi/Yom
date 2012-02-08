using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yom.Web.Models.ReferenceUser;
using Yom.Web.Services;
using Yom.Web.Helper;

namespace Yom.Web.Controllers
{
    [RedirectToPrevious]
    public class ReferenceUserController : Controller
    {
        private IReferenceUserService ReferenceUserService;

        public ReferenceUserController(IReferenceUserService referenceUserService)
        {
            ReferenceUserService = referenceUserService;
        }

        //
        // GET: /ReferenceUser/

        public ActionResult Index()
        {
            var model = from i in ReferenceUserService.Get()
                        select new ReferenceUserViewModel(i);

            return View(model);
        }

        //
        // GET: /ReferenceUser/Details/5

        //public ActionResult Details(long id)
        //{
        //    return View();
        //}

        //
        // GET: /ReferenceUser/Create

        public ActionResult Create()
        {
            

            return View();
        }

        //
        // POST: /ReferenceUser/Create

        [HttpPost]
        public ActionResult Create(ReferenceUserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var retVal = ReferenceUserService.Create(model.Mail, model.LastName, model.FirstName);

                if (retVal.IsSuccessful)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", retVal.Message);
                }
            }

            

            return View();
        }

        //
        // GET: /ReferenceUser/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /ReferenceUser/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /ReferenceUser/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /ReferenceUser/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
