using System;
using System.Threading;
using Lib.Csharp.Tools;
using Lib.Csharp.Tools.Base;

namespace Lfb.DataGrab.Tasks
{
    /// <summary>
    /// 相关新闻抓取程序
    /// </summary>
    public class News : MultiThread<News>
    {

        static News()
        {
            AddTask(NewsDeal, 10 * 60);

            AddTask(AuthorNewsDeal, 15 * 60);

            AddTask(AuthorNewsByRefreshGathering, 15 * 60);

            AddTask(GatherAuthorFromNews, 15 * 60);
        }

        /// <summary>
        /// 头条频道新闻抓取处理
        /// </summary>
        public static void NewsDeal()
        {
            try
            {
                if (Global.IsEnableGatherChannel != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                
                while (true)
                {
                    Log.Info("频道新闻抓取开始:" + DateTime.Now);
                    var siteList = XmlDeal.GetSitesInfo();

                    if (siteList != null && siteList.Count > 0)
                    {
                        foreach (var site in siteList)
                        {
                            if (site.SiteName.ToLower() == "toutiao")
                            {
                                var bll = new ToutiaoGather();
                                bll.AuthorUrlGathering(site.Url, site.NewsType);
                            }

                            Thread.Sleep(60*1000);
                        }
                    }
                    else
                    {
                        Log.Error("抓取错误-检查site.xml" + DateTime.Now);
                    }
                    Log.Info("频道新闻抓取结束:" + DateTime.Now);
                    Thread.Sleep(60*1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 头条作者新闻处理
        /// </summary>
        public static void AuthorNewsDeal()
        {
            try
            {
                if (Global.IsEnableGatherAuthor != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                while (true)
                {
                    Log.Info("作者列表页新闻抓取开始:" + DateTime.Now);

                    var bll = new ToutiaoGather();
                    bll.AuthorNewsGathering();


                    Log.Info("作者列表页新闻抓取结束:" + DateTime.Now);
                    Thread.Sleep(60*1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 根据文章的刷新间隔取得该作者的主页来 抓取该作者文章阅读量等数据
        /// </summary>
        public static void AuthorNewsByRefreshGathering()
        {
            try
            {
                if (Global.IsEnableRefreshNews != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                while (true)
                {
                    Log.Info("作者列表页刷新开始:" + DateTime.Now);

                    var bll = new ToutiaoGather();
                    bll.AuthorNewsByRefreshGathering();


                    Log.Info("作者列表页刷新结束:" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 从新闻页抓取作者信息
        /// </summary>
        public static void GatherAuthorFromNews()
        {
            try
            {
                if (Global.IsEnableGatherAuthorFromNews != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                while (true)
                {
                    Log.Info("从新闻页抓取作者开始:" + DateTime.Now);

                    var bll = new ToutiaoGather();
                    bll.GatherAuthorFromNews();


                    Log.Info("从新闻页抓取作者结束:" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
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
                if (Global.IsEnableImgDeal < 1)
                {
                    return;
                }
                //时段控制 0-9点不处理
                if (DateTime.Now.Hour < 9)
                {
                    return;
                }

                Log.Info("图片处理开始:" + DateTime.Now);

                var bll = new ImgDeal();
                bll.ImgDealing();

                Log.Info("图片处理结束:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

    }
}
