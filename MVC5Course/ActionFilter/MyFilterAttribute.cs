using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.ActionFilter
{
    public class MyFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting
            (ActionExecutingContext filterContext)
        {
            Debug.WriteLine("OnActionExecuting");

            filterContext.Controller.ViewBag.Title = "XXX";

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Debug.WriteLine("OnActionExecuted");

            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Debug.WriteLine("OnResultExecuting");

            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Debug.WriteLine("OnResultExecuted");

            base.OnResultExecuted(filterContext);
        }
    }
}