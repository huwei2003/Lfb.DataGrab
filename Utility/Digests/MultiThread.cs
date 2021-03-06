﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using ThreadState = System.Threading.ThreadState;

namespace Comm.Tools.Utility
{
    /// <summary>
    /// 多线程
    /// </summary>
    public abstract class MultiThread<T>
    {
        private static readonly string TypeName = typeof(T).Name;
        private static readonly double WornSeconds = 10D;
        /// <summary>
        /// 线程
        /// </summary>
        internal static readonly List<ThreadInfo> ThreadInfos = new List<ThreadInfo>();

        /// <summary>
        /// 日志
        /// </summary>
        private static readonly Log Log = new Log("Tasks");
        /// <summary>
        /// 停止前调用函数
        /// </summary>
        private static readonly List<Func<bool>> ExitFuncs = new List<Func<bool>>();

        static MultiThread()
        {
            AddTask(ThreadMonitor, 10);
        }

        #region 启动&停止

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="type">0：异步启动1:同步启动</param>
        public static void Start(int type = 0)
        {
            try
            {
                typeof(T).GetConstructors().First().Invoke(new object[0]);
                new Thread(Begin).Start(type);
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
        }
        /// <summary>
        /// 启动
        /// </summary>
        private static void Begin(object type)
        {
            try
            {
                foreach (var threadInfo in ThreadInfos)
                {
                    if (!threadInfo.Thread.IsBackground)
                    {
                        threadInfo.Thread.IsBackground = true;
                    }
                    var times = 0;
                    while (threadInfo.Thread.ThreadState == (ThreadState.Background | ThreadState.Unstarted) && times < 10)
                    {
                        try
                        {
                            if (threadInfo.IsParameterized)
                            {
                                threadInfo.Thread.Start(threadInfo.Parameter);
                            }
                            else
                            {
                                threadInfo.Thread.Start();
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

                Log.Info(TypeName, "启动成功，共启动个{0}线程".Formats(ThreadInfos.Count));
                if (type.ConvertTo(0) != 1)
                {
                    return;
                }
                Thread.Sleep(1000);
                foreach (var thread in ThreadInfos)
                {
                    try
                    {
                        thread.Thread.Join();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message + e.StackTrace);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
        }

        private static void End()
        {
            try
            {
                Log.Info(TypeName, "共 {0} 个线程".Formats(ThreadInfos.Count));

                while (ThreadInfos.Count(a => a.Thread != null && a.Thread.IsAlive) > 0)
                {
                    foreach (var thread in ThreadInfos)
                    {
                        if (thread.Thread.IsAlive)
                        {
                            thread.Thread.Abort();
                            Log.Info(TypeName, "正在终止线程 {0}".Formats(thread.ThreadName));
                        }
                    }

                    Log.Info(TypeName, "剩余 {0} 个未终止".Formats(ThreadInfos.Count(a => a != null && a.Thread.IsAlive)));

                    Thread.Sleep(100);
                }

                Log.Info(TypeName, "线程全部正常停止");
            }
            catch (Exception e)
            {
                Log.Error(TypeName, "{0} {1}".Formats(e.Message, e.StackTrace));
            }
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public static void Exit()
        {
            foreach (var func in ExitFuncs)
            {
                func();
            }
            new Thread(End).Start();
        }

        #endregion

        protected static void LogDebug(string log, bool isHour = false)
        {
            Log.Debug(TypeName, log, isHour);
        }
        protected static void LogInfo(string log, bool isHour = false)
        {
            Log.Info(TypeName, log, isHour);
        }
        protected static void LogWarn(string log, bool isHour = false)
        {
            Log.Warn(TypeName, log, isHour);
        }
        protected static void LogError(string log, bool isHour = false)
        {
            Log.Error(TypeName, log, isHour);
        }
        protected static void LogError(Exception e, bool isHour = false)
        {
            Log.Error(TypeName, e.Message + e.StackTrace, isHour);
        }
        protected static void LogMonitor(string log, bool isHour = false)
        {
            Log.Monitor(TypeName, log, isHour);
        }

        /// <summary>
        /// 添加线程任务
        /// </summary>
        /// <param name="threadStart">线程函数，注：无需while 和 try catch</param>
        /// <param name="waitSeconds">线程守候间隔秒</param>
        /// <param name="manualResetEvent">通知线程阻塞或唤醒</param>
        protected static void AddTask(ThreadStart threadStart, int waitSeconds, ManualResetEvent manualResetEvent = null)
        {
            lock (ThreadInfos)
            {
                var threadName = threadStart.Method.Name;
                var threadInfo = new ThreadInfo
                {
                    ThreadName = threadName,
                    ThreadStart = threadStart,
                    WaitSeconds = waitSeconds,
                    ManualResetEvent = manualResetEvent
                };
                threadInfo.Thread = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            LogMonitor("线程{0}执行开始".Formats(threadName));
                            var sw = Stopwatch.StartNew();
                            threadStart();
                            sw.Stop();
                            var ts = sw.Elapsed;
                            if (ts.TotalSeconds > WornSeconds)
                            {
                                LogWarn("线程{0}执行结束，{1}".Formats(threadName, ts.ToUseSimpleTime()));
                            }
                            LogMonitor("线程{0}执行结束，{1}".Formats(threadName, ts.ToUseSimpleTime()));
                        }
                        catch (Exception ex)
                        {
                            LogError("线程{0}执行异常:{1} {2}".Formats(threadName, ex.Message, ex.StackTrace));
                        }
                        if (threadInfo.WaitSeconds > 0)
                        {
                            if (threadInfo.ManualResetEvent != null)
                            {
                                threadInfo.ManualResetEvent.Reset();
                                threadInfo.ManualResetEvent.WaitOne(1000 * threadInfo.WaitSeconds);
                            }
                            else
                            {
                                Thread.Sleep(1000 * threadInfo.WaitSeconds);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                });
                ThreadInfos.Add(threadInfo);
            }
        }

        /// <summary>
        /// 添加线程任务
        /// </summary>
        /// <param name="threadStart">线程函数，注：无需while 和 try catch</param>
        /// <param name="timePoint">线程守候每天触发时间点,如：每天5点</param>
        protected static void AddTask(ThreadStart threadStart, TimeSpan timePoint)
        {
            lock (ThreadInfos)
            {
                var threadName = threadStart.Method.Name;
                var threadInfo = new ThreadInfo
                {
                    ThreadName = threadName,
                    ThreadStart = threadStart,
                    TimePoint = timePoint
                };
                threadInfo.Thread = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            if (threadInfo.TimePoint == new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))
                            {
                                LogMonitor("线程{0}执行开始".Formats(threadName));
                                var sw = Stopwatch.StartNew();
                                threadStart();
                                sw.Stop();
                                var ts = sw.Elapsed;
                                if (ts.TotalSeconds > WornSeconds)
                                {
                                    LogWarn("线程{0}执行结束，{1}".Formats(threadName, ts.ToUseSimpleTime()));
                                }
                                LogMonitor("线程{0}执行结束，{1}".Formats(threadName, ts.ToUseSimpleTime()));
                            }
                        }
                        catch (Exception ex)
                        {
                            LogError("线程{0}执行异常:{1} {2}".Formats(threadName, ex.Message, ex.StackTrace));
                        }
                        Thread.Sleep(1000);
                    }
                });
                ThreadInfos.Add(threadInfo);
            }
        }

        /// <summary>
        /// 添加带参数的线程任务
        /// </summary>
        /// <param name="parameterizedThreadStart">线程函数，注：无需while 和 try catch</param>
        /// <param name="parameter">线程使用参数</param>
        /// <param name="waitSeconds">线程守候间隔秒</param>
        protected static void AddTask(ParameterizedThreadStart parameterizedThreadStart, object parameter, int waitSeconds)
        {
            lock (ThreadInfos)
            {
                var threadName = parameterizedThreadStart.Method.Name;
                var threadInfo = new ThreadInfo
                {
                    ThreadName = threadName,
                    IsParameterized = true,
                    WaitSeconds = waitSeconds,
                    Parameter = parameter,
                    ParameterizedThreadStart = parameterizedThreadStart
                };
                threadInfo.Thread = new Thread(p =>
                {
                    while (true)
                    {
                        try
                        {
                            LogMonitor("线程{0}({1})执行开始".Formats(threadName, p));
                            var sw = Stopwatch.StartNew();
                            parameterizedThreadStart(p);
                            sw.Stop();
                            var ts = sw.Elapsed;
                            if (ts.TotalSeconds > WornSeconds)
                            {
                                LogWarn("线程{0}({1})执行结束，{2}".Formats(threadName, p, sw.Elapsed.ToUseSimpleTime()));
                            }
                            LogMonitor("线程{0}({1})执行结束，{2}".Formats(threadName, p, sw.Elapsed.ToUseSimpleTime()));
                        }
                        catch (Exception ex)
                        {
                            LogError("线程{0}({1})执行异常:{2} {3}".Formats(threadName, p, ex.Message, ex.StackTrace));
                        }
                        if (threadInfo.WaitSeconds > 0)
                        {
                            Thread.Sleep(1000 * threadInfo.WaitSeconds);
                        }
                        else
                        {
                            break;
                        }
                    }
                });
                ThreadInfos.Add(threadInfo);
            }
        }

        /// <summary>
        /// 批量添加线程任务
        /// </summary>
        /// <param name="threadStarts">线程集合</param>
        /// <param name="waitSeconds">线程守候间隔秒</param>
        protected static void AddTasks(IEnumerable<ThreadStart> threadStarts, int waitSeconds)
        {
            foreach (var threadStart in threadStarts)
            {
                AddTask(threadStart, waitSeconds);
            }
        }

        /// <summary>
        /// 批量添加带参数的线程任务
        /// </summary>
        /// <param name="threadStarts">线程集合</param>
        /// <param name="waitSeconds">线程守候间隔秒</param>
        protected static void AddTasks(IEnumerable<KeyValuePair<ParameterizedThreadStart, object>> threadStarts, int waitSeconds)
        {
            foreach (var kv in threadStarts)
            {
                AddTask(kv.Key, kv.Value, waitSeconds);
            }
        }

        /// <summary>
        /// 添加退出时执行任务
        /// </summary>
        protected static void AddExitCall(Func<bool> func)
        {
            ExitFuncs.Add(func);
        }

        /// <summary>
        /// 线程监控，停止自动启动
        /// </summary>
        private static void ThreadMonitor()
        {
            while (true)
            {
                Thread.Sleep(10000);
                try
                {
                    var r = ThreadInfos.Count(a => a.Thread != null && a.Thread.IsAlive);
                    var e = ThreadInfos.Count(a => a.Thread == null || !a.Thread.IsAlive);
                    if (e > 0)
                    {
                        var es = ThreadInfos.Where(a => a.Thread == null || !a.Thread.IsAlive).ToArray();
                        var errThreadNames = es.Select(a => a.ThreadName).ToArray();

                        LogWarn("线程共{0}个，正常{1}个，异常{2}个，异常线程：{3}".Formats(ThreadInfos.Count, r, e, errThreadNames.Join(",")));
                        LogWarn("正在重启线程{0}".Formats(errThreadNames.Join(",")));

                        foreach (var thread in es)
                        {
                            var ti = ThreadInfos[ThreadInfos.FindIndex(a => a == thread)];
                            if (ti.IsParameterized)
                            {
                                ti.Thread = new Thread(p =>
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            ti.ParameterizedThreadStart(p);
                                            LogMonitor("线程{0}({1})执行正常".Formats(ti.ThreadName, p));
                                        }
                                        catch (Exception ex)
                                        {
                                            LogError("线程{0}({1})执行异常:{2} {3}".Formats(ti.ThreadName, p, ex.Message, ex.StackTrace));
                                        }
                                        if (ti.WaitSeconds > 0)
                                        {
                                            Thread.Sleep(1000 * ti.WaitSeconds);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                });
                                ti.Thread.Start(ti.Parameter);
                            }
                            else
                            {
                                ti.Thread = new Thread(() =>
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            ti.ThreadStart();
                                            LogInfo("线程{0}执行正常".Formats(ti.ThreadName));
                                        }
                                        catch (Exception ex)
                                        {
                                            LogError("线程{0}执行异常:{1} {2}".Formats(ti.ThreadName, ex.Message, ex.StackTrace));
                                        }
                                        if (ti.WaitSeconds > 0)
                                        {
                                            Thread.Sleep(1000 * ti.WaitSeconds);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                });
                                ti.Thread.Start();
                            }
                            Thread.Sleep(1000);
                        }
                    }
                }
                catch (Exception e)
                {
                    LogError(e.Message + e.StackTrace);
                }
            }
        }
    }

    public class ThreadInfo
    {
        /// <summary>
        /// 线程名称
        /// </summary>
        public string ThreadName;

        /// <summary>
        /// 线程实体
        /// </summary>
        public Thread Thread;

        /// <summary>
        /// 线程执行函数(不带参数)
        /// </summary>
        public ThreadStart ThreadStart;

        /// <summary>
        /// 是否带参数
        /// </summary>
        public bool IsParameterized;

        /// <summary>
        /// 线程执行函数(带参数)
        /// </summary>
        public ParameterizedThreadStart ParameterizedThreadStart;

        /// <summary>
        /// 参数
        /// </summary>
        public object Parameter;

        /// <summary>
        /// 线程守候间隔秒
        /// </summary>
        public int WaitSeconds;

        /// <summary>
        /// 线程守候时间点
        /// </summary>
        public TimeSpan TimePoint;

        /// <summary>
        /// 通知线程阻塞或唤醒
        /// </summary>
        public ManualResetEvent ManualResetEvent;
    }
}
