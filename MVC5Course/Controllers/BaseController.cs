using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ProductRepository repo = RepositoryHelper.GetProductRepository();

        public ActionResult DeBug()
        {
            return View();
        }

        protected override void HandleUnknownAction(string actionName)
        {
            if (Request.IsLocal)
                this.Redirect("/?unknown-action=" + actionName).ExecuteResult(this.ControllerContext);
            else
                base.HandleUnknownAction(actionName);
        }
    }
}