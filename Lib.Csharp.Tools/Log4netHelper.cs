using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// Log4Net 日志操作类
    /// </summary>
    public class Log4NetHelper
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger("Admin");

         static Log4NetHelper()
        {
            var logConfig = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/log4net.config";
            XmlConfigurator.ConfigureAndWatch(
                new FileInfo(logConfig));
        }

        public static void Debug(string message)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug(message);
            }
        }
        /// <summary>
        /// 记录异常的相关信息,log4net
        /// </summary>
        /// <param name="ex1"></param>
        public static void Debug(System.Exception ex1)
        {
            if (Log.IsDebugEnabled)
            {
                if (ex1 != null)
                {
                    Log.Debug(ex1.Message.ToString() + "\r\n" + ex1.Source.ToString() + "\r\n" + ex1.TargetSite.ToString() + "\r\n" + ex1.StackTrace.ToString());
                }
            }

        }
        public static void Error(string message)
        {
            if (Log.IsErrorEnabled)
            {
                Log.Error(message);
            }
        }
        public static void Fatal(string message)
        {

            if (Log.IsFatalEnabled)
            {
                Log.Fatal(message);
            }
        }
        public static void Info(string message)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(message);
            }
        }

        public static void Warn(string message)
        {
            if (Log.IsWarnEnabled)
            {
                Log.Warn(message);
            }
        }
    }
}
