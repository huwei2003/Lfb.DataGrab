using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Comm.Tools.Utility
{
    public static class ThreadHelper
    {
        static readonly Log Log = new Log("System");

        /// <summary>
        /// 开启同步多线程
        /// </summary>
        public static void StartSync(this IEnumerable<Thread> threads, object startPara = null, Func<object, object> callback = null)
        {
            var ts = threads.ToArray();
            //启动线程
            foreach (var thread in ts)
            {
                if (!thread.IsBackground)
                {
                    thread.IsBackground = true;
                }
                var times = 0;
                while (thread.ThreadState == (ThreadState.Background | ThreadState.Unstarted) && times < 10)
                {
                    try
                    {
                        if (startPara == null)
                        {
                            thread.Start();
                        }
                        else
                        {
                            thread.Start(startPara);
                        }
                    }
                    catch (Exception e)
                    {
                        times++;
                        Log.Error(e.Message + e.StackTrace);
                    }
                    Thread.Sleep(100);
                }
            }
            Thread.Sleep(2000);
            //等待全部结束
            foreach (var thread in ts)
            {
                try
                {
                    thread.Join();
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + e.StackTrace);
                }
            }
            if (callback != null)
            {
                callback(startPara);
            }
        }

        /// <summary>
        /// 开启多线程
        /// </summary>
        public static void StartAsync(this IEnumerable<Thread> threads, object startPara = null, Func<object, object> callback = null)
        {
            var ts = threads.ToArray();
            //启动线程
            foreach (var thread in ts)
            {
                if (!thread.IsBackground)
                {
                    thread.IsBackground = true;
                }
                var times = 0;
                while (thread.ThreadState == (ThreadState.Background | ThreadState.Unstarted) && times < 10)
                {
                    try
                    {
                        if (startPara == null)
                        {
                            thread.Start();
                        }
                        else
                        {
                            thread.Start(startPara);
                        }
                    }
                    catch (Exception e)
                    {
                        times++;
                        Log.Error(e.Message + e.StackTrace);
                    }
                    Thread.Sleep(100);
                }
            }
            if (callback != null)
            {
                callback(startPara);
            }
        }

        /// <summary>
        /// 开启同步线程
        /// </summary>
        public static void StartSync(this Thread thread, object parameter = null)
        {
            try
            {
                if (!thread.IsBackground)
                {
                    thread.IsBackground = true;
                }
                if (parameter == null)
                {
                    thread.Start();
                }
                else
                {
                    thread.Start(parameter);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.Message);
            }
            Thread.Sleep(1000);
            try
            {
                thread.Join();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
        }

        /// <summary>
        /// 开启带超时的同步线程
        /// </summary>
        public static void StartSyncTimeout(this Thread thread, int timeoutSeconds, object parameter = null)
        {
            try
            {
                if (!thread.IsBackground)
                {
                    thread.IsBackground = true;
                }
                if (parameter == null)
                {
                    thread.Start();
                }
                else
                {
                    thread.Start(parameter);
                }
                thread.Join(timeoutSeconds * 1000);
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.Message);
            }
        }

        /// <summary>
        /// 开启异步线程
        /// </summary>
        public static void StartAsync(this Thread thread, object parameter = null)
        {
            try
            {
                if (!thread.IsBackground)
                {
                    thread.IsBackground = true;
                }
                if (parameter == null)
                {
                    thread.Start();
                }
                else
                {
                    thread.Start(parameter);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.Message);
            }
        }
    }
}
