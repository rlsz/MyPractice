using System.Web.Mvc;

namespace ForWebStudy.Areas.Activity2
{
    public class Activity2AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Activity2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Activity2_default",
                "Activity2/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "ForWebStudy.Areas.Activity2" }
            );
        }
    }
}