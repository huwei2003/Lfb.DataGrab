using System;
using System.Globalization;

namespace Comm.Tools.Utility
{
    public static class DateTimeFormat
    {
        /// <summary>
        /// 介于start-end之间
        /// </summary>
        public static bool Between(this DateTime source, DateTime start, DateTime end)
        {
            return source >= start && source <= end;
        }

        public static string ToChineseDate(this DateTime dt)
        {
            if (!(dt == DateTime.MinValue) && !(dt == DateTime.MaxValue))
            {
                return dt.ToString("yyyy年MM月dd日");
            }
            return "";
        }

        public static string ToChineseDateTime(this DateTime dt)
        {
            if (!(dt == DateTime.MinValue) && !(dt == DateTime.MaxValue))
            {
                return dt.ToString("yyyy年MM月dd日HH时mm分ss秒");
            }
            return "";
        }

        public static string ToChineseDayOfWeek(this DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "星期日";

                case DayOfWeek.Monday:
                    return "星期一";

                case DayOfWeek.Tuesday:
                    return "星期二";

                case DayOfWeek.Wednesday:
                    return "星期三";

                case DayOfWeek.Thursday:
                    return "星期四";

                case DayOfWeek.Friday:
                    return "星期五";

                case DayOfWeek.Saturday:
                    return "星期六";
            }
            return "";
        }

        public static string ToChineseDayOfWeek2(this DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "周日";

                case DayOfWeek.Monday:
                    return "周一";

                case DayOfWeek.Tuesday:
                    return "周二";

                case DayOfWeek.Wednesday:
                    return "周三";

                case DayOfWeek.Thursday:
                    return "周四";

                case DayOfWeek.Friday:
                    return "周五";

                case DayOfWeek.Saturday:
                    return "周六";
            }
            return "";
        }
        public static string ToChineseDayOfNum(this DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "7";

                case DayOfWeek.Monday:
                    return "1";

                case DayOfWeek.Tuesday:
                    return "2";

                case DayOfWeek.Wednesday:
                    return "3";

                case DayOfWeek.Thursday:
                    return "4";

                case DayOfWeek.Friday:
                    return "5";

                case DayOfWeek.Saturday:
                    return "6";
            }
            return "";
        }

        public static string ToEnglishDate(this DateTime dt)
        {
            if (!(dt.Date == DateTime.MinValue.Date) && !(dt.Date == DateTime.MaxValue.Date))
            {
                return dt.ToString("yyyy-MM-dd");
            }
            return "";
        }

        public static string ToEnglishDateTime(this DateTime dt)
        {
            if (!(dt.Date == DateTime.MinValue.Date) && !(dt.Date == DateTime.MaxValue.Date))
            {
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return "";
        }

        public static string OleDateTimeToString(this string oleDate, string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (oleDate.IsNullOrEmpty()) return "";
            double d;
            return double.TryParse(oleDate, out d) ? DateTime.FromOADate(d).ToString(format) : Convert.ToDateTime(oleDate).ToString(format);
        }

        /// <summary>
        /// 中欧夏令时转北京时间  CEST – Central European Summer Time(Daylight Saving Time)
        /// </summary>
        public static DateTime CestToDateTime(this string cest)
        {
            return cest.Replace(" CEST", "").ToDateTime().AddHours(6);
        }

        /// <summary>
        /// js时间
        /// </summary>
        /// <returns></returns>
        public static long ToJsTime(this DateTime dt)
        {
            var ts = DateTime.Now.Subtract(DateTime.Parse("1970-01-01"));
            return ts.TotalMilliseconds.ToString(CultureInfo.InvariantCulture).Split('.')[0].ToLong();
        }
    }
}