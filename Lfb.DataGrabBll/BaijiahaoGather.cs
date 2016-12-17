using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Comm.Global.DTO.News;
using Comm.Global.Enum.Business;
using Lfb.DataGrabBll.Dto;
using Lfb.DataGrabBll.Model;
using Lib.Csharp.Tools;
using Lib.Csharp.Tools.Security;
using Newtonsoft.Json;
using System.Linq;

namespace Lfb.DataGrabBll
{
    /// <summary>
    /// 百家号的抓取类
    /// </summary>
    public class BaijiahaoGather
    {

        public static Dictionary<string, int> DictUrl = new Dictionary<string, int>();

        #region === task 定时调用的方法 ===
        public int GatheringAuthorUrlSearch()
        {
            if (!string.IsNullOrWhiteSpace(Global.BjhSearchKeywords))
            {
                var keywords = Global.BjhSearchKeywords.Split(',');
                foreach (var keyword in keywords)
                {
                    GatheringAuthorUrlFromSearch(keyword, 100, 0);
                    Thread.Sleep(3 * 1000);
                }

            }
            else
            { 
                
            }
            return 0;
        }

        /// <summary>
        /// 抓取百家号主页的list,抓取文章阅读量等数据,
        /// </summary>
        /// <returns></returns>
        public int GatheringNewsFromAuthor()
        {
            try
            {
                //取出待处理百家号的数据，并置位isdeal=2 处理中
                var list = DalNews.GetNoDealAuthorList_Bjh();
                #region === 取出待刷新的百家号url数据 ===
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrWhiteSpace(item.AuthorId))
                        {
                            var url = "http://baijiahao.baidu.com/api/content/article/listall?sk=super&ak=super&app_id={0}&_skip={1}&_limit=12";
                            DealAuthorData(url, item.AuthorId, item.GroupId, 0);
                        }
                    }
                    //Thread.Sleep(5 * 1000);
                    Thread.Sleep(200);
                }
                else
                {
                    Log.Info("暂时没有要处理的百家号url");
                    Thread.Sleep(60 * 1000);
                }
                #endregion
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return 0;
        }



        #endregion


        #region === 辅助方法 ===

        /// <summary>
        /// 根据搜索关键字搜索百家号
        /// </summary>
        /// <param name="newsListUrl"></param>
        /// <param name="newsType"></param>
        /// <returns></returns>
        public int GatheringAuthorUrlFromSearch(string keywords, int newsType, int searchPageIndex)
        {
            if (string.IsNullOrWhiteSpace(keywords))
            {
                return 0;
            }
            //百家号地址计数器，如果当前搜索页百家号地址小于2则不再读取下一页数据
            var iBjhCount = 0;
            //有效的百家号计数器
            var iHaveValidBjh = 0;
            var strContent = "";
            //贡献文章 总阅读数 作者文章 按时间
            //keywords = keywords.Replace("贡献文章", "\"贡献文章\"");
            //keywords = keywords.Replace("总阅读数", "\"总阅读数\"");
            //keywords = keywords.Replace("作者文章", "\"作者文章\"");
            //keywords = keywords.Replace("按时间", "\"按时间\"");
            
            keywords = keywords.Replace("贡献文章 ", "");
            keywords = keywords.Replace("贡献文章", "");
            keywords = keywords.Replace("总阅读数 ", "");
            keywords = keywords.Replace("总阅读数", "");
            keywords = keywords.Replace("作者文章 ", "");
            keywords = keywords.Replace("作者文章", "");
            keywords = keywords.Replace("按时间", "");

            //用来记录搜索关键字
            var groupid = keywords;
            if (groupid.Length > 50)
            {
                groupid = groupid.Substring(0, 30);
            }
            keywords = keywords.Replace(" ","%20").Replace("\\","");
            //keywords = System.Web.HttpUtility.UrlEncode(keywords);

            //var site = "%20site%3Abaijiahao.baidu.com";
            var inurl = "inurl%3Abaijiahao.baidu.com%3Fu%3Dapp_id";
            var url = "https://www.baidu.com/s?wd=" + keywords + inurl;
            
            try
            {
                if (searchPageIndex > 0)
                {
                    url += "&pn=" + searchPageIndex * 10;
                }
                Log.Info(url + " 搜索 页码" + searchPageIndex);

                #region === 取内容 ===
                strContent = HttpHelper.GetContent(url, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(strContent))
                {
                    Thread.Sleep(2*1000);
                    //重新请求一次，因为用了代理后，经常会失败
                    strContent = HttpHelper.GetContentByAgent(url, Encoding.UTF8);
                    if (string.IsNullOrWhiteSpace(strContent))
                    {
                        //HttpHelper.IsUseProxy = false;
                        //重新请求一次，因为用了代理后，经常会失败
                        Thread.Sleep(1 * 1000);
                        strContent = HttpHelper.GetContentByAgent(url, Encoding.UTF8);
                        //HttpHelper.IsUseProxy = true;
                        if (string.IsNullOrWhiteSpace(strContent))
                        {
                            Log.Info(url + " 未抓取到任何内容 页码" + searchPageIndex);
                        }
                    }
                }
                #endregion

                //Log.Info("===========begin =============="+url + " " + searchPageIndex);

                //Log.Info(strContent);

                //Log.Info("===========end ==============" + url + " " + searchPageIndex);



                #region === deal baijiahao ===
                if (!string.IsNullOrWhiteSpace(strContent))
                {
                    var lista = XpathHelper.GetOuterHtmlListByXPath(strContent, "//div[@class='f13']/a[1]");
                    if (lista != null && lista.Count > 0)
                    {
                        iBjhCount = 0;
                        iHaveValidBjh = 0;
                        foreach (var a in lista)
                        {
                            var href = XpathHelper.GetAttrValueByXPath(a, "//a", "href");
                            var hrefName = XpathHelper.GetInnerHtmlByXPath(a, "//a", "");

                            #region === deal baijiahao ===
                            if (hrefName.ToLower().Contains("baijiahao"))
                            {
                                iBjhCount++;
                                //var str  = HttpHelper.GetContentByAgent(href, Encoding.UTF8);
                                if (iBjhCount % 2 == 0)
                                {
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    Thread.Sleep(1000);
                                }
                                

                                var str = HttpHelper.GetContent(href, Encoding.UTF8);
                                if (string.IsNullOrWhiteSpace(str))
                                {
                                    str = HttpHelper.GetContentByAgent(href, Encoding.UTF8);
                                }
                                //取百家号主页里的百家号名称，appid
                                var author = "";
                                var appId = "";
                                if (!string.IsNullOrWhiteSpace(str))
                                {
                                    author = XpathHelper.GetInnerHtmlByXPath(str, "//title", "").Replace("-百家号", "");
                                    appId = XpathHelper.GetAttrValueByXPath(str, "//input[@class='author-appid mth-config']", "data-appid");
                                }
                                else
                                {
                                    Log.Info("取百家号主页内容没取到 href=" + href);
                                }
                                if (string.IsNullOrWhiteSpace(appId))
                                {
                                    Log.Info("appid没取到 内容如下=== begin === href=" + href);
                                    //Log.Info(str);
                                    Log.Info("appid没取到 内容如下=== end === href" + href);
                                    continue;
                                }
                                #region === 判断是否已存在 ===
                                var isHave = DalNews.IsExistsAuthor_Bjh(appId);
                                if (!isHave)
                                {
                                    iHaveValidBjh++;
                                    var model = new DtoAuthor()
                                    {
                                        Author = author,
                                        AuthorId = appId,
                                        GroupId = groupid,
                                        IntervalMinutes = 60,
                                        IsDeal = 0,
                                        IsShow = 0,
                                        LastDealTime = DateTime.Now,
                                        RefreshTimes = 0,
                                        Url = "http://baijiahao.baidu.com/u?app_id=" + appId,

                                    };
                                    var id = DalNews.Insert_Author_Bjh(model);
                                    Log.Info("keyword" + keywords + "authodid=" + id);
                                }
                                else
                                {
                                    //iHaveValidBjh = 0;
                                    Log.Info("appid" + appId + "已存在");
                                }
                                #endregion
                            }
                            else
                            {
                                //iHaveValidBjh = 0;
                                Log.Info("非百家号地址");
                                Log.Info("href=" + href + " hrefname=" + hrefName);
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    Log.Error("url=" + url + " 无内容" + DateTime.Now);
                }
                #endregion

                //如果当前页有百家号>=3则翻页，否则结束
                if (iBjhCount >= 3)
                {
                    //当翻页到后面且没有新的百家号时退出，不再翻页
                    if (iHaveValidBjh < 1 && searchPageIndex > 50)
                    {
                        return 0;
                    }
                    searchPageIndex++;
                    GatheringAuthorUrlFromSearch(keywords, newsType, searchPageIndex);
                }
            }
            catch (Exception ex)
            {
                Log.Error("url=" + url + " " + DateTime.Now);
                Log.Error(ex.Message + ex.StackTrace);
            }
            return 0;
        }

        public int DealAuthorData(string url, string authorId, string groupId, int AuthorPageIndex)
        {
            //var url = "http://baijiahao.baidu.com/api/content/article/listall?sk=super&ak=super&app_id={0}&_skip={1}&_limit=12";
            var skip = AuthorPageIndex * 12;
            if (skip == 0)
            {
                url = string.Format(url, authorId, skip);
            }
            else
            {
                url = url.Replace((skip-12).ToString(), skip.ToString());
            }

            var strContent = "";

            try
            {
                Log.Info(url + " 百家号抓取开始 页码" + AuthorPageIndex);
                strContent = HttpHelper.GetContentByAgent(url, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(strContent))
                {
                    //重新请求一次，因为用了代理后，经常会失败
                    strContent = HttpHelper.GetContentByAgent(url, Encoding.UTF8);
                    if (string.IsNullOrWhiteSpace(strContent))
                    {
                        //HttpHelper.IsUseProxy = false;
                        //重新请求一次，因为用了代理后，经常会失败
                        strContent = HttpHelper.GetContentByAgent(url, Encoding.UTF8);
                        //HttpHelper.IsUseProxy = true;
                        if (string.IsNullOrWhiteSpace(strContent))
                        {
                            Log.Info(url + " 未抓取到任何内容 页码" + AuthorPageIndex);
                            return 0;
                        }
                    }
                }
                var isHaveMore = false;
                //strContent = FormatJsonData(strContent);
                var data = JsonConvert.DeserializeObject<DtoBaijiahaoAuthorJsData>(strContent);
                if (data != null)
                {
                    Log.Info(url + " 页码" + AuthorPageIndex);

                    #region === 处理data中的数据，存储新闻信息 ===

                    if (data.items != null && data.items.Count > 0)
                    {
                        if (data.total > (AuthorPageIndex+1)*12)
                        {
                            isHaveMore = true;
                        }
                        foreach (var subItem in data.items)
                        {
                            try
                            {
                                var pubTime = Comm.Tools.Utility.StringConverter.ToDateTime(subItem.publish_at);
                                //一个月前的新闻不抓取
                                if (pubTime.AddMonths(1) < DateTime.Now)
                                {
                                    continue;
                                }
                                var newsId = DalNews.IsExistsNews_Bjh(authorId, subItem.title);
                                if (newsId < 1)
                                {
                                    #region === 不存在的插入===
                                    var model = new DtoNews()
                                    {
                                        Author = "",
                                        AuthorId = authorId,
                                        Contents = "",
                                        CreateTime = DateTime.Now,
                                        //CurReadTimes = Global.ToInt(subItem.read_amount),
                                        CurReadTimes = subItem.read_amount,
                                        FromSiteName = "baijiahao",
                                        FromUrl = subItem.url,
                                        IntervalMinutes = 60,
                                        IsDeal = 0,
                                        IsHot = 0,
                                        IsShow = 1,
                                        LastDealTime = DateTime.Now,
                                        LastReadTimes = subItem.read_amount,
                                        LogoOriginalUrl = subItem.url,
                                        LogoUrl = "",
                                        NewsHotClass = 7,
                                        NewsTypeId = (int)NewsTypeEnum.新闻,
                                        PubTime = Comm.Tools.Utility.StringConverter.ToDateTime(subItem.publish_at),
                                        Tags = subItem.tag,
                                        Title = subItem.title,
                                        TotalComments = subItem.comment_amount,
                                        RefreshTimes = 0,
                                        GroupId = subItem.app_id
                                    };
                                    DalNews.Insert_News_Bjh(model);
                                    #endregion
                                }
                                else
                                {
                                    #region === 存在的则更新数据 ===
                                    var oldNews = DalNews.GetNews_Bjh(newsId);

                                    if (oldNews != null)
                                    {
                                        //b、变化数据，如果是当天发稿的文章，每15分钟刷新一次阅读量，如果5、6、7级，则改为小时更新；
                                        //7天内发稿的文章，每一小时更新一次阅读数；
                                        //7天以上，每天刷新；
                                        //（这个可以按欢迎度级别优化，如15分钟阅读增加在10000以上为1级，5000以上为2级，2500以上为3级，1000以上为4级，500以上为5级，100以上为6级，100以下为7级）
                                        var isHot = 0;
                                        var minutes = (DateTime.Now - oldNews.LastDealTime).TotalMinutes;
                                        var newsClassId = 7;
                                        var addReads = subItem.read_amount - oldNews.CurReadTimes;
                                        var intervalMinutes = 24 * 60;
                                        if (addReads > 0)
                                        {
                                            if (minutes > 60)
                                            {
                                                var perHourReads = addReads / (minutes / 60.0);
                                                if (perHourReads > 10000)
                                                {
                                                    isHot = 1;
                                                }
                                            }
                                            else
                                            {
                                                if (addReads > 10000)
                                                {
                                                    isHot = 1;
                                                }
                                            }
                                            #region === 15分钟阅读量分析　===
                                            var per15MinutesReads = addReads / (minutes / 15.0);
                                            if (per15MinutesReads > 10000)
                                            {
                                                newsClassId = 1;
                                                isHot = 1;
                                                intervalMinutes = 15;
                                            }
                                            else if (per15MinutesReads > 5000)
                                            {
                                                newsClassId = 2;
                                                isHot = 1;
                                                intervalMinutes = 15;
                                            }
                                            else if (per15MinutesReads > 2500)
                                            {
                                                newsClassId = 3;
                                                intervalMinutes = 15;
                                            }
                                            else if (per15MinutesReads > 1000)
                                            {
                                                newsClassId = 4;
                                                intervalMinutes = 15;
                                            }
                                            else if (per15MinutesReads > 500)
                                            {
                                                newsClassId = 5;
                                                intervalMinutes = 60;
                                            }
                                            else if (per15MinutesReads > 100)
                                            {
                                                newsClassId = 6;
                                                intervalMinutes = 60;
                                            }
                                            else
                                            {
                                                newsClassId = 7;
                                                intervalMinutes = 60;
                                            }
                                            #endregion
                                        }
                                        if (oldNews.PubTime.AddHours(24) < DateTime.Now)
                                        {
                                            //不是今天发布的
                                            intervalMinutes = 24 * 60;
                                        }

                                        //如果原来是爆文的不修改ishot
                                        if (oldNews.IsHot == 1)
                                        {
                                            isHot = 1;
                                        }
                                        if (oldNews.NewsHotClass < newsClassId)
                                        {
                                            newsClassId = oldNews.NewsHotClass;
                                        }
                                        var model = new DtoNews()
                                        {
                                            Id = newsId,
                                            LastReadTimes = oldNews.CurReadTimes,
                                            CurReadTimes = subItem.read_amount,
                                            IsHot = isHot,
                                            IsDeal = 1,
                                            TotalComments = subItem.comment_amount,
                                            IntervalMinutes = intervalMinutes,
                                            NewsHotClass = newsClassId,
                                            LastDealTime = DateTime.Now,
                                        };
                                        
                                        DalNews.UpdateNews_Bjh(model);

                                        //暂不更新作者表的刷新时间，没用上
                                        //DalNews.UpdateAuthorInterval(authorId, intervalMinutes);
                                    }
                                    #endregion
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    #endregion

                    //Random rnd = new Random();
                    //有更多数据，则继续抓取数据
                    if (isHaveMore)
                    {
                        //sleep
                        //Thread.Sleep(rnd.Next(1000, 2500));
                        Thread.Sleep(200);
                        AuthorPageIndex++;
                        
                        DealAuthorData(url, authorId, groupId, AuthorPageIndex);
                    }
                    else
                    {
                        Log.Info("本百家号抓取结束总页数" + AuthorPageIndex);
                        //置位状态
                        //DalNews.UpdateAuthorIsDeal(authorId, 1);
                        AuthorPageIndex = 0;
                        //Thread.Sleep(rnd.Next(2000, 5000));
                        Thread.Sleep(200);
                    }
                }
                else
                {
                    Log.Info(url + " 百家号未取到数据 页码" + AuthorPageIndex);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
                Log.Debug("======strContent begin 百家号抓取=========");
                Log.Debug(url);
                Log.Debug(strContent);
                Log.Debug("======strContent end =========");
            }
            return 1;
        }
        #endregion

    }
}
