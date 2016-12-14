using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Filters
{
    public class AdminAuthorize : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {

            //get user from session["User"]
            var user = (User)System.Web.HttpContext.Current.Session["User"];

            if (user == null || user.isAdmin == false)
            {
                //if not redirect to Home
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Index");
                redirectTargetDictionary.Add("controller", "Home");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                this.OnActionExecuting(filterContext);
            }

            //check if user is admin or not 
            //if (user.isAdmin == false)
            //{
            //    // if not redirect to home 
            //    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
            //    redirectTargetDictionary.Add("action", "Index");
            //    redirectTargetDictionary.Add("controller", "Home");
            //    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            //    this.OnActionExecuting(filterContext);
            //}

            //if yes continue
            this.OnActionExecuting(filterContext);
        }

    }
}