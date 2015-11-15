using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.ActionFilter;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [MyFilter]
        public ActionResult Test()
        {
            Debug.WriteLine("Test");

            //if(Request.IsAjaxRequest())
            throw new Exception("BAD");

            return View();
        }

        public ActionResult ViewTest()
        {
            //return PartialView();

            int[] data = new int[] { 1, 2, 3, 4, 5 };

            return View(data);
        }
    }
}