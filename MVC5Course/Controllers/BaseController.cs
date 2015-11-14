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
    }
}