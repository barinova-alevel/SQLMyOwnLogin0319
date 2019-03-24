using System.Web.Mvc;
using System.Web.Security;
using MyOwnLogin.Attributes;
using System.Security.Principal;
using MyOwnLogin.BusinesLogic.Managers;
using System.Web;
using System;
using MyOwnLogin.Core.ViewModels;
using MyOwnLogin.BusinesLogic.Interfaces;

namespace MyOwnLogin.Controllers
{
    [OnlyMoreThanTwoSymbolsQueryParams]
    public class LoginController : Controller
    {
        IAccountManager _manager;

        public LoginController(IAccountManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult SignIn(string returnUrl)
        {
            var loginVm = new LoginVm()
            {
                ReturnUrl = returnUrl
            };

            return View(loginVm);
        }

        [HttpPost]
        public ActionResult SignIn(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                var account = _manager.GetAccount(loginVm);

                if(account != null)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(account.Login, true);
                    Session["UserId"] = account.UserName;
                    SetRefreshTokenCookie(loginVm);
                    return RedirectToLocal();
                }
                else
                {
                    ModelState.AddModelError("SigninError", "There is no user with such login and password");
                }
            }

            return View(loginVm);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            Session.Clear();
            System.Web.HttpContext.Current.Session.Clear();

            return RedirectToLocal();
        }

        public JsonResult IsAccountExists(string login)
        {
            return Json(_manager.IsExitedByName(login), JsonRequestBehavior.AllowGet);
        }

        private ActionResult RedirectToLocal(string returnUrl = "")
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        private void SetRefreshTokenCookie(LoginVm loginVm)
        {
            var token = _manager.GetToken(loginVm);

            HttpCookie tokenCookie = new HttpCookie("PhoneBookCookie");
            tokenCookie.Value = token.RefreshToken;
            tokenCookie.Expires = DateTime.MaxValue;

            Response.Cookies.Add(tokenCookie);
        }
    }
}