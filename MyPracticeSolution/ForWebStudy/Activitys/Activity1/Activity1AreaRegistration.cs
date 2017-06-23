using System.Web.Mvc;

namespace ForWebStudy.Areas.Activity1
{
    public class Activity1AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Activity1";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Activity1_default",
                "Activity1/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "ForWebStudy.Areas.Activity1.Controllers" }
            );
        }
    }
}