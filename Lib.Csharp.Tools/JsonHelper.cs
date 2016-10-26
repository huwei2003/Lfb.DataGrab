using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// Json操作类  用Newtonsoft.Json实现 推荐用
    /// </summary>
    public class JsonHelper
    {


        /// <summary>
        /// 对象转换为Json字符串
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string ToJson(object jsonObject)
        {
            return JsonConvert.SerializeObject(jsonObject);
        }

        /// <summary>
        /// 对象转换为Json字符串
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <param name="jsonName">json串的名称</param>
        /// <returns></returns>
        public static string ToJson(object jsonObject,string jsonName)
        {
            return "{\"" + jsonName + "\":"+JsonConvert.SerializeObject(jsonObject)+"}";
        }
        /// <summary>
        /// json字符串序列化为object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T ToObject<T>(string strJson)
        {
            return JsonConvert.DeserializeObject<T>(strJson);
        }
    }
}
