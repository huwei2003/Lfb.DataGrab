using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    public class StrHelper
    {
        public StrHelper()
        {
        }

        #region ===字符串精确截取===

        /// <summary>
        /// 字符串精确截取函数
        /// </summary>
        /// <param name="aSrcStr">要截取的字符串</param>
        /// <param name="aLimitedNum">要截取的长度</param>
        /// <returns>返回截取后的字符串</returns>
        public static string GetStrByLen(string aSrcStr, int aLimitedNum)
        {

            string temp;
            var n = aSrcStr.Count(c => c >= 0x4e00 && c <= 0x9fa5);//中文的个数

            if (aSrcStr.Length > aLimitedNum)
            {
                temp = aSrcStr.Substring(0, aLimitedNum - 1 + 50000);
                temp += "..";
            }
            else
            {
                temp = aSrcStr;
            }
            return temp;
        }



        /// <summary>
        /// 字符串精确截取函数2
        /// </summary>
        /// <param name="aSrcStr">要截取的字符串</param>
        /// <param name="aLimitedNum">要截取的长度</param>
        /// <returns>返回截取后的字符串</returns>
        public static string GetStrByLen2(string aSrcStr, int aLimitedNum)
        {
            if (aSrcStr.Length > aLimitedNum)
            {
                aSrcStr = aSrcStr.Substring(0, aSrcStr.Length - 1);
            }

            if (aLimitedNum <= 0) return aSrcStr;
            var tmpStr = aSrcStr;
            var tmpStrBytes = System.Text.Encoding.GetEncoding("GB2312").GetBytes(tmpStr);
            if (tmpStrBytes.Length <= aLimitedNum)
            {
                return aSrcStr;
            }
            byte[] lStrBytes = null;
            var needStrNum = 0;
            if (tmpStrBytes[aLimitedNum] > 127)
            {
                lStrBytes = new byte[aLimitedNum + 1];
                needStrNum = aLimitedNum + 1;
            }
            else
            {
                lStrBytes = new byte[aLimitedNum];
                needStrNum = aLimitedNum;
            }
            Array.Copy(tmpStrBytes, lStrBytes, needStrNum);
            var temp = System.Text.Encoding.GetEncoding("GB2312").GetString(lStrBytes);
            if (temp.Substring(temp.Length - 1, 1) == "?")
                temp = temp.Substring(0, temp.Length - 1) + "";
            return temp + Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(".."));
        }

        /// <summary>
        /// 字符串精确截取函数3
        /// </summary>
        /// <param name="aSrcStr">要截取的字符串</param>
        /// <param name="aLimitedNum">要截取的长度</param>
        /// <returns>返回截取后的字符串</returns>
        public static string GetStrByLen3(string aSrcStr, int aLimitedNum)
        {
            if (aSrcStr.Length > aLimitedNum)
            {
                aSrcStr = aSrcStr.Substring(0, aSrcStr.Length - 1);
            }

            if (aLimitedNum <= 0) return aSrcStr;
            var tmpStr = aSrcStr;
            byte[] tmpStrBytes = System.Text.Encoding.GetEncoding("GB2312").GetBytes(tmpStr);
            if (tmpStrBytes.Length <= aLimitedNum)
            {
                return aSrcStr;
            }
            else
            {
                byte[] lStrBytes = null;
                int needStrNum = 0;
                if (tmpStrBytes[aLimitedNum] > 127)
                {
                    lStrBytes = new byte[aLimitedNum + 1];
                    needStrNum = aLimitedNum + 1;
                }
                else
                {
                    lStrBytes = new byte[aLimitedNum];
                    needStrNum = aLimitedNum;
                }
                Array.Copy(tmpStrBytes, lStrBytes, needStrNum);
                string temp = System.Text.Encoding.GetEncoding("GB2312").GetString(lStrBytes);
                if (temp.Substring(temp.Length - 1, 1) == "?")
                    temp = temp.Substring(0, temp.Length - 1) + "";
                return temp;
            }
        }


        /// <summary>
        /// 字符串精确截取函数4
        /// </summary>
        /// <param name="aSrcStr">要截取的字符串</param>
        /// <param name="aLimitedNum">要截取的长度</param>
        /// <param name="dotCount">.数</param>
        /// <returns></returns>
        public static string GetLenghtStr(string aSrcStr, int aLimitedNum, int dotCount)
        {
            if (aLimitedNum <= 0) return aSrcStr;
            var tmpStr = aSrcStr;
            byte[] tmpStrBytes = System.Text.Encoding.GetEncoding("GB2312").GetBytes(tmpStr);
            if (tmpStrBytes.Length <= aLimitedNum)
            {
                return aSrcStr;
            }
            else
            {
                byte[] lStrBytes = null;
                var needStrNum = 0;
                if (tmpStrBytes[aLimitedNum] > 127)
                {
                    lStrBytes = new byte[aLimitedNum + 1];
                    needStrNum = aLimitedNum + 1;
                }
                else
                {
                    lStrBytes = new byte[aLimitedNum];
                    needStrNum = aLimitedNum;
                }
                Array.Copy(tmpStrBytes, lStrBytes, needStrNum - 1);
                string temp = System.Text.Encoding.GetEncoding("GB2312").GetString(lStrBytes);
                if (temp.Substring(temp.Length - 1, 1) == "?" || temp.Substring(temp.Length - 1, 1) == "？")
                    temp = temp.Substring(0, temp.Length - 1) + "";
                //temp=temp.Replace("?","").Replace("？","");
                string strDot = string.Empty;

                for (var i = 0; i < dotCount; i++)
                {
                    strDot += ".";
                }
                return temp + strDot;
            }
        }
        #endregion

        #region ===过滤不安全字符===

        /// <summary>
        /// 过滤不安全字符
        /// </summary>
        /// <param name="sStr">要过滤的字符串</param>
        /// <returns>过滤的字符串</returns>
        public static string GetSafetyStr(string sStr)
        {
            string returnStr = sStr;
            returnStr = returnStr.Replace("&", "&amp;");
            returnStr = returnStr.Replace("'", "’");
            returnStr = returnStr.Replace("\"", "&quot;");
            returnStr = returnStr.Replace(" ", "&nbsp;");
            returnStr = returnStr.Replace("<", "&lt;");
            returnStr = returnStr.Replace(">", "&gt;");
            returnStr = returnStr.Replace("\n", "<br>");

            return returnStr;//Globals.GetOutString(returnStr);
        }
        #endregion

        #region ===去除HTML标记===
        /// <summary>
        /// 替换HTML标记
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string FormatHtml(string strHtml)
        {
            //删除脚本
            strHtml = Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            strHtml = Regex.Replace(strHtml, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"([rn])[s]+", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"-->", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"<!--.*", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(quot|#34);", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(iexcl|#161);", "xa1", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(cent|#162);", "xa2", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(pound|#163);", "xa3", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(copy|#169);", "xa9", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&#(d+);", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            strHtml = strHtml.Replace("<", "");
            strHtml = strHtml.Replace(">", "");
            strHtml = strHtml.Replace("rn", "");
            strHtml = strHtml.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return strHtml;
        }

        /// <summary>
        /// 删除脚本
        /// </summary>
        /// <param name="strHtml">输入内容</param>
        /// <returns></returns>
        public static string FormatScript(string strHtml)
        {
            //删除单行脚本
            strHtml = Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除脚本块
            strHtml = Regex.Replace(strHtml, @"<script>[\s\S]*?</script>", "", RegexOptions.IgnoreCase);


            return strHtml;
        }

        /// <summary>
        /// 去除图片中的style
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static string RemoveImgStyle(string contents)
        {
            var str = contents;
            try
            {
                //提取内容中的图片的style,去掉
                var imgList = XpathHelper.GetAttrValueListByXPath(str, "//img", "style");
                if (imgList != null && imgList.Count > 0)
                {
                    foreach (var styleItem in imgList)
                    {
                        try
                        {
                            var style = "style=\"" + styleItem + "\"";
                            var style2 = "style='" + styleItem + "'";
                            str = str.Replace(style, "").Replace(style2, "");
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return str;
        }

        /// <summary>
        /// 去除内容中的a标签
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static string RemoveHref(string contents)
        {
            var str = contents;
            //str = str.Replace("target=\"_blank\"", "");
            try
            {

                str = Regex.Replace(str, @"<a\s*[^>]*>", "", RegexOptions.IgnoreCase);
                str = Regex.Replace(str, @"</a>", "", RegexOptions.IgnoreCase);


                ////提取内容中的 链接的地址
                //var hrefList = StrHelperUtil.GetListOuterHtmlByXPath(str, "//a");
                //if (hrefList != null && hrefList.Count > 0)
                //{
                //    foreach (var item in hrefList)
                //    {
                //        try
                //        {
                //            var aStr = StrHelperUtil.GetStrByXPath(item, "//a", "");
                //            aStr = StrHelperUtil.FormatHtml(aStr);
                //            //var style = "style=\"" + styleItem + "\"";
                //            //var style2 = "style='" + styleItem + "'";
                //            str = str.Replace(item, aStr);
                //            str = Regex.Replace(str, item, aStr);
                //        }
                //        catch (Exception ex)
                //        {
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
            }
            return str;
        }

        #endregion

        #region ===sql分配过滤===
        /// <summary>
        /// 字符串过滤
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public static string FormatSql(string strsql)
        {
            
            //Regex reg = new Regex(@"/w|and|exec|insert|select|delete|update|count|master|truncate|declare|char|mid|chr|/w", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //5次循环替换
            try
            {
                for (var i = 0; i < 5; i++)
                {
                    var mc = Regex.Matches(strsql, @"/<script|exec |insert |select |delete |update |count |master |truncate |declare |char\(|chr\(|mid\(|<script |</script", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    if (mc.Count > 0)
                    {

                        foreach (Match m in mc)
                        {
                            //string _goldAmend = m.Groups["goldAmend"].Value.ToString().Trim();
                            string str1 = m.Groups[0].Value.ToString();
                            strsql = strsql.Replace(str1, "");
                        }
                    }
                    i += 1;
                }
            }
            catch
            { }

            strsql = strsql.Replace("'", "''");
            return strsql;
        }
        #endregion

        #region ===要输入的文字[换行]===
        public static string ReplaceBr(string sStr)
        {
            return sStr.Replace("\r", "<br>");
        }
        #endregion

        #region ===给指定的字标上颜色===

        public static string ReplaceColor(string word, string str, string color, bool isbold)
        {
            return word.Replace(str, (isbold ? "<b>" : "") + "<font color=" + color + ">" + str + "" + (isbold ? "</b>" : ""));
        }
        public static string ReplaceColor(string word, string str, string color)
        {
            return word.Replace(str, "<font color=" + color + ">" + str + "");
        }
        #endregion

        #region ===判断整数===
        public static bool IsInt(string num)
        {
            var reg = new Regex(@"^\d+$");
            return reg.IsMatch(num);
        }
        #endregion

        #region ===查出中文字符串对应的拼音缩写，如“1中国c”返回"1ZGc",如果是非中文，则返回本身===

        /// <summary>
        /// 查出中文字符串对应的拼音缩写，如“1中国c”返回"1ZGc",如果是非中文，则返回本身
        /// </summary>
        /// <param name="chineseStr"></param>
        /// <returns></returns>
        public static string GetCnLetter(string chineseStr)
        {
            byte[] cBs = System.Text.Encoding.Default.GetBytes(chineseStr);

            if (cBs.Length < 2)
                return chineseStr;

            byte ucHigh, ucLow;
            int nCode;

            string strFirstLetter = string.Empty;
            for (int i = 0; i < cBs.Length; i++)
            {
                //1个中文字符的的2个字节都必须大于的128，非中文字符返回本身
                if (cBs[i] < 0x80)
                {
                    strFirstLetter += Encoding.Default.GetString(cBs, i, 1);
                    continue;
                }

                ucHigh = (byte)cBs[i];//中文字符的第1个字节
                ucLow = (byte)cBs[i + 1];//中文字符的第2个字节
                //1个中文字符的2个字节都必须大于161，否则是无效中文
                if (ucHigh < 0xa1 || ucLow < 0xa1)
                    continue;
                else
                    // Treat code by section-position as an int type parameter,
                    //获得一个中文字符的Asc码值 so make following change to nCode.
                    nCode = (ucHigh - 0xa0) * 100 + ucLow - 0xa0;

                string str = FirstLetter(nCode);
                strFirstLetter += str != string.Empty ? str : Encoding.Default.GetString(cBs, i, 2);//如果没有查询出该中文字符的字母则返回该中文
                i++;
            }
            return strFirstLetter;
        }

        /// <summary>
        /// Get the first letter of pinyin according to specified Chinese character code
        /// </summary>
        /// <param name="nCode">the code of the chinese character</param>
        /// <returns>receive the string of the first letter</returns>
        public static string FirstLetter(int nCode)
        {
            string strLetter = string.Empty;

            if (nCode >= 1601 && nCode < 1637) strLetter = "A";
            if (nCode >= 1637 && nCode < 1833) strLetter = "B";
            if (nCode >= 1833 && nCode < 2078) strLetter = "C";
            if (nCode >= 2078 && nCode < 2274) strLetter = "D";
            if (nCode >= 2274 && nCode < 2302) strLetter = "E";
            if (nCode >= 2302 && nCode < 2433) strLetter = "F";
            if (nCode >= 2433 && nCode < 2594) strLetter = "G";
            if (nCode >= 2594 && nCode < 2787) strLetter = "H";
            if (nCode >= 2787 && nCode < 3106) strLetter = "J";
            if (nCode >= 3106 && nCode < 3212) strLetter = "K";
            if (nCode >= 3212 && nCode < 3472) strLetter = "L";
            if (nCode >= 3472 && nCode < 3635) strLetter = "M";
            if (nCode >= 3635 && nCode < 3722) strLetter = "N";
            if (nCode >= 3722 && nCode < 3730) strLetter = "O";
            if (nCode >= 3730 && nCode < 3858) strLetter = "P";
            if (nCode >= 3858 && nCode < 4027) strLetter = "Q";
            if (nCode >= 4027 && nCode < 4086) strLetter = "R";
            if (nCode >= 4086 && nCode < 4390) strLetter = "S";
            if (nCode >= 4390 && nCode < 4558) strLetter = "T";
            if (nCode >= 4558 && nCode < 4684) strLetter = "W";
            if (nCode >= 4684 && nCode < 4925) strLetter = "X";
            if (nCode >= 4925 && nCode < 5249) strLetter = "Y";
            if (nCode >= 5249 && nCode < 5590) strLetter = "Z";

            //if (strLetter == string.Empty)
            //    System.Windows.Forms.MessageBox.Show(String.Format("信息：\n{0}为非常用字符编码,不能为您解析相应匹配首字母！",nCode));
            return strLetter;
        }
        #endregion

        #region ===获取用户登录IP===
        /// <summary>
        /// 获取用户登录IP客户端外网ip
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            string userIp = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userIp))
            {
                userIp = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return userIp;
        }

        /// <summary>
        /// 获得当前页面内网的IP
        /// </summary>
        /// <returns>当前页面客户端内网的IP</returns>
        public static string GetIp2()
        {
            string result;
            try
            {
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            catch
            {
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }


            return result;

        }

        public static string IpAddress
        {
            get
            {
                var result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(result))
                {
                    //可能有代理 
                    if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。 
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIpAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //找到不是内网的地址 
                                }
                            }
                        }
                        else if (IsIpAddress(result)) //代理即是IP格式 
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP 
                    }

                }

                var ipAddress = (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];



                if (string.IsNullOrEmpty(result))
                    result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (string.IsNullOrEmpty(result))
                    result = System.Web.HttpContext.Current.Request.UserHostAddress;

                return result;
            }
        }

        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIpAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        #endregion

        #region ===类型转换===
        /// <summary>
        /// 转换成数值型
        /// </summary>
        /// <param name="strData">要转换的字符型数值</param>
        /// <returns></returns>
        public static int ToInt32(string strData)
        {
            int iData = 0;
            if (strData.Trim() == "")
                iData = 0;
            try
            {
                iData = Convert.ToInt32(strData);
            }
            catch
            {
                iData = 0;
                if (strData.IndexOf(".") > 0)
                {
                    try
                    {
                        iData = Convert.ToInt32(strData.Substring(0, strData.IndexOf(".")));
                    }
                    catch
                    {

                    }
                }
            }
            return iData;
        }
        /// <summary>
        /// 转换成double
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static double ToDouble(string strData)
        {
            double iData = 0;
            if (strData.Trim() == "")
                iData = 0;
            try
            {
                iData = Convert.ToDouble(strData);
            }
            catch
            {
                iData = 0;

            }
            return iData;
        }

        /// <summary>
        /// 转换成Double
        /// </summary>
        /// <param name="strData">要转换的字符型参数</param>
        /// <returns></returns>
        public static decimal ToDecimal(string strData)
        {
            decimal iDecimal = 0;
            if (strData.Trim() == "")
                iDecimal = 0;
            try
            {
                iDecimal = Convert.ToDecimal(strData);
            }
            catch
            {
                iDecimal = 0;
            }
            return iDecimal;
        }
        /// <summary>
        /// 转换成日期格式
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string strData)
        {
            DateTime datetimew = DateTime.Now;
            try
            {
                datetimew = Convert.ToDateTime(strData);
            }
            catch
            {

            }
            return datetimew;
        }
        #endregion

        #region ===format===

        /// <summary>
        /// 去掉字符串前后符号
        /// </summary>
        /// <param name="strDh">要去掉的字符</param>
        /// <param name="strFh">符号</param>
        /// <returns></returns>
        public static string FormatDh(string strDh, string strFh)
        {
            if (strDh.Length > 1)
            {
                //strDh = strDh.Replace(",,", ",");
                if (strDh.Substring(0, 1) == strFh)
                    strDh = strDh.Substring(1);
                if (strDh.Substring(strDh.Length - 1) == strFh)
                    strDh = strDh.Substring(0, strDh.Length - 1);
            }
            return strDh;
        }
        /// <summary>
        /// 格式化日期及时间，
        /// </summary>
        /// <param name="strDate">转入的字符串</param>
        /// <param name="iType">为类型，1表示为2010年4月14日;2为2010年4月14日 9时8分25秒</param>
        /// <returns></returns>
        public static string FormatDate(string strDate, int iType)
        {
            if (strDate.Trim() == "")
                return "";

            string str = "";
            try
            {
                var dt = Convert.ToDateTime(strDate);

                if (iType == 1)
                {
                    str = string.Format("{0}年{1}月{2}日", dt.Year, dt.Month, dt.Day);
                    //str += dt.Year + "年";
                    //str += dt.Month + "月";
                    //str += dt.Day + "日";
                }
                else if (iType == 2)
                {
                    str = string.Format("{0}年{1}月{2}日 {3}时{4}分{5}秒", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                    //str += dt.Year + "年";
                    //str += dt.Month + "月";
                    //str += dt.Day + "日 ";
                    //str += dt.Hour + "时";
                    //str += dt.Minute + "分";
                    //str += dt.Second + "秒";
                }
            }
            catch
            {
                str = "";
            }
            return str;
        }


        /// <summary>
        /// 格式化金额
        /// </summary>
        /// <param name="strJy">转入要转换的字符</param>
        /// <param name="iType">为类型，1表示为425.40元，2表示￥425.40元</param>
        /// <returns></returns>
        public static string FormatMoney(string strJy, int iType)
        {
            if (strJy.Trim() == "")
                return "";

            var str = "";
            try
            {
                double dNum = Convert.ToDouble(strJy);
                if (iType == 1)
                {
                    str = dNum.ToString("N");
                }
                else if (iType == 2)
                {
                    str = dNum.ToString("C");
                }
            }
            catch
            {
            }
            return str;
        }

        /// <summary>
        /// 转换成性别
        /// </summary>
        /// <param name="strSex">要转换的字符，如1表示男，2表示女，0表示性别不限</param>
        /// <returns></returns>
        public static string FormatSex(string strSex)
        {
            if (strSex.Trim() == "")
                return "";
            if (strSex.Trim() == "0")
                return "性别不限";
            else if (strSex.Trim() == "1")
                return "男";
            else if (strSex.Trim() == "2")
                return "女";
            else
                return "";
        }

        /// <summary>
        /// 过滤非法关键词
        /// </summary>
        /// <param name="content">待过滤的内容</param>
        public static string FilterBadWords(string content)
        {
            content = FormatSql(content);
            string[] badWords = { "自杀手册", "凌辱美少女", "毛泽东毛爷爷", "大祚榮", "校花沉沦记", "五奶小青", "江湖淫娘", "红楼绮梦", "骆冰淫传", "夫妇乐园", "阿里布达年代记", "爱神之传奇", "不良少女日记", "沧澜曲", "创世之子猎艳之旅", "熟女之惑", "风骚侍女", "海盗的悠闲", "黑星女侠", "狡猾的风水相师", "俪影蝎心", "秦青的幸", "四海龙女", "我的性启蒙", "伴我淫", "屠龙别记", "淫术炼金士", "十景缎", "舌战法庭", "少妇白洁", "风尘劫", "妇的哀羞", "哥言语录", "年春衫薄", "王子淫传", "少年阿宾", "禁断少女", "枪淫少妇", "淫间道", "嫩穴", "电车之狼", "淫水", "肉棍", "鸡吧", "鸡巴", "朱蒙", "出售走私", "浩方平台抽奖", "针孔摄像头", "出售银行", "出售发票", "迷昏", "失意药", "遗忘药", "失身药", "乙醚", "迷魂药", "迷魂", "失忆药", "手qiang", "迷幻", "mihuan", "mi幻", "qiang支", "枪zhi", "出售手枪", "卫星接收器", "香港GHB水", "透视", "老虎机", "轮盘机", "百家乐", "连线机", "模拟机", "彩票机", "礼品机", "火药制作", "麻醉枪", "监听", "监视", "海乐神", "酣乐欣", "三唑仑", "窃听", "三挫仑", "短信猫", "车牌反光", "江绵恒", "海归美女国内手眼通天", "激流中国", "富人与农民工", "98印尼", "华人惨案", "华国锋", "批评刘少奇", "李书凯", "蚁力神", "五毛党", "网络评论员工作指南", "邓小平", "高干子女名单", "鄧小平", "中国震惊世界", "江泽民", "吴官正无官", "罗干不干", "曾庆红不红", "黄菊早黄", "六合采", "六合彩", "六和彩", "白小姐", "踩江民谣", "罢食", "罢吃", "罢饭", "法輪", "镇压学生", "趙紫陽", "赵紫阳", "自由亚州", "人民报", "法lun功", "香港马会", "曾道人", "特码", "一码中特", "自由门", "李洪志", "大纪元", "真善忍", "新生网", "新唐人", "明慧", "无界浏览", "美国之音", "大紀元", "汕尾事件", "反中游行", "学生暴动", "死刑过程", "色空寺", "裸聊", "灭绝罪", "生成身份证", "身份证生成", "性用品", "性药品", "无界浏览器", "偷拍走光", "色情电影", "黄色电影", "黄色小电影", "rentiandiling", "xintangrendianshi", "全球春节晚会预告", "风骚少妇", "极品写真", "陈冠希裸照", "阿娇裸照", "裸体", "露点图片", "露点视频", "露点电影", "露点小电影", "漏点图片", "漏点电影", "漏点视频", "漏点小电影", "苹果被删", "胡锦涛和陈水扁对话", "两岸才子对话", "中国十大最恶心语录", "北京奥运会开幕式方案", "法轮", "法一轮", "阴唇", "阴户", "淫靡", "阴茎", "阴蒂", "乱伦", "手淫", "做鸡", "六四事件", "淫色", "K粉", "白粉", "大麻", "摇头丸", "出售猎枪", "手机定位", "钢珠手枪", "军用制式", "脱衣女郎", "脱衣美女", "脱衣MM", "赌球", "彩票预测", "球预测", "亚洲盘", "即时赔率", "移动电话拦截", "电表控制器", "针孔照相机", "防爆电击枪", "防爆喷雾剂", "出售气枪", "出售套牌车", "电子秤解码器", "地磅专用解码器", "电表专用调表器", "开锁专用工具", "DIY原子弹", "波佳达", "蜜洞", "菊花洞", "性学教授", "太王四神记", "凸点装", "国际足坛五大", "扶不起的阿斗", "出墙红杏之千岛湖之旅", "徐和柴学友", "遭警察殴打", "新诗年鉴", "李沛瑶", "美国凯德", "肥东交警", "针对台湾", "梁保华", "苏东解体", "反右题材", "信号拦截器", "山西洪洞", "巨额骗储", "东北风情熟女之惑", "海盗的悠闲生活", "黑天使", "混蛋神风流史", "秦青的幸福生活", "逃亡艳旅", "我的性启蒙老师", "现代艳帝传奇", "星光伴我淫", "倚天屠龙别记", "风月大陆", "美少妇的哀羞", "阿兵哥言语录", "遥想当年春衫薄", "神雕外传之郭襄", "睡着的武神", "汽车爆炸案", "周容重", "胡錦濤", "汕頭頻傳擄童割器官", "法輪功", "六决不", "清华网管", "卢跃刚", "最淫官员", "中国人都在上的四大当", "痛批政法委", "山西黑砖窑", "黑窑奴役", "敢坐飞机吗", "韩国身份证", "台湾身份证", "广安第二人民医院", "天鹅之旅", "玉蒲团", "践踏中国女性", "南京大学法学院", "挥发型迷药", "喷雾型迷药", "金伯帆", "崔英杰", "松花江污染", "仁寿警方", "愈快乐愈堕落麻醉枪", "唐人电视台", "金鳞岂是池中物", "江山美人志", "民警当副院长", "股市民谣", "卫星遭黑客攻击", "萬人暴", "官逼民反", "動乱", "军火价格", "劉奇葆", "女友坊", "奴役童工", "性奴", "奴事件", "二奶大奖赛", "性爱日记", "计生风暴", "厦门大游行", "二奶大赛", "纪股票市场五卅惨案", "毛爷爷复活", "智能H", "仿真假钞", "赣江学院", "江西田园置业集团", "高莺莺", "西藏禁书", "股民造反", "股民率先造反", "黑匣子最后", "抵制家乐福", "大恐怖杀手", "您的心被曾经的谎言", "金麟岂是池中物", "代理发票", "神州共震迎奥运", "三分钟禁播片", "日韩情欲电影", "夫妻两性电影少女人体图", "少女刺激电影", "全裸人体艺术图", "日本情欲", "妹妹艳情", "小妹妹艳爽", "少女浴室", "美女三点", "免费桃色", "激爽情欲", "夜生活夜电影", "美女午夜", "欧美情欲", "极品艳情", "国民党被推翻了么", "全国包二奶大赛", "高干子弟名单", "金晶的嫌疑人分析", "星爷搞笑发挥到如此境", "陈shuibian", "温jiabao", "胡jintao", "唐飞", "中國發表的東海開發", "民政局局长居然还摸", "福娃是魔咒", "股民暴动", "警察被指强奸女当事人", "四月圣火被搞怪", "必先撞其火车", "抢其火炬", "震其国土吓其国民", "中国是全球唯一绝对不", "个特权家庭垄断中国", "清海无上师", "代理国税", "代理海关", "代理地税", "龙的腾飞一定会有", "瓮安副县长", "出售工字牌气枪", "机打真发票", "刻章办证", "震后最流行幽默段子", "当今六大谎言", "东海协议", "当代无耻语录排行榜", "零八奥运会公式", "开平女生", "地下先烈们打电话", "阿扁推翻", "毛主席", "胡主席", "忠告股民", "法轮功", "忠告中国股民", "代开", "代办", "财税代理", "各类发票", "税务代理", "窃聽器", "楼主是猪" };
            for (int i = 0; i < badWords.Length; i++)
            {
                content = content.Replace(badWords[i], "");
            }
            return content;
        }

        /// <summary>
        /// 处理不规则的发布时间格式
        /// </summary>
        /// <param name="pubTime"></param>
        /// <returns></returns>
        public static string FormatPubTime(string pubTime)
        {
            var pubDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            try
            {
                pubDate = Convert.ToDateTime(pubTime).ToString("yyyy-MM-dd HH:mm");
            }
            catch (Exception ex)
            {
                var temp = pubTime.Replace("年", "-").Replace("月", "-").Replace("日", " ").Replace("/", "-").Replace("  ", " ");
                if (temp.Length > 16)
                {
                    temp = temp.Substring(0, 16);
                }
                temp = temp.Trim();
                if (temp.Length == 10)
                {
                    temp = temp + " " + DateTime.Now.ToString("HH:mm");
                }

                try
                {
                    pubDate = Convert.ToDateTime(temp).ToString("yyyy-MM-dd HH:mm");
                }
                catch (Exception ex1)
                {
                    pubDate = temp;
                    if (string.IsNullOrWhiteSpace(temp))
                    {
                        pubDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    }
                }
            }
            return pubDate;
        }
        #endregion

        #region ===mac===
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]

        private static extern Int32 inet_addr(string ip);

        /// <summary>
        /// 获取客户端的mac地址，取不到时取ip
        /// </summary>
        /// <returns></returns>
        public static string GetMac(string userip, string strClientIp)
        {
            var mac = "";
            try
            {
                var ldest = inet_addr(strClientIp); //目的地的ip 
                var macinfo = new Int64();
                var len = 6;
                var res = SendARP(ldest, 0, ref macinfo, ref len);
                var macSrc = macinfo.ToString("X");
                if (macSrc == "0")
                {
                    mac = GetIp();
                }

                while (macSrc.Length < 12)
                {
                    macSrc = macSrc.Insert(0, "0");
                }

                var macDest = "";

                for (var i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            macDest = macDest.Insert(0, macSrc.Substring(i, 2));
                        }
                        else
                        {
                            macDest = "-" + macDest.Insert(0, macSrc.Substring(i, 2));
                        }
                    }
                }
                mac = macDest;
            }
            catch
            {
                mac = GetIp();
            }
            if (mac.Length < 1)
                mac = GetIp();
            if (mac == "00-00-00-00-00-00")
            {

                mac = GetIp();

            }
            if (mac.Length > 30)
                mac = mac.Substring(0, 30);
            return mac;
        }

        #endregion

        #region ===unicode===
        /// <summary>
        /// 中文转unicode
        /// </summary>
        /// <returns></returns>
        public static string Chinese_To_Unicode(string str)
        {
            var outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (var i = 0; i < str.Length; i++)
                {
                    outStr += "/u" + ((int)str[i]).ToString("x");
                }
            }
            return outStr;
        }
        /// <summary>
        /// unicode转中文
        /// </summary>
        /// <returns></returns>
        public static string Unicode_To_Chinese(string str)
        {
            var outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                var strlist = str.Replace("/", "").Split('u');
                try
                {
                    for (var i = 1; i < strlist.Length; i++)
                    {
                        //将unicode字符转为10进制整数，然后转为char中文字符  
                        outStr += (char)int.Parse(strlist[i], NumberStyles.HexNumber);
                    }
                }
                catch (FormatException ex)
                {
                    outStr = ex.Message;
                }
            }
            return outStr;
        }


        /// <summary>
        /// unicode转中文（符合js规则的）
        /// </summary>
        /// <returns></returns>
        public static string Unicode_To_Chinese_Js(string str)
        {
            var outStr = "";
            var reg = new Regex(@"(?i)\\u([0-9a-f]{4})");
            outStr = reg.Replace(str, delegate(Match m1)
            {
                return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();
            });
            return outStr;
        }
        /// <summary>
        /// 中文转unicode（符合js规则的）
        /// </summary>
        /// <returns></returns>
        public static string Chinese_To_Unicode_Js(string str)
        {
            var outStr = "";
            var a = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (var i = 0; i < str.Length; i++)
                {
                    if (Regex.IsMatch(str[i].ToString(), @"[\u4e00-\u9fa5]")) { outStr += "\\u" + ((int)str[i]).ToString("x"); }
                    else { outStr += str[i]; }
                }
            }
            return outStr;
        } 
        #endregion

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

        #region === other ===

        /// <summary>
        /// 可枚举类型转化为字符串，类似于js中数组的join函数
        /// </summary>
        public static string Join<T>(IEnumerable<T> ss, string tag = "")
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(tag))
            {
                foreach (var s in ss)
                {
                    sb.Append(s);
                }
            }
            else
            {
                foreach (var s in ss)
                {
                    sb.Append(tag).Append(s);
                }
                if (sb.Length >= tag.Length)
                {
                    sb.Remove(0, tag.Length);
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}
