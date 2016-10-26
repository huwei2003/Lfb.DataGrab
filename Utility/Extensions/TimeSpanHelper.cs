using System;

namespace Comm.Tools.Utility
{
    public static class TimeSpanHelper
    {
        /// <summary>
        /// TimeSpan 耗时中文格式显示
        /// </summary>
        public static string ToUseSimpleTime(this TimeSpan ts)
        {
            string s;

            if (ts.TotalDays > 1D)
            {
                s = "耗时 {0} 天".Formats(ts.TotalDays.ToString("0.00"));
            }
            else if (ts.TotalHours > 1D)
            {
                s = "耗时 {0} 小时".Formats(ts.TotalHours.ToString("0.00"));
            }
            else if (ts.TotalMinutes > 1D)
            {
                s = "耗时 {0} 分钟".Formats(ts.TotalMinutes.ToString("0.00"));
            }
            else if (ts.TotalSeconds > 1D)
            {
                s = "耗时 {0} 秒钟".Formats(ts.TotalSeconds.ToString("0.00"));
            }
            else
            {
                s = "耗时 {0} 毫秒".Formats(ts.TotalMilliseconds.ToString("0.00"));
            }

            return s;
        }
    }
}
