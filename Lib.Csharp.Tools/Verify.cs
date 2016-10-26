using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// 验证类
    /// </summary>
    public class Verify
    {
        #region 验证文本框输入为数字
        /// <summary>
        /// 验证是不是数字（包含整数和小数）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNum(string str)
        {
            return Regex.IsMatch(str, @"^[-]?\d+[.]?\d*$");
        }
        #endregion

        #region 验证文本框输入为整数
        /// <summary>
        /// 验证文本框输入为整数
        /// </summary>
        /// <param name="strNum">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsInteger(string strNum)
        {
            return Regex.IsMatch(strNum, "^[0-9]*$");
        }
        #endregion

        #region 验证文本框输入为日期
        /// <summary>
        /// 判断日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsValidDate(string date)
        {
            //验证YYYY-MM-DD格式,基本上把闰年和2月等的情况都考虑进去
            bool bValid = Regex.IsMatch(date, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
            return (bValid && System.String.Compare(date, "1753-01-01", System.StringComparison.Ordinal) >= 0);

            //将平年和闰年的日期验证表达式合并，我们得到最终的验证日期格式为YYYY-MM-DD的正则表达式为：

            //(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|
            //[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-
            //(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|
            //(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|
            //[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)


        }

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"（\d{4}）-（\d{1,2}）-（\d{1,2}）");
        }

        #endregion

        #region 验证文本框输入为电子邮件
        //验证电子邮件
        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

        #region  验证文本框输入为电话号码
        /// <summary>
        /// 验证文本框输入为电话号码
        /// </summary>
        /// <param name="strPhone">输入字符串</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsPhone(string strPhone)
        {
            return Regex.IsMatch(strPhone, @"\d{3,4}-\d{7,8}");
        }
        #endregion

        #region  验证文本框输入为传真号码
        /// <summary>
        /// 验证文本框输入为传真号码
        /// </summary>
        /// <param name="strFax">输入字符串</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsFax(string strFax)
        {
            return Regex.IsMatch(strFax, @"86-\d{2,3}-\d{7,8}");
        }
        #endregion



        #region 验证文本是否由字母数字组成的普通字符串
        //验证电子邮件
        public static bool IsNormalString(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[0-9a-zA-Z]*$");
        }
        #endregion
    }
}
