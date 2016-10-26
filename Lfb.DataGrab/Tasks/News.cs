using System;
using System.Threading;
using Lib.Csharp.Tools;
using Lib.Csharp.Tools.Base;

namespace Lfb.DataGrab.Tasks
{
    /// <summary>
    /// 佛相关新闻，活动等抓取程序
    /// </summary>
    public class News : MultiThread<News>
    {

        static News()
        {
            AddTask(NewsDeal, 60*60);

            AddTask(ImgDeal, 65 * 60);
        }

        /// <summary>
        /// 新闻抓取处理
        /// </summary>
        public static void NewsDeal()
        {
            try
            {
                if (Global.IsEnableGather<1)
                {
                    return;
                }
                //时段控制 0-8点不抓取
                if (DateTime.Now.Hour < 8)
                {
                    return;
                }
                Log.Info("新闻抓取开始:"+DateTime.Now.ToString());
                var siteList = XmlDeal.GetSitesInfo();
                
                if (siteList != null && siteList.Count > 0)
                {
                    foreach (var site in siteList)
                    {
                        if (site.SiteName.ToLower() == "toutiao")
                        {
                            var bll = new ToutiaoGather();
                            bll.NewsUrlGathering(site.Url, site.NewsType);
                        }
                        
                        Thread.Sleep(60*1000);
                    }
                }
                Log.Info("新闻抓取结束:" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 图片转存处理
        /// </summary>
        public static void ImgDeal()
        {
            try
            {
                if (Global.IsEnableImgDeal<1)
                {
                    return;
                }
                //时段控制 0-9点不处理
                if (DateTime.Now.Hour < 9)
                {
                    return;
                }

                Log.Info("图片处理开始:" + DateTime.Now.ToString());

                var bll = new ImgDeal();
                bll.ImgDealing();

                Log.Info("图片处理结束:" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

    }
}
