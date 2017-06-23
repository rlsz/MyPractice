using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Areas.test.Controllers
{
    public class TestController : Controller
    {
        // GET: test/Test
        public ActionResult Index()
        {
            return View();
        }
    }
}