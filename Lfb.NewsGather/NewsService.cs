using System;
using System.Threading;
using Lfb.DataGrabBll;
using Lib.Csharp.Tools;
using Lib.Csharp.Tools.Base;

namespace Lfb.NewsGather
{
    public class NewsService 
    {


        public void Start()
        {
            NewsDealing.Start();
        }

        
        public  void Stop()
        {
            try
            {
                Log.Info("服务退出");
            }
            catch
            {
            }
        }
    }
}
