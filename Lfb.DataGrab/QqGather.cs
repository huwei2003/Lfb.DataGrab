using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Comm.Global.DTO.News;
using Comm.Global.Enum.Business;
using Lib.Csharp.Tools;
using Newtonsoft.Json;

namespace Lfb.DataGrab
{
    /// <summary>
    /// 腾讯佛教的抓取类
    /// </summary>
    public class QqGather : IBaseGather
    {
        

        public List<DtoNewsUrlList> NewsUrlGathering(string newsListUrl, int newsType)
        {
            try
            {
                Log.Info(newsListUrl + " 抓取开始");
                var strContent = HttpHelper.GetContentByMobileAgent(newsListUrl, Encoding.GetEncoding("gb2312"));
                if (string.IsNullOrWhiteSpace(strContent))
                {
                    Log.Info(newsListUrl + " 未抓取到任何内容");
                    return null;
                }

                //取得标题列表
                var strList = XpathHelper.GetInnerHtmlListByXPath(strContent, "//div[@class='leftList']/ul/li");

                if (strList != null && strList.Count > 0)
                {
                    foreach (var item in strList)
                    {
                        try
                        {
                            var url = XpathHelper.GetAttrValueByXPath(item, "//a", "href");
                            var title = XpathHelper.GetInnerHtmlByXPath(item, "//a", "");
                            title = StrHelper.FormatHtml(title).Trim();


                            var isHave = DalNews.IsExistsNews(title);
                            //如果已存在则跳过
                            if (isHave)
                            {
                                continue;
                            }

                            if (newsType == 100 || newsType == 200 || newsType == 300)
                            {
                                #region === 根据详细页地址取新闻内容 ===

                                var news = NewsGathering(url);
                                if (news != null)
                                {
                                    news.NewsTypeId = newsType;
                                    news.Title = title;

                                    news.PubTime = StrHelper.ToDateTime( StrHelper.FormatPubTime(news.PubTime.ToString()));
                                    //入库
                                    var newsId = DalNews.Insert(news);
                                    if (newsId < 1)
                                    {
                                        continue;
                                    }

                                    //从内容中提取img,存入newsmedia
                                    var mediaList = ImgDeal.GetImgList(news.Contents);
                                    if (mediaList != null && mediaList.Count > 0)
                                    {
                                        news.Contents = mediaList[0].Description;

                                        foreach (var picitem in mediaList)
                                        {
                                            picitem.NewsId = newsId;
                                            DalNews.InsertMedia(picitem);
                                        }
                                    }

                                    //休眠 控制抓取的频率
                                    Random rnd = new Random();
                                    var sleepSeconds = rnd.Next(30, 90);
                                    Thread.Sleep(sleepSeconds*1000);
                                }

                                #endregion
                            }
                            if (newsType == 400)
                            {
                                #region === 根据详细页地址取图片内容 ===

                                var mediaList = NewsPicGathering(url);

                                var news = new DtoNews()
                                {
                                    Title = title,
                                    FromUrl = url,
                                    NewsTypeId = newsType,

                                };
                                if (mediaList != null && mediaList.Count > 0)
                                {

                                    news.Contents = mediaList[0].Description;

                                    //入库
                                    var newsId = DalNews.Insert(news);

                                    foreach (var picitem in mediaList)
                                    {
                                        picitem.NewsId = newsId;
                                        DalNews.InsertMedia(picitem);
                                    }
                                }

                                //休眠 控制抓取的频率
                                Random rnd = new Random();
                                var sleepSeconds = rnd.Next(30, 90);
                                Thread.Sleep(sleepSeconds*1000);

                                #endregion
                            }
                            Log.Info(url + " 抓取完成");
                        }
                        catch (Exception ex)
                        {
                            //Log.Error("内容: " + item);
                            Log.Error(ex.Message + ex.StackTrace);
                        }
                    }
                }
                Log.Info(newsListUrl + " 抓取结束");
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return null;
        }

        public DtoNews NewsGathering(string newsUrl)
        {

            try
            {
                var title = "";
                var content = "";
                var pubTime = "";
                var from = "";
                var author = "";
                var picUrl = "";

                var strNewContent = HttpHelper.GetContent(newsUrl, Encoding.GetEncoding("gb2312"));

                content = XpathHelper.GetInnerHtmlByXPath(strNewContent, "//div[@id='Cnt-Main-Article-QQ']", "");

                //从content里去除最下面的广告部分
                //var contentlast = StrHelper.GetStrByXPath(content, "//span[last()]", "");
                //content = content.Replace(contentlast, "");
                //content = Regex.Replace(content, contentlast, "", RegexOptions.IgnoreCase);
                
                
                //从content里去除最上面的分享部分
                //var contentfirst = StrHelper.GetStrByXPath(strNewContent, "//div[@class='tit-bar clearfix']", "");
                //content = content.Replace(contentfirst, "");
                //content = content.Replace("<div class='tit-bar clearfix' bosszone='titleDown'></div>","");
                content = content.Trim();

                pubTime = XpathHelper.GetInnerHtmlByXPath(strNewContent, "//span[@class='article-time']", "");
                pubTime = StrHelper.FormatHtml(pubTime).Trim();
                from = "腾讯佛学";
                //from = StrHelper.GetStrByXPath(strNewContent, "//span[@bosszone='jgname']/a", "");
                //from = StrHelper.FormatHtml(from);

                var picUrlList = XpathHelper.GetAttrValueListByXPath(content, "//img", "src");
                if (picUrlList != null && picUrlList.Count > 0)
                {
                    picUrl = picUrlList[0];
                }

                author = XpathHelper.GetInnerHtmlByXPath(strNewContent, "//div[@id='C-Main-Article-QQ']/div[1]/div/div[1]/span[5]", "");
                author = StrHelper.FormatHtml(author).Trim();


                //*[@id="Cnt-Main-Article-QQ"]/p/div[@r='1']
                content = DealContent(content);

                var news = new DtoNews
                {
                    Contents = content,
                    Title = title,
                    PubTime = StrHelper.ToDateTime( pubTime),
                    FromUrl = newsUrl,
                    FromSiteName = from,
                    Author = author,
                    CreateTime = DateTime.Now,
                    IsShow = 1,
                    LogoOriginalUrl = picUrl,
                    LogoUrl = picUrl
                };
                return news;
            }
            catch (Exception ex)
            {
                Log.Error(newsUrl + " 错误:" + ex.Message + ex.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// 主要处理内容里广告性质的文字及样式
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        private string DealContent(string contents)
        {
            var str = contents;
            str = StrHelper.FormatScript(str);
            str = Global.RemoveSymbol(str);
            try
            {
                var i01 = str.LastIndexOf("【腾讯佛学】");


                if (i01 > 0)
                {
                    str = str.Substring(0, i01);
                }
                if (str.Contains("上微信搜"))
                {
                    i01 = str.LastIndexOf("<span");
                    if (i01 > 0)
                    {
                        str = str.Substring(0, i01);
                    }
                }
                if (str.Contains("上微信搜"))
                {
                    i01 = str.LastIndexOf("<span");
                    if (i01 > 0)
                    {
                        str = str.Substring(0, i01);
                    }
                }
                if (str.Contains("上微信搜"))
                {
                    i01 = str.LastIndexOf("<span");
                    if (i01 > 0)
                    {
                        str = str.Substring(0, i01);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            str = StrHelper.RemoveImgStyle(str);
            str = StrHelper.RemoveHref(str);
            return str;
        }

        public List<DtoNewsMedia> NewsPicGathering(string newsUrl)
        {

            try
            {
                var list = new List<DtoNewsMedia>();

                if (!newsUrl.Contains("p="))
                {
                    newsUrl = newsUrl + "#p=1";
                }
                list =  NewsPicGatheringOne(newsUrl);


                //if (model == null)
                //    return null;
                //list.Add(model);
                //if (model.Orders < model.Id)
                //{
                //    for (var i = model.Orders+1; i < model.Id; i++)
                //    {
                //        var url = newsUrl.Replace("p="+model.Orders,"p="+model.Orders+1);
                //        var newmodel = await NewsPicGatheringOne(url);
                //        if (newmodel != null)
                //        {
                //            list.Add(newmodel);
                //        }
                //    }
                //}

                return list;
            }
            catch (Exception ex)
            {
                Log.Error(newsUrl + " 错误:" + ex.Message + ex.StackTrace);
            }
            return null;
        }

        public List<DtoNewsMedia> NewsPicGatheringOne(string newsUrl)
        {

            try
            {
                var list = new List<DtoNewsMedia>();

                var content = "";
                var picUrl = "";
                var curPage = 0;
                var totalPage = 1;

                var strNewContent = HttpHelper.GetContentByMobileAgent(newsUrl, Encoding.UTF8);

                var strScriptList = XpathHelper.GetInnerHtmlListByXPath(strNewContent, "/html/body/script");
                var strSrcipt = "";
                if (strScriptList != null && strScriptList.Count > 0)
                {
                    foreach (var str in strScriptList)
                    {
                        if (str.Contains("G_listdata"))
                        {
                            strSrcipt = str;
                            break;
                        }
                    }
                }
                var istrart = strSrcipt.IndexOf('[');
                var iend = strSrcipt.IndexOf(']');
                var strJson = strSrcipt.Substring(istrart, iend - istrart+1);
                var imgList = JsonConvert.DeserializeObject<List<DtoIfengImg>>(strJson);

                //不通过网页html分析内容了，因为要的内容在script中
                //var strcurPage = StrHelper.GetStrByXPath(strNewContent, "//div[@id='picTxt']/div/span[1]", "");
                //curPage = Convert.ToInt32(StrHelper.FormatHtml(strcurPage));

                //var strtotalPage = StrHelper.GetStrByXPath(strNewContent, "//div[@id='picTxt']/div/span[3]", "");
                //totalPage = Convert.ToInt32(StrHelper.FormatHtml(strtotalPage));

                //content = StrHelper.GetStrByXPath(strNewContent, "//div[@id='picTxt']/ul/li/p", "");
                //content = StrHelper.FormatHtml(content);
                //picUrl = StrHelper.GetAttrValueByXPath(strNewContent, "//img[@id='photo']", "src");

                //*[@id="picTxt"]/ul/li/p

                if (imgList != null && imgList.Count > 0)
                {
                    curPage = 1;
                    foreach (var img in imgList)
                    {
                        //id 临时用来记录总图片数
                        var model = new DtoNewsMedia
                        {
                            Description = img.title.Trim(),
                            Orders = curPage,
                            PicUrl = img.img,
                            PicOriginalUrl = img.originalimg,
                            IsShow = 1,
                            Id = imgList.Count
                        };
                        curPage++;
                        list.Add(model);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Log.Error(newsUrl + " 错误:" + ex.Message + ex.StackTrace);
            }
            return null;
        }
    }
}
