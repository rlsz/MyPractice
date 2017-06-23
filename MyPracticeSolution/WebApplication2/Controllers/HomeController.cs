using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Filters;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [FilterTest]
    public class HomeController : Controller
    {

        protected override IActionInvoker CreateActionInvoker()
        {
            return new CustomControllerActionInvoker();
        }

        protected void AvoidCache()
        {
            //System.Threading.Thread.Sleep(3000);
            //Debug.WriteLine(Response==null, "my log in action");
            //Debug.WriteLine(Response.Headers[0].ToString(), "my log in action");
            Response.Headers["Custom-Cache"] = "no-cache";
            //Response.AddHeader("Custom-Cache", "no-cache");
            System.Threading.Thread.Sleep(5000);
        }
        protected void RefreshCache()
        {
            Response.AddHeader("Custom-Cache", "absolute-cache");
        }
        [HaCache]
        [NoCache]
        public ActionResult Index()
        {

            //test11();
            //test22();

            return View();
        }
        public ActionResult AjaxTest(string param1,string param2)
        {
            Random random = new Random();
            var aaa = random.Next(30);
            AvoidCache();
            Debug.WriteLine(aaa, "my log in action");
            return Json(new { test = aaa, param1 = param1, param2 = param2, time = DateTime.Now.ToString("HH:mm:ss:fff") }, JsonRequestBehavior.AllowGet);
        }
        private void test11()
        {
            //Stopwatch sp = new Stopwatch();
            //sp.Start();
            string str = "";
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    str += "string append1= ";
                    str += i.ToString() + " ";
                    str += "string append2= ";
                    str += j.ToString() + " ";
                }
            }
            //sp.Stop();
            //Console.WriteLine("Test1 Time={0}", sp.Elapsed.ToString());
        }
        private void test22()
        {
            //Stopwatch sp = new Stopwatch();
            //sp.Start();
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    str.Append("string append1= ");
                    str.Append(i.ToString());
                    str.Append("string append2=");
                    str.Append(j.ToString());
                }
            }
            //sp.Stop();
            //Console.WriteLine("Test2 Time={0}", sp.Elapsed.ToString());
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
        public ActionResult test()
        {

            AvoidCache();
            return View();
        }
        public ActionResult test1()
        {
            return null;
            //return View();
        }
    }
    
}