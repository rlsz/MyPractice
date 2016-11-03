using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPractice
{
   public static partial class JsonHelper
    {
        public static IsoDateTimeConverter TimeFormat;
        static JsonHelper()
        {
            TimeFormat = new IsoDateTimeConverter();
            TimeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
        /// <summary>
        /// 序列化Object为Json字符串,日期格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public static string ToJsonString(this Object obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, TimeFormat);
        }
        /// <summary>
        /// 反序列化Json字符串成为T的对象实例,如果成员类型为DateTime，则检测格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public static T DeserializeJsonString<T>(this String str)
        {
            return JsonConvert.DeserializeObject<T>(str, TimeFormat);
        }

        public static string ToJsonString(this Object obj, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, converters);//请勿将Newtonsoft.Json.Formatting设置为缩进，否则将导致前台parseJSON函数失效，很多功能将会出现问题
        }
        public static T DeserializeJsonString<T>(this String str, params JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject<T>(str, converters);
        }
    }
}
