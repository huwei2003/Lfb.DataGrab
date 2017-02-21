using System;
using System.Threading;
using Lfb.DataGrabBll;
using Lib.Csharp.Tools;
using Lib.Csharp.Tools.Base;

namespace Lfb.NewsGather
{
    public class NewsDealing : MultiThread<NewsDealing>
    {

        //private bool _isStop = false;
        private static bool _isGetProxy = false;
        private static readonly object LockObj = new object();

        static NewsDealing()
        {
            //初始化
            #region === toutiao ===
            AddTask(RefreshProxyDeal, 1 * 60);

            AddTask(DealProxyListRemove, 10 * 60);

            AddTask(GatheringAuthorUrlFromChannel, 10 * 60);

            AddTask(GatheringNewsFromAuthor, 15 * 60);

            //开三个
            AddTask(GatheringAuthorNewsByRefresh, 15 * 60);
            //AddTask(GatheringAuthorNewsByRefresh, 16 * 60);
            //AddTask(GatheringAuthorNewsByRefresh, 17 * 60);

            //开二个
            AddTask(GatherAuthorFromNews, 15 * 60);
            //AddTask(GatherAuthorFromNews, 16 * 60);


            AddTask(GatherRelationFromAuthor, 15 * 60);

            AddTask(GatherNewsFromZtRecent, 15 * 60);

            AddTask(GatheringUserFromRelationUser, 1 * 60);

            AddTask(GatheringUserInfoFromUserUrl, 3 * 60);
            #endregion

            #region  === 百度百家号 ===
            
            AddTask(GatheringAuthorUrlSearch, 1 * 60);
            //AddTask(GatheringAuthorUrlSearch2, 1 * 60);
            AddTask(GatheringNewsFromAuthor_Bjh, 1 * 60);
            AddTask(GatheringNewsFromAuthor_Bjh, 3 * 60);
            AddTask(GatheringNewsFromAuthor_Bjh, 5 * 60);
            
            #endregion
        }

