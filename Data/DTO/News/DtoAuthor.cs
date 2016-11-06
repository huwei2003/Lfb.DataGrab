using System;
using Comm.Global.Enum.Business;

namespace Comm.Global.DTO.News
{
    /// <summary>
    /// 作者类
    /// </summary>
    public class DtoAuthor
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id;

        /// <summary>
        /// 作者id
        /// </summary>
        public string AuthorId;

        /// <summary>
        /// 是否处理
        /// </summary>
        public int IsDeal;
        
        /// <summary>
        /// 作者首页地址
        /// </summary>
        public string Url;

        /// <summary>
        /// 最后处理时间
        /// </summary>
        public DateTime LastDealTime;


        /// <summary>
        /// 作者名
        /// </summary>
        public string Author;

        /// <summary>
        /// 刷新时间间隔
        /// </summary>
        public int IntervalMinutes;

        /// <summary>
        /// 是否刷新，用于标识是否抓取过作者相关新闻
        /// </summary>
        public int IsShow;
    }
}
