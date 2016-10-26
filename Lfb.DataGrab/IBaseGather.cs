using System.Collections.Generic;
using Comm.Global.DTO.News;

namespace Lfb.DataGrab
{
    /// <summary>
    /// 抓取的基本接口
    /// </summary>
    public interface IBaseGather
    {
        /// <summary>
        /// 抓取新闻链接的接口
        /// </summary>
        /// <param name="newsListUrl">新闻分类或频道的url</param>
        /// <param name="newsType">新闻类型</param>
        /// <returns></returns>
        List<DtoNewsUrlList> NewsUrlGathering(string newsListUrl, int newsType);

        /// <summary>
        /// 新闻内容的抓取接口
        /// </summary>
        /// <param name="newsUrl">新闻详细页的url</param>
        /// <returns></returns>
        DtoNews NewsGathering(string newsUrl);

        /// <summary>
        /// 图片内容的抓取接口
        /// </summary>
        /// <param name="newsUrl">图片的网页地址</param>
        /// <returns></returns>
        List<DtoNewsMedia> NewsPicGathering(string newsUrl);
    }
}
