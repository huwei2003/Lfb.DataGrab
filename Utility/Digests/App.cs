using System;
using System.Diagnostics;
using System.Threading;

namespace Comm.Tools.Utility
{
    public static class App
    {
        private readonly static Process Process = Process.GetCurrentProcess();

        /// <summary>
        /// 当前使用的物理内存
        /// </summary>
        public static void PrintWorkingSet()
        {
            Process.Refresh();
            Console.WriteLine(@"当前使用的物理内存 {0:N2} KB", Process.WorkingSet64 / 1024);
        }

        /// <summary>
        /// 自动回收内存
        /// </summary>
        public static void AutoGc()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(10000);
                    try
                    {
                        Process.Refresh();
                        Console.WriteLine(@"{0} - 清理前内存 {1:N2} KB", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), Process.WorkingSet64 / 1024);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                        Process.Refresh();
                        Console.WriteLine(@"{0} - 清理后内存 {1:N2} KB", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), Process.WorkingSet64 / 1024);
                    }
                    catch
                    {
                        //
                    }
                }
            }).StartAsync();
        }
    }
}
