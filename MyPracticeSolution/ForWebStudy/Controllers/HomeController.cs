using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForWebStudy.Controllers
{
    public class HomeController : Controller
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

        public JsonResult test()
        {
            var test = new List<object>();
            for(var i = 0; i < 30; i++)
            {
                test.Add(new { a = "aa" });
            }
             
            return Json(new {total=1000,data= test }, JsonRequestBehavior.AllowGet);
            
        }
    }
}