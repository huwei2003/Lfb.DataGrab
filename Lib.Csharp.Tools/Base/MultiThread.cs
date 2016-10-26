using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadState = System.Threading.ThreadState;

namespace Lib.Csharp.Tools.Base
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
                Log.Error(e);
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
                            Log.Error(e);
                        }
                        Thread.Sleep(100);
                    }
                }

                Log.Info(string.Format("{0}启动成功，共启动个{1}线程", TypeName,ThreadInfos.Count));
                if (StrHelper.ToInt32(type.ToString()) != 1)
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
                        Log.Error(e);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private static void End()
        {
            try
            {
                Log.Info(string.Format("{0}共 {1} 个线程", TypeName,ThreadInfos.Count));

                while (ThreadInfos.Count(a => a.Thread != null && a.Thread.IsAlive) > 0)
                {
                    foreach (var thread in ThreadInfos)
                    {
                        if (thread.Thread.IsAlive)
                        {
                            thread.Thread.Abort();
                            Log.Info(string.Format("{0}正在终止线程 {1}", TypeName,thread.ThreadName));
                        }
                    }

                    Log.Info(string.Format("{0}剩余 {1} 个未终止", TypeName,ThreadInfos.Count(a => a != null && a.Thread.IsAlive)));

                    Thread.Sleep(100);
                }

                Log.Info(string.Format("{0}线程全部正常停止",TypeName));
            }
            catch (Exception e)
            {
                Log.Error(string.Format("{0},{1} {2}", TypeName,e.Message, e.StackTrace));
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
                            Log.Monitor(string.Format("线程{0}执行开始",threadName));
                            var sw = Stopwatch.StartNew();
                            threadStart();
                            sw.Stop();
                            var ts = sw.Elapsed;
                            if (ts.TotalSeconds > WornSeconds)
                            {
                                Log.Warn(string.Format("线程{0}执行结束，{1}",threadName, DateTimeHelper.ToUseSimpleTime(ts)));
                            }
                            Log.Monitor(string.Format("线程{0}执行结束，{1}",threadName, DateTimeHelper.ToUseSimpleTime(ts)));
                        }
                        catch (Exception ex)
                        {
                            Log.Error(string.Format("线程{0}执行异常:{1} {2}",threadName, ex.Message, ex.StackTrace));
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
                                Log.Monitor(string.Format("线程{0}执行开始",threadName));
                                var sw = Stopwatch.StartNew();
                                threadStart();
                                sw.Stop();
                                var ts = sw.Elapsed;
                                if (ts.TotalSeconds > WornSeconds)
                                {
                                    Log.Warn(string.Format("线程{0}执行结束，{1}",threadName, DateTimeHelper.ToUseSimpleTime(ts)));
                                }
                                Log.Monitor(string.Format("线程{0}执行结束，{1}",threadName, DateTimeHelper.ToUseSimpleTime(ts)));
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(string.Format("线程{0}执行异常:{1} {2}",threadName, ex.Message, ex.StackTrace));
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
                            Log.Monitor(string.Format("线程{0}({1})执行开始", threadName, p));
                            var sw = Stopwatch.StartNew();
                            parameterizedThreadStart(p);
                            sw.Stop();
                            var ts = sw.Elapsed;
                            if (ts.TotalSeconds > WornSeconds)
                            {
                                Log.Warn(string.Format("线程{0}({1})执行结束，{2}",threadName, p, DateTimeHelper.ToUseSimpleTime( sw.Elapsed)));
                            }
                            Log.Monitor(string.Format("线程{0}({1})执行结束，{2}",threadName, p, DateTimeHelper.ToUseSimpleTime(sw.Elapsed)));
                        }
                        catch (Exception ex)
                        {
                            Log.Error(string.Format("线程{0}({1})执行异常:{2} {3}",threadName, p, ex.Message, ex.StackTrace));
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

                        Log.Warn(string.Format("线程共{0}个，正常{1}个，异常{2}个，异常线程：{3}",ThreadInfos.Count, r, e, StrHelper.Join(errThreadNames,",")));
                        Log.Warn(string.Format("正在重启线程{0}",StrHelper.Join(errThreadNames,",")));

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
                                            Log.Monitor(string.Format("线程{0}({1})执行正常",ti.ThreadName, p));
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(string.Format("线程{0}({1})执行异常:{2} {3}",ti.ThreadName, p, ex.Message, ex.StackTrace));
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
                                            Log.Info(string.Format("线程{0}执行正常",ti.ThreadName));
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(string.Format("线程{0}执行异常:{1} {2}",ti.ThreadName, ex.Message, ex.StackTrace));
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
                    Log.Error(e.Message + e.StackTrace);
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