        /// <summary>
        /// 定时刷新代理列表
        /// </summary>
        public static void RefreshProxyDeal()
        {
            try
            {
                if (Global.IsEnableRefreshProxy != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}

                var i = 0;
                while (true)
                {
                    lock (LockObj)
                    {
                        if (!_isGetProxy)
                        {
                            _isGetProxy = true;
                            i++;
                            Log.Info("定时刷新代理列表开始 i=" + i + " time=" + DateTime.Now);

                            ProxyDeal.GetProxyList();

                            Log.Info("定时刷新代理列表结束 i=" + i + " time=" + DateTime.Now);
                        }
                    }
                    Thread.Sleep(5 * 60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 定时刷新代理列表
        /// </summary>
        public static void DealProxyListRemove()
        {
            try
            {
                if (Global.IsEnableRefreshProxy != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                var i = 0;
                while (true)
                {
                    lock (LockObj)
                    {
                        i++;
                        Log.Info("定时处理被移聊代理列表开始 i=" + i + " time=" + DateTime.Now);

                        ProxyDeal.DealProxyListRemove();

                        Log.Info("定时处理被移聊代理列表结束 i="+i+" time=" + DateTime.Now);

                    }
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 头条频道新闻抓取处理
        /// </summary>
        public static void GatheringAuthorUrlFromChannel()
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
                int i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("频道新闻抓取开始 i=" + i + " time=" + DateTime.Now);
                    var siteList = XmlDeal.GetSitesInfo();

                    if (siteList != null && siteList.Count > 0)
                    {

                        //foreach (var site in siteList)
                        //{
                        //    if (site.SiteName.ToLower() == "toutiao")
                        //    {
                        //        var bll = new ToutiaoGather();
                        //        bll.AuthorUrlGathering(site.Url, site.NewsType);
                        //    }
                        //    Thread.Sleep(60 * 1000);
                        //}

                        #region === 改成随机，不固定顺序，避免多开时从同一个顺序启动抓取 ===

                        Random rnd = new Random();
                        var iStart = rnd.Next(0, siteList.Count);
                        //增加从下面索引开始的机率
                        if (iStart % 3 == 0)
                        {
                            iStart = 0;
                        }
                        if (iStart % 4 == 0)
                        {
                            iStart = 1;
                        }
                        if (iStart % 5 == 0)
                        {
                            iStart = 5;
                        }
                        if (iStart % 6 == 0)
                        {
                            iStart = 9;
                        }
                        for (var start = iStart; start < siteList.Count; start++)
                        {
                            if (start > siteList.Count || start < 0)
                            {
                                start = 0;
                            }
                            if (siteList[start].SiteName.ToLower() == "toutiao")
                            {
                                var bll = new ToutiaoGather();
                                bll.GatheringAuthorUrlFromChannel(siteList[start].Url, siteList[start].NewsType,0);
                            }
                            Thread.Sleep(5 * 1000);
                        }
                        #endregion

                    }
                    else
                    {
                        Log.Error("抓取错误-检查site.xml" + DateTime.Now);
                    }
                    Log.Info("频道新闻抓取结束 i=" + i + " time=" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
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
        public static void GatheringNewsFromAuthor()
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
                var i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("作者列表页新闻抓取开始 i=" + i + " time=" + DateTime.Now);

                    var bll = new ToutiaoGather();
                    bll.GatheringNewsFromAuthor();


                    Log.Info("作者列表页新闻抓取结束 i=" + i + " time=" + DateTime.Now);
                    Thread.Sleep(5 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 根据文章的刷新间隔取得该作者的主页来 抓取该作者文章阅读量等数据
        /// 要多开
        /// </summary>
        public static void GatheringAuthorNewsByRefresh()
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
                var i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("新闻刷新开始 i="+i+" time=" + DateTime.Now);

                    var bll = new ToutiaoGather();
                    bll.GatheringAuthorNewsByRefresh();


                    Log.Info("新闻刷新结束 i="+i+ " time=" + DateTime.Now);
                    Thread.Sleep(5 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
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
        /// 同时会抓取这个新闻的评论列表，从评论列表抓取用户，再从用户这里抓取用户订阅的作者
        /// 要多开
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
                int i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("从新闻页抓取作者开始 i=" + i + " time=" + DateTime.Now);

                    var bll = new ToutiaoGather();
                    bll.GatherAuthorFromNews();


                    Log.Info("从新闻页抓取作者结束 i=" + i + " time=" + DateTime.Now);
                    Thread.Sleep(5 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }


        /// <summary>
        /// 从作者抓取相关新闻作者信息
        /// 频率可以小点，因为作者相关新闻更新也慢
        /// </summary>
        public static void GatherRelationFromAuthor()
        {
            try
            {
                if (Global.IsEnableGatherRelationFromAuthor != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                var i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("从作者抓相关新闻的作者开始 i="+i +" time="+ DateTime.Now);

                    var bll = new ToutiaoGather();
                    bll.GatherRelationNewsFromAuthor();


                    Log.Info("从作者抓相关新闻的作者结束 i="+i + " time=" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }


        /// <summary>
        /// 从组图列表抓取作者信息
        /// </summary>
        public static void GatherNewsFromZtRecent()
        {
            try
            {
                if (Global.IsEnableGatherZt != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                var i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("从组图列表抓相关新闻的作者开始 i="+i+" time=" + DateTime.Now);

                    var bll = new ToutiaoGather();
                    var url = "http://www.toutiao.com/api/article/recent/?source=2&count=20&category=%E7%BB%84%E5%9B%BE&max_behot_time=0&utm_source=toutiao&device_platform=web&offset=0&as=A1B508A27D30C8F&cp=582D607C78CFCE1&_=1479347343375";
                    bll.GatherNewsFromZtRecent(url,0);


                    Log.Info("从组图列表抓相关新闻的作者结束 i="+i+" time=" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 用户列表抓相关推荐作者
        /// </summary>
        public static void GatheringUserFromRelationUser()
        {
            try
            {
                if (Global.IsEnableGatherUserInfo != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                var i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("从用户列表抓相关推荐作者开始 i=" + i + " time=" + DateTime.Now);

                    var bll = new ToutiaoGather();
                    
                    bll.GatheringUserFromRelationUser();


                    Log.Info("从用户列表抓相关推荐作者结束 i=" + i + " time=" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 用户列表抓作者信息
        /// </summary>
        public static void GatheringUserInfoFromUserUrl()
        {
            try
            {
                if (Global.IsEnableGatherUserInfo != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                var i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("从用户列表抓作者信息开始 i=" + i + " time=" + DateTime.Now);

                    var bll = new ToutiaoGather();

                    bll.GatheringUserInfoFromUserUrl();


                    Log.Info("从用户列表抓作者信息结束 i=" + i + " time=" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }


        /// <summary>
        /// 从用户订阅抓取作者信息 暂不用，放在新闻列表处理里一起做
        /// 暂不用在这里启动，在新闻处理里一起做了
        /// </summary>
        public static void GatherAuthorFromUserSub()
        {
            try
            {
                if (Global.IsEnableGatherUserSub != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                while (true && ProxyDeal.IsProxyReady)
                {
                    Log.Info("从用户订阅抓相关新闻的作者开始:" + DateTime.Now);

                    var bll = new ToutiaoGather();

                    //bll.GatherAuthorFromUserSub();


                    Log.Info("从用户订阅抓相关新闻的作者结束:" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }


        public static void GatheringAuthorUrlSearch() 
        {
            try
            {
                if (Global.IsEnableBjhSearch != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                var i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("从百度搜索百家号作者开始 i=" + i + " time=" + DateTime.Now);

                    var bll = new BaijiahaoGather();
                    
                    bll.GatheringAuthorUrlSearch();

                    Log.Info("从百度搜索百家号作者结束 i=" + i + " time=" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        public static void GatheringAuthorUrlSearch2()
        {
            try
            {
                if (Global.IsEnableBjhSearch != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                var i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("从百度搜索百家号作者2开始 i=" + i + " time=" + DateTime.Now);

                    var bll = new BaijiahaoGather();

                    bll.GatheringAuthorUrlSearch2();

                    Log.Info("从百度搜索百家号作者2结束 i=" + i + " time=" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
        }

        public static void GatheringNewsFromAuthor_Bjh() 
        {
            try
            {
                if (Global.IsEnableBjhAuthorGather != "1")
                {
                    return;
                }
                ////时段控制 0-8点不抓取
                //if (DateTime.Now.Hour < 8)
                //{
                //    return;
                //}
                var i = 0;
                while (true && ProxyDeal.IsProxyReady)
                {
                    i++;
                    Log.Info("从百家号作者主页抓取开始 i=" + i + " time=" + DateTime.Now);

                    var bll = new BaijiahaoGather();

                    bll.GatheringNewsFromAuthor();

                    Log.Info("从百家号作者主页抓取结束 i=" + i + " time=" + DateTime.Now);
                    Thread.Sleep(60 * 1000);
                }
                if (!ProxyDeal.IsProxyReady)
                {
                    Log.Info("代理未准备好" + DateTime.Now);
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
