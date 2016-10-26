using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// 正则表达式帮助类
    /// </summary>
    public class RegexHelper
    {
        
        #region === 正则处理 ===

        /// <summary>
        /// 从一段html中取出一个url
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string GetUrlFromHtml(string strHtml)
        {
            var strUrl = GetStrByRegx(strHtml, @"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-,]*)?");

            return strUrl;
        }
        /// <summary>
        /// 从字符串中取出与正则匹配的字符串
        /// </summary>
        /// <param name="inputStr">源字符串</param>
        /// <param name="strPattern">正则表达式 如:/\\d{2,20}/</param>
        /// <returns>string</returns>
        public static string GetStrByRegx(string inputStr, string strPattern)
        {
            var retStr = "";
            try
            {
                MatchCollection mc = Regex.Matches(inputStr, strPattern, RegexOptions.IgnoreCase);
                if (mc.Count > 0)
                {
                    foreach (Match m in mc)
                    {
                        retStr += m.Value;
                    }
                }
            }
            catch
            {
            }
            return retStr;
        }
        /// <summary>
        /// 从字符串中取出与正则匹配的字符串组
        /// </summary>
        /// <param name="inputStr">源字符串</param>
        /// <param name="strPattern">正则表达式 注意要带分组 分组名固定为:"gname" 如: <a id=\"ctl00_M_dtgResumeList(?<gname>.*?).*> </param>
        /// <returns>List-string</returns>
        public static List<string> GetListStrByRegxGroup(string inputStr, string strPattern)
        {
            var list = new List<string>();

            MatchCollection mc = Regex.Matches(inputStr, strPattern, RegexOptions.IgnoreCase);

            if (mc.Count > 0)
            {
                foreach (Match m in mc)
                {
                    var str = m.Groups["gname"].Value.Trim();
                    if (str.Length > 0)
                    {
                        list.Add(str);
                    }
                }
            }

            return list;
        }
        #endregion
    }
}
