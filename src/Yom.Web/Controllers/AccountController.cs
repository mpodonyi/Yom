using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Yom.Web.Models;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.Messaging;
using Yom.Web.Services;
using Yom.Web.Models.User;

namespace Yom.Web.Controllers
{
    public class AccountController : Controller
    {
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();

        IUserService UserService;

        public AccountController()
        {
            UserService = new UserService();
        }


        //[ValidateInput(false)]
        //public ActionResult Authenticate(string returnUrl)
        //{
        //    var response = openid.GetResponse();
        //    if (response == null)
        //    {
        //        // Stage 2: user submitting Identifier
        //        Identifier id;
        //        if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
        //        {
        //            try
        //            {
        //                var request = openid.CreateRequest(Request.Form["openid_identifier"]);

        //                //Ask user for their email address
        //                ClaimsRequest fields = new ClaimsRequest();
        //                fields.Email = DemandLevel.Require;
        //                fields.FullName = DemandLevel.Require;
        //                request.AddExtension(fields);
                        
        //                return request.RedirectingResponse.AsActionResult();
        //            }
        //            catch (ProtocolException ex)
        //            {
        //                ViewData["Message"] = ex.Message;
        //                return View("LogOn");
        //            }
        //        }

        //        ViewData["Message"] = "Invalid identifier";
        //        return View("LogOn");
        //    }

        //    // Stage 3: OpenID Provider sending assertion response
        //    switch (response.Status)
        //    {
        //        case AuthenticationStatus.Authenticated:
        //            MembershipUser user = Membership.GetUser(response.ClaimedIdentifier);
        //            if (user == null)
        //            {
        //                MembershipCreateStatus membershipCreateStatus;


        //                //Get custom fields for user
        //                var sreg = response.GetExtension<ClaimsResponse>();
        //                if (sreg != null)
        //                    Membership.CreateUser(response.ClaimedIdentifier, "12345", sreg.Email, "", "", true, out membershipCreateStatus);
        //                else
        //                    Membership.CreateUser(response.ClaimedIdentifier, "12345", "john@doe.com", "", "", true, out membershipCreateStatus);

        //                if (membershipCreateStatus == MembershipCreateStatus.Success)
        //                {
        //                    FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);
        //                    //FormsService.SignIn(response.ClaimedIdentifier, false /* createPersistentCookie */);
        //                    return RedirectToAction("Index", "home");
        //                }
        //                ViewData["Message"] = "Error creating new user";
        //                return View("LogOn");
        //            }

        //            FormsAuthentication.SetAuthCookie(user.UserName, false);
        //            if (!string.IsNullOrEmpty(returnUrl))
        //            {
        //                return Redirect(returnUrl);
        //            }

        //            return RedirectToAction("Index", "Home");
        //        case AuthenticationStatus.Canceled:
        //            ViewData["Message"] = "Canceled at provider";
        //            return View("LogOn");
        //        case AuthenticationStatus.Failed:
        //            ViewData["Message"] = response.Exception.Message;
        //            return View("LogOn");
        //    }

        //    return new EmptyResult();
        //}


 
        //public ActionResult LogOn()
        //{
        //    var openid = new OpenIdRelyingParty();
        //    IAuthenticationResponse response = openid.GetResponse();
 
        //    if (response != null)
        //    {
        //        switch (response.Status)
        //        {
        //            case AuthenticationStatus.Authenticated:
        //                FormsAuthentication.RedirectFromLoginPage(
        //                    response.ClaimedIdentifier, false);
        //                break;
        //            case AuthenticationStatus.Canceled:
        //                ModelState.AddModelError("loginIdentifier",
        //                    "Login was cancelled at the provider");
        //                break;
        //            case AuthenticationStatus.Failed:
        //                ModelState.AddModelError("loginIdentifier",
        //                    "Login failed using the provided OpenID identifier");
        //                break;
        //        }
        //    }           
 
        //    return View();
        //}

        
        public ActionResult LogOn(string openid_identifier, string returnUrl)
        {
            var response = openid.GetResponse();
            if (response == null)
            {
                // Stage 2: user submitting Identifier
                Identifier id;
                if (Identifier.TryParse(openid_identifier, out id))
                {
                    try
                    {
                        var request = openid.CreateRequest(openid_identifier);

                        //Ask user for their email address
                        ClaimsRequest fields = new ClaimsRequest();
                        fields.Email = DemandLevel.Require;
                        request.AddExtension(fields);

                        return request.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        ViewData["Message"] = ex.Message;
                        return View();
                    }
                }

                ViewData["Message"] = "Invalid identifier";
                return View();
            }

            // Stage 3: OpenID Provider sending assertion response
            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    UserViewModel user = UserService.UserGet(response.ClaimedIdentifier);
                    if (user == null)
                    {
                        MembershipCreateStatus membershipCreateStatus;
                       
                        //Get custom fields for user
                        var sreg = response.GetExtension<ClaimsResponse>();
                        if (sreg != null)
                            membershipCreateStatus = UserService.UserCreate(response.ClaimedIdentifier, sreg.Email);
                            //Membership.CreateUser(response.ClaimedIdentifier, "12345", sreg.Email, "", "", true, out membershipCreateStatus);
                        else
                            membershipCreateStatus = UserService.UserCreate(response.ClaimedIdentifier, "john@doe.com"); 
                            //Membership.CreateUser(response.ClaimedIdentifier, "12345", "john@doe.com", "", "", true, out membershipCreateStatus);

                        if (membershipCreateStatus == MembershipCreateStatus.Success)
                        {
                           // FormsAuthentication.RedirectFromLoginPage()

                            FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, true);
                            //FormsService.SignIn(response.ClaimedIdentifier, false /* createPersistentCookie */);
                            return RedirectToAction("Index", "home");
                        }
                        ViewData["Message"] = "Error creating new user";
                        return View();
                    }

                   // Membership.DeleteUser(user.UserName, true);

                    FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, true);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                case AuthenticationStatus.Canceled:
                    ViewData["Message"] = "Canceled at provider";
                    return View();
                case AuthenticationStatus.Failed:
                    ViewData["Message"] = response.Exception.Message;
                    return View();
            }

            return new EmptyResult();
        }



        ////
        //// GET: /Account/LogOn

        //public ActionResult LogOn()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/LogOn

        //[HttpPost]
        //public ActionResult LogOn(LogOnModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Membership.ValidateUser(model.UserName, model.Password))
        //        {
        //            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
        //            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
        //                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
        //            {
        //                return Redirect(returnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "The user name or password provided is incorrect.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
