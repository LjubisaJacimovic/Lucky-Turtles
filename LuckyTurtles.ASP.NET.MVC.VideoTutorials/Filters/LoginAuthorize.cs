using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.Routing;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Filters
{
    public class LoginAuthorize: ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //get user from session["User"]
            var user = System.Web.HttpContext.Current.Session["User"];

            //check there is user in session
            if (user == null)
            {
                //if not redirect to Home
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Index");
                redirectTargetDictionary.Add("controller", "Home");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                this.OnActionExecuting(filterContext);
            }

            //if yes continue to action
            this.OnActionExecuting(filterContext);
        }

    }
}