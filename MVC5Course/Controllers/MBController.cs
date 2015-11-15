using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class MBController : Controller
    {
        // GET: MB
        public ActionResult Index()
        {
            var data = new NewProductVM()
            {
                Price = 100,
                ProductName = "T-Shirt"
            };

            ViewData["MyTitle"] = "will";
            ViewBag.MyTitle = "will 2";

            var db = new FabricsEntities();
            db.Configuration.LazyLoadingEnabled = false;
            ViewBag.Products = db.Product.Take(5).ToList();

            TempData["Msg"] = "TEST!";

            ViewData.Model = data;
            return View();
        }

        public ActionResult FormBinding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FormBinding([Bind(Exclude="Active")] Product prod)
        {
            return Json(prod);
        }

        public ActionResult MultiBinding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MultiBinding(Product data1, Product data2)
        {
            return Json(data1);
        }

    }
}