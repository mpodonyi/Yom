using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yom.Lib.Data.EF;
using System.Data.Objects.SqlClient;
using Yom.Lib.Services;
using Yom.Web.Models.IPayYou;
using SystemControlCenter.Helper;
using Yom.Web.Models.ReferenceUser;

namespace Yom.Web.Controllers
{
    public class IPayYouController : Controller
    {

        private IReferenceUserService ReferenceUserService;
        private IPaymentService PaymentService;

        public IPayYouController(IPaymentService paymentService, IReferenceUserService referenceUserService)
        {
            ReferenceUserService = referenceUserService;
            PaymentService = paymentService;
        }

        ////
        //// GET: /YouOweMe/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        ////
        //// GET: /YouOweMe/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /YouOweMe/Create

        private void SetDefaults()
        {
            ViewBag.ReferenceUsers = ReferenceUserService.Get().ToSelectListItems(k => k.Id,
                    k => new ReferenceUserViewModel(k).ToString(),
                    new SelectListItem[]
                    {
                        new SelectListItem{ Value="",Text="Select Reference User"},
                        new SelectListItem{ Value="createnewreferenceuser",Text="Create New Reference User..."}
                    }
                    );
        }

        public ActionResult Create()
        {
            SetDefaults();
            return View();
        }

        public ViewResult Success()
        {
            return View();
        }

        //
        // POST: /YouOweMe/Create

        [HttpPost]
        public ActionResult Create(IPayYouCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var retVal = PaymentService.Create(PaymentType.IPayYou, model.ReferenceUser, model.Amount, model.Description);

                if (retVal.IsSuccessful)
                {
                    return RedirectToAction("Success");
                }
                else
                {
                    ModelState.AddModelError("", retVal.Message);
                }
            }
            SetDefaults();
            return View();
        }

        ////
        //// GET: /YouOweMe/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /YouOweMe/Edit/5

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
        //// GET: /YouOweMe/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /YouOweMe/Delete/5

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
