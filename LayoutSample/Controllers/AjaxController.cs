using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayoutSample.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTime()
        {
            return Content(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff"));
        }
    }
}