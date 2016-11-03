using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPractice
{
    public static class JsonHelper
    {


        public static string ToJsonString(this Object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None);
        }
        public static string ToJsonString(this Object obj, Formatting formatting)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }
        public static T DeserializeJsonString<T>(this string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
    }
}