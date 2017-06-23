using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication2.Filters
{
    public class FilterTest:ActionFilterAttribute
    {
        public static string Test { get; set; }

        private static List<ActionCache> caches = new List<ActionCache>();
        private ActionCache currentCache;

        private Action callback;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);

            StringWriter cachingWriter = new StringWriter(CultureInfo.InvariantCulture);
            TextWriter originalWriter = filterContext.HttpContext.Response.Output;
            filterContext.HttpContext.Response.Output = cachingWriter;
            callback = () =>
            {
                filterContext.HttpContext.Response.Output = originalWriter;
                string capturedText = cachingWriter.ToString();
                filterContext.HttpContext.Response.Write(capturedText);
            };


            return;
            var cache = caches.FirstOrDefault(c => c.Match(filterContext));
            if (cache != null)
            {
                //task

                currentCache = cache;
                filterContext.Result = cache.result;

                //filterContext.ActionDescriptor.Execute(filterContext.Controller.ControllerContext, filterContext.ActionParameters);

                //filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                //filterContext.HttpContext.Response.BufferOutput = true;
                //base.OnResultExecuted(new ResultExecutedContext(filterContext.Controller.ControllerContext, cache.result, true, null));
            }
            else
            {
                currentCache = new ActionCache(filterContext);
                caches.Add(currentCache);
            }
            
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            return;
            if (isNeedCache(filterContext))
            {
                currentCache.Refresh(filterContext.Result);
            }

            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            callback?.Invoke();
            
            Log("OnResultExecuted", filterContext.RouteData);
        }
        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Debug.WriteLine(message, "Action Filter Log");
        }
        private bool isNeedCache(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Response.Headers["Custom-Cache"] == "no-cache")
            {
                return false;
            }
            if (filterContext.HttpContext.Response.Headers["Custom-Cache"] == "absolute-cache")
            {
                return true;
            }
            if (filterContext.Result is EmptyResult)
            {
                return false;
            }
            //TODO 根据result类型进行内容审核，判断是否需要更新缓存
            return true;
        }
    }
    public class ActionCache: IEquatable<ActionCache>
    {
        public ActionCache(ActionExecutingContext context)
        {
            this.controller = context.RouteData.Values["controller"];
            this.action = context.RouteData.Values["action"];
            this.actionParams = context.ActionParameters;
        }

        public Object controller { get; set; }
        public Object action { get; set; }
        public IDictionary<string, object> actionParams { get; set; }
        public ActionResult result { get; set; }

        public void Refresh(ActionResult result)
        {
            this.result = result;
        }

        public bool Equals(ActionCache other)
        {
            return Match(other.controller, other.action, other.actionParams);
        }
        public bool Match(ActionExecutingContext context)
        {
            return Match(context.RouteData.Values["controller"], context.RouteData.Values["action"], context.ActionParameters);
        }
        private bool Match(Object controller, Object action, IDictionary<string, object> actionParams)
        {
            if (!this.controller.Equals(controller))
            {
                return false;
            }
            if (!this.action.Equals(action))
            {
                return false;
            }
            if (this.actionParams == null && actionParams == null)
            {
                return true;
            }
            if (this.actionParams == null || actionParams == null)
            {
                return false;
            }
            foreach (var item in this.actionParams.Keys)
            {
                if (!this.actionParams[item].Equals(actionParams[item]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}