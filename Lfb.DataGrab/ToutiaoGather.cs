using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Comm.Global.DTO.News;
using Comm.Global.Enum.Business;
using Lfb.DataGrab.Dto;
using Lib.Csharp.Tools;
using Newtonsoft.Json;

namespace Lfb.DataGrab
{
    /// <summary>
    /// 今日头条的抓取类
    /// </summary>
    public class ToutiaoGather
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
                var data = JsonConvert.DeserializeObject<DtoTouTiaoJsData>(strContent);
                if (data != null)
                {
                    //处理data中的数据，有作者的地址则存储
                    if (data.data != null && data.data.Count > 0)
                    {
                        foreach (var item in data.data)
                        {
                            //item.media_url;
                            //"media_url": "http://toutiao.com/m3470331046/
                            var isAuthorUrl = Global.IsToutiaoAuthorUrl(item.media_url);
                            if (isAuthorUrl)
                            {
                                var authorId = Global.GetToutiaoAuthorId(item.media_url);
                                var isExists = DalNews.IsExistsAuthor(authorId);
                                if (!isExists)
                                {
                                    var model = new DtoAuthor()
                                    {
                                        Author = "",
                                        AuthorId = authorId,
                                        IsDeal = 0,
                                        LastDealTime = DateTime.Now,
                                        Url = item.media_url,
                                    };
                                    var id = DalNews.Insert(model);
                                }
                            }
                            else
                                continue;
                        }
                    }
                }
                var isHaveMore = data.has_more;
                //有更多数据，则继续抓取数据
                if(isHaveMore)
                {
                    var max_behot_time = data.next.max_behot_time;
                    //替换url中的max_behot_time

                }
                


                //判断是否还可以翻页，有则再取数据
                

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// 处理url,替换其中的时间参数，用以读取下一页数据
        /// </summary>
        /// <param name="url">原url</param>
        /// <param name="newMaxBehotTime">时间</param>
        /// <returns></returns>
        public string ModifyUrlMax_behot_time(string url,string newMaxBehotTime)
        {
            var reStr = url;
            try
            {
                //http://www.toutiao.com/api/article/feed/?category=__all__&utm_source=toutiao&widen=0&max_behot_time=1477194899&max_behot_time_tmp=1477194899&as=A135F8A09C7897E&cp=580C08C9477EAE1

                var arrStr = url.Split('&');
                if (arrStr.Length > 0)
                {
                    foreach (var item in arrStr)
                    {
                        if (item.ToLower().Contains("max_behot_time") && !item.ToLower().Contains("max_behot_time_tmp"))
                        {
                            reStr = reStr.Replace(item, "max_behot_time=" + newMaxBehotTime);
                        }
                        if (item.ToLower().Contains("max_behot_time_tmp"))
                        {
                            reStr = reStr.Replace(item, "max_behot_time_tmp=" + newMaxBehotTime);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return reStr;
        }


    }
}
