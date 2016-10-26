using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Comm.Tools.Utility
{
    public sealed class Log
    {
        private readonly string _logPath;

        private bool IsDebugLog
        {
            get
            {
                var isDebugLog = Cache.GetCache(() =>
                {
                    try
                    {
                        ConfigurationManager.RefreshSection("appSettings");
                        return ConfigurationManager.AppSettings["IsDebugLog"].ConvertTo(false);
                    }
                    catch (Exception e)
                    {
                        Error(e.Message + e.StackTrace);
                        return false;
                    }
                }, 60);
                return isDebugLog;
            }
        }

        private static readonly bool IsInfoLog;

        static Log()
        {
            IsInfoLog = ConfigurationManager.AppSettings["IsInfoLog"].ConvertTo(true);
        }

        public Log(string logPath = "")
        {
            _logPath = logPath;
            var path = AppDomain.CurrentDomain.BaseDirectory + @"App_Log\{0}".Formats(logPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void Error(string strLog, bool isHour = false)
        {
            Write(@"{0}\Error".Formats(_logPath), strLog, isHour);
        }
        public void Error(Exception e, bool isHour = false)
        {
            Write(@"{0}\Error".Formats(_logPath), e.Message + e.StackTrace, isHour);
        }
        public void Error(string dir, string strLog, bool isHour = false)
        {
            Write(@"{0}\{1}\Error".Formats(_logPath, dir), strLog, isHour);
        }

        public void Debug(string strLog, bool isHour = false)
        {
            if (IsDebugLog)
            {
                Write(@"{0}\Debug".Formats(_logPath), strLog, isHour);
            }
        }
        public void Debug(string dir, string strLog, bool isHour = false)
        {
            if (IsDebugLog)
            {
                Write(@"{0}\{1}\Debug".Formats(_logPath, dir), strLog, isHour);
            }
        }

        public void Info(string strLog, bool isHour = false)
        {
            if (IsInfoLog)
            {
                Write(@"{0}\Info".Formats(_logPath), strLog, isHour);
            }
        }
        public void Info(string dir, string strLog, bool isHour = false)
        {
            if (IsInfoLog)
            {
                Write(@"{0}\{1}\Info".Formats(_logPath, dir), strLog, isHour);
            }
        }

        public void Warn(string strLog, bool isHour = false)
        {
            Write(@"{0}\Warn".Formats(_logPath), strLog, isHour);
        }
        public void Warn(string dir, string strLog, bool isHour = false)
        {
            Write(@"{0}\{1}\Warn".Formats(_logPath, dir), strLog, isHour);
        }

        public void Monitor(string strLog, bool isHour = false)
        {
            Write(@"{0}\Monitor".Formats(_logPath), strLog, isHour);
        }
        public void Monitor(string dir, string strLog, bool isHour = false)
        {
            Write(@"{0}\{1}\Monitor".Formats(_logPath, dir), strLog, isHour);
        }

        private static void Write(string logType, string strLog, bool isHour = false)
        {
            var logStr = new StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff | "));
            try
            {
                var trace = new StackTrace();
                var frames = trace.GetFrames().Reverse().ToList();
                frames = frames.Skip(frames.FindLastIndex(a => a.GetMethod().ReflectedType.FullName.StartsWith("System.")) + 1).ToList();
                foreach (var frame in frames)
                {
                    var method = frame.GetMethod();
                    if (method.ReflectedType.FullName.StartsWith("Utility."))
                    {
                        continue;
                    }
                    logStr.Append(method.ReflectedType.FullName).Append(".").Append(method.Name).Append(" -> ");
                }
            }
            catch
            {
            }

            var logDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\App_Log\" + logType + @"\";
            string logFile;
            if (isHour)
            {
                logFile = logDirectory + DateTime.Now.ToString("yyyy-MM-dd-HH") + ".log";
            }
            else
            {
                logFile = logDirectory + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            }

            Mutex mutexFile = null;
            try
            {
                mutexFile = new Mutex(false, logFile.Md5());
                mutexFile.WaitOne();
            }
            catch
            {
            }

            try
            {
                if (strLog != null)
                {
                    logStr.Append(strLog);
                }
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
                using (var fs = new FileStream(logFile, FileMode.Append, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        writer.WriteLine(logStr.ToString());
                    }
                }
            }
            catch
            {
            }

            if (mutexFile != null)
            {
                try
                {
                    mutexFile.ReleaseMutex();
                    mutexFile.Close();
                }
                catch
                {
                }
            }
        }
    }
}