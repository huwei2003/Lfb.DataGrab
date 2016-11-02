using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Comm.Global.DTO.News;
using Lib.Csharp.Tools;
using Newtonsoft.Json;

namespace Lfb.DataGrabBll
{
    /// <summary>
    /// ifeng 凤凰佛教的抓取类
    /// </summary>
    public class IFengGather : IBaseGather
    {
        

        public List<DtoNewsUrlList> NewsUrlGathering(string newsListUrl, int newsType)
        {
            try
            {
                Log.Info(newsListUrl + " 抓取开始");
                var strContent = HttpHelper.GetContentByMobileAgent(newsListUrl, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(strContent))
                {
                    Log.Info(newsListUrl+" 未抓取到任何内容");
                    return null;
                }
                //取得标题列表
                var strList = XpathHelper.GetInnerHtmlListByXPath(strContent, "//div[@class='box_list clearfix']");

                if (strList != null && strList.Count > 0)
                {
                    foreach (var item in strList)
                    {
                        try
                        {
                            var url = XpathHelper.GetAttrValueByXPath(item, "//h2/a", "href");
                            var title = XpathHelper.GetAttrValueByXPath(item, "//h2/a", "title");
                            title = StrHelper.FormatHtml(title).Trim();
                            var logoUrl = XpathHelper.GetAttrValueByXPath(item, "//div/a/img", "src");

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
                                    news.LogoOriginalUrl = logoUrl;
                                    news.LogoUrl = logoUrl;

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
                                    LogoUrl = logoUrl,
                                    LogoOriginalUrl = logoUrl
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
                                Thread.Sleep(sleepSeconds * 1000);

                                #endregion
                            }
                            Log.Info(url+ " 抓取完成");
                        }
                        catch (Exception ex)
                        {
                            //Log.Error("内容: "+item);
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

                var strNewContent = HttpHelper.GetContentByMobileAgent(newsUrl, Encoding.UTF8);

                content = XpathHelper.GetInnerHtmlByXPath(strNewContent, "//div[@id='artical_real']", "").Trim();
                pubTime = XpathHelper.GetInnerHtmlByXPath(strNewContent, "//div[@id='artical_sth']/p/span[1]", "");
                pubTime = StrHelper.FormatHtml(pubTime).Trim();
                from = XpathHelper.GetInnerHtmlByXPath(strNewContent, "//div[@id='artical_sth']/p/span[3]/span", "");
                from = StrHelper.FormatHtml(from).Trim();
                author = XpathHelper.GetInnerHtmlByXPath(strNewContent, "//div[@id='artical_sth']/p/span[4]/span", "");
                author = StrHelper.FormatHtml(author).Trim();
                if (string.IsNullOrWhiteSpace(from))
                {
                    from = "凤凰佛教";
                }
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
                    IsShow = 0,
                };
                return news;
            }
            catch (Exception ex)
            {
                Log.Error(newsUrl + " 错误:"+ex.Message + ex.StackTrace);
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
                var p1 = "<span class=\"ifengLogo\"><a href=\"http://www.ifeng.com/\" target=\"_blank\"><img src=\"http://p2.ifengimg.com/a/2016/0810/204c433878d5cf9size1_w16_h16.png\"></a></span></p>";
                str = Regex.Replace(str, p1, "");
                str = str.Replace(p1,"");

                var p2 = "<img src=\"http://p2.ifengimg.com/a/2016/0810/204c433878d5cf9size1_w16_h16.png\">";
                str = Regex.Replace(str, p2, "");
                str = str.Replace(p2, "");


            }
            catch (Exception ex)
            {
            }
            str = StrHelper.RemoveHref(str);
            str = StrHelper.RemoveImgStyle(str);

            return str.Trim();
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
                if (istrart > 0 && iend > 0 && strSrcipt.Length >= iend)
                {
                    var strJson = strSrcipt.Substring(istrart, iend - istrart + 1);
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
                                PicUrl = img.big_img,
                                PicOriginalUrl = img.originalimg,
                                IsShow = 1,
                                Id = imgList.Count
                            };
                            curPage++;
                            list.Add(model);

                        }
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
