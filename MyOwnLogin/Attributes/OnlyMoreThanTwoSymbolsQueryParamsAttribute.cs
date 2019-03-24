using System.Diagnostics;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyOwnLogin.Attributes
{
    public class OnlyMoreThanTwoSymbolsQueryParamsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext);

            foreach (string key in HttpContext.Current.Request.QueryString.AllKeys)
            {
                string value = HttpContext.Current.Request.QueryString[key];
                if(value == null || value.Length < 2)
                {
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
                    return;
                }
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExequted", filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExequting", filterContext);
        }

        private void Log(string methodName, ControllerContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            string isAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest() ? "AJAX: " : string.Empty;

            var message = $"{isAjaxRequest} {methodName}- controller: {controllerName} action: {actionName}";
            Debug.WriteLine(message);
        }
    }
}