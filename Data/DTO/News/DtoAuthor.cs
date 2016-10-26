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
        public int Id { get; set; }

        /// <summary>
        /// 作者id
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// 是否处理
        /// </summary>
        public int IsDeal { get; set; }
        
        /// <summary>
        /// 作者首页地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 最后处理时间
        /// </summary>
        public DateTime LastDealTime { get; set; }


        /// <summary>
        /// 作者名
        /// </summary>
        public string Author { get; set; }

        
    }
}
