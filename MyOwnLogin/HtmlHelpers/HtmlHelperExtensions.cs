using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyOwnLogin.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString LoginLink(this HtmlHelper htmlHelper,
                                                string signInLinkText,
                                                string signOutLinkText,
                                                string signInAction,
                                                string signOutAction,
                                                string controller)
        {
            string linkText = string.Empty;
            string action = string.Empty;

            if(!HttpContext.Current.Request.IsAuthenticated)
            {
                linkText = signInLinkText;
                action = signInAction;
            }
            else
            {
                linkText = string.Format(signOutLinkText, HttpContext.Current.Session["UserID"]);
                action = signOutAction;
            }

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var anchor = new TagBuilder("a")
            {
                InnerHtml = linkText
            };
            anchor.Attributes["href"] = urlHelper.Action(action, controller);

            return MvcHtmlString.Create(anchor.ToString());
        }
    }
}