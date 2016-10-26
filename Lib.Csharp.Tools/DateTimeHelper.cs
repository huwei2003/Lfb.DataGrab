using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Csharp.Tools.Base;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// 时间，日期类的工具方法
    /// </summary>
    public class DateTimeHelper
    {
        public static DateTime UtcBeginTime = new DateTime(1970, 1, 1);
        /// <summary>
        /// UtcBeginTime.Ticks
        /// </summary>
        public static long UtcBeginTicks = 621355968000000000L;

        /// <summary>  
        /// 获取指定时间的时间戳  10位时间戳(second) or 13位时间戳(millisecond)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="bflag">为真时获取10位时间戳(second),为假时获取13位时间戳(millisecond).</param>  
        /// <returns></returns>  
        public static long GetTimeStamp(DateTime dt,bool bflag = true)
        {
            var ts = dt.ToUniversalTime() - UtcBeginTime;
            var ret = 0L;
            if (bflag)
                ret = Convert.ToInt64(ts.TotalSeconds);
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds);

            return ret;
        }

        /// <summary>
        /// 获取正确的时间格式 2010/05/15 12:25:36
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetFormatDate(DateTime dt)
        {
            var str = string.Format("{0}/{1}/{2} {3}:{4}:{5}", dt.Year.ToString("0000"), dt.Month.ToString("00"), dt.Day.ToString("00"),dt.Hour.ToString("00"), dt.Minute.ToString("00"),dt.Second.ToString("00"));
            return str;
        }
        /// <summary>
        /// 获取正确的时间格式 2010-05-15 12:25:36 112
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetFormatLongDate(DateTime dt)
        {
            //var str = string.Format("{0}/{1}/{2} {3}:{4}:{5}", dt.Year.ToString("0000"), dt.Month.ToString("00"), dt.Day.ToString("00"), dt.Hour.ToString("00"), dt.Minute.ToString("00"), dt.Second.ToString("00"));
            return dt.ToString("yyyy-MM-dd HH:mm:ss zzz");
        }
        /// <summary>
        /// 返回GMT格式时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetGmtTime(DateTime dt)
        {
            return dt.ToString("r");
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
        //(payCreate.ExpiredTime.Ticks - 621355968000000000)/10000000
        //parameters.Add("Timestamp", DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"));
        //请求的时间戳。日期格式按照ISO8601标准表示，并需要使用UTC时间。格式为：YYYY-MM-DDThh:mm:ssZ例如，2014-7-29T12:00:00Z(为北京时间2014年7月29日的20点0分0秒

        /// <summary>
        /// 将整形时间（到1970的秒数）转为datetime格式
        /// </summary>
        /// <param name="lData"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(long lData)
        {
            //621355968000000000 is UtcBeginTime.Ticks 100毫微秒为单位
            
            DateTime datetimew = DateTime.Now;
            try
            {
                //lData*10000000 second to ticks 1s=10000000 ticks
                datetimew = new DateTime(lData*10000000 + 621355968000000000);
            }
            catch
            {

            }
            return datetimew;
        }

        /// <summary>
        /// 日期格式按照ISO8601标准表示，并需要使用UTC时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetUtcDateTime(DateTime dt)
        {

            var datetimew = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
            try
            {
                datetimew = dt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            catch
            {

            }
            return datetimew;
        }
        /// <summary>
        /// 格式化成中文日期及时间，1 short 2 long
        /// </summary>
        /// <param name="strDate">转入的字符串</param>
        /// <param name="iType">为类型，1 表示为2010年4月14日;2 为2010年4月14日 9时8分25秒</param>
        /// <returns></returns>
        public static string GetChineseDate(string strDate, int iType)
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
                }
                else if (iType == 2)
                {
                    str = string.Format("{0}年{1}月{2}日 {3}时{4}分{5}秒", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute,dt.Second);
                }
            }
            catch
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// 两个日期相减，第二个减第一个，得出天数
        /// </summary>
        /// <param name="strDateS">开始日期</param>
        /// <param name="strDateE">结束日期</param>
        /// <returns></returns>
        public static int DateSubtract(string strDateS, string strDateE)
        {
            if (strDateS.Trim() == "" || strDateE.Trim() == "")
            {
                return 0;
            }

            TimeSpan ts;
            int days = 0;
            try
            {
                DateTime dtS = Convert.ToDateTime(strDateS);
                DateTime dtE = Convert.ToDateTime(strDateE);

                ts = dtE - dtS;

                days = ((int)ts.Days) + 1;
            }
            catch
            {
            }

            return days;
        }

        /// <summary>
        /// 设置本机时间-将系统时间修改为指定的时间
        /// </summary>
        /// <param name="dt">指定的时间</param>
        public static void SetServerTime(DateTime dt)
        {
            SNTPTimeClient.SetTime(dt);
        }

        /// <summary>
        /// /// <summary>
        /// 时间还原为当前时间
        /// </summary>
        /// <param name="ip">时间服务器ip地址 202.120.2.101 上海交通大学网络中心NTP服务器地址</param>
        /// <param name="port">时间服务器服务port 123 is default port</param>
        /// <returns></returns>
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static async Task ReSetServerTime(string ip, string port)
        {
            await SNTPTimeClient.ResetTime(ip,port);
        }

        /// <summary>
        /// TimeSpan 耗时中文格式显示
        /// </summary>
        public static string ToUseSimpleTime(TimeSpan ts)
        {
            string s;

            if (ts.TotalDays > 1D)
            {
                s = string.Format("耗时 {0} 天", ts.TotalDays.ToString("0.00"));
            }
            else if (ts.TotalHours > 1D)
            {
                s = string.Format("耗时 {0} 小时", ts.TotalHours.ToString("0.00"));
            }
            else if (ts.TotalMinutes > 1D)
            {
                s = string.Format("耗时 {0} 分钟", ts.TotalMinutes.ToString("0.00"));
            }
            else if (ts.TotalSeconds > 1D)
            {
                s = string.Format("耗时 {0} 秒钟", ts.TotalSeconds.ToString("0.00"));
            }
            else
            {
                s = string.Format("耗时 {0} 毫秒", ts.TotalMilliseconds.ToString("0.00"));
            }

            return s;
        }
    }
}
