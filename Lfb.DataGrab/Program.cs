using System;
using System.ServiceProcess;
using Lib.Csharp.Tools;

namespace Lfb.DataGrab
{
    static class Program
    {
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                try
                {
                    var ex = e.ExceptionObject as Exception;
                    if (ex != null)
                    {
                        Log.Error(string.Format("应用程序未处理异常:退出={0} {1} {2}",e.IsTerminating, ex.Message, ex.StackTrace));
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message + ex.StackTrace);
                }
            };
            ServiceBase.Run(new ServiceBase[] { new Service() });
        }
    }
}
