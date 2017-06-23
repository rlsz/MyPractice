using MyPractice;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class CustomControllerActionInvoker: ControllerActionInvoker
    {
        private static List<ResultCache> caches = new List<ResultCache>();

        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            if (actionDescriptor.GetCustomAttributes(typeof(NoCacheAttribute), false).Length > 0)
            {
                return base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);
            }

            var cache = caches.FirstOrDefault(c => c.Match(actionDescriptor.ControllerDescriptor.ControllerName, actionDescriptor.ActionName, parameters));
            if (cache == null)
            {
                cache = new ResultCache(actionDescriptor.ControllerDescriptor.ControllerName, actionDescriptor.ActionName, parameters);
                caches.Add(cache);
            }

            ActionResult result;
            if (cache.result == null)
            {
                result = base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);
                cache.Refresh(result);
            }
            else
            {
                result = cache.result;
                //异步更新缓存
                cache.AsyncRefresh(() => base.InvokeActionMethod(controllerContext, actionDescriptor, parameters));
                //cache.AsyncRefresh(() => {
                //    //Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"), "my log in task");
                //    var res = base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);
                //    var test = controllerContext.HttpContext.Response.Headers["Custom-Cache"];
                //    return res;
                //});
                //System.Threading.Thread.Sleep(3000);
                //Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"), "my log out task");
                //Debug.WriteLine(result.ToJsonString(), "my log out task");

            }
            return result;
        }
    }
    public class ResultCache
    {
        public ResultCache(string controllerName, string actionName, IDictionary<string, object> parameters)
        {
            this.controllerName = controllerName;
            this.actionName = actionName;
            this.parameters = parameters;
        }
        public ActionResult result { get; set; }
        public string controllerName { get; set; }
        public string actionName { get; set; }
        public IDictionary<string, object> parameters { get; set; }
        public void Refresh(ActionResult result)
        {
            //审查result的内容，决定是否需要缓存
            this.result = result;
        }
        private bool asyncRefreshFlag = false;
        public void AsyncRefresh(Func<ActionResult> func)
        {
            if (!asyncRefreshFlag)
            {
                asyncRefreshFlag = true;
                var temp = Task.Factory.StartNew(func).ContinueWith((task) =>
                  {
                      this.Refresh(task.Result);
                      asyncRefreshFlag = false;
                  });
                var status = temp.Status;
            }
        }
        public bool Match(string controllerName, string actionName, IDictionary<string, object> parameters)
        {
            if (!this.controllerName.Equals(controllerName))
            {
                return false;
            }
            if (!this.actionName.Equals(actionName))
            {
                return false;
            }
            if (this.parameters == null && parameters == null)
            {
                return true;
            }
            if (this.parameters == null || parameters == null)
            {
                return false;
            }
            foreach (var item in this.parameters.Keys)
            {
                if (!this.parameters[item].Equals(parameters[item]))
                {
                    return false;
                }
            }
            return true;
        }
    }

    /// <summary>
    /// 不缓存action result
    /// </summary>
    public class NoCacheAttribute:Attribute
    {

    }
    public class HaCacheAttribute : Attribute
    {

    }
}