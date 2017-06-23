using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForWebStudy.Controllers
{
    public class ActivityTestController : Controller
    {
        static ActivityTestController()
        {

        }
        // GET: Activitys
        public ActionResult Index(string pageName="")
        {


            if (string.IsNullOrEmpty(pageName))
            {
                return View();
            }
            else
            {
                return View(pageName);
            }
        }
        public ActionResult Test(string param2)
        {
            return View();
        }

        protected override void HandleUnknownAction(string actionName)
        {
            //检索静态页面
            this.View(actionName).ExecuteResult(this.ControllerContext);
        }
    }
}