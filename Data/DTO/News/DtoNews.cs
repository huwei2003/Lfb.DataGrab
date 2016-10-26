using System;
using Comm.Global.Enum.Business;

namespace Comm.Global.DTO.News
{
    /// <summary>
    /// 新闻类
    /// </summary>
    public class DtoNews
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 正文内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public string PubTime { get; set; }

        /// <summary>
        /// 新闻来源(url)
        /// </summary>
        public string FromUrl { get; set; }

        /// <summary>
        /// 来源站点名称
        /// </summary>
        public string FromSiteName { get; set; }
        
        /// <summary>
        /// logo图地址
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// logo图的原地址
        /// </summary>
        public string LogoOriginalUrl { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 总评论数
        /// </summary>
        public int TotalComments { get; set; }

        /// <summary>
        /// 总点击数
        /// </summary>
        public int TotalClick { get; set; }

        /// <summary>
        /// 总收藏数
        /// </summary>
        public int TotalCollection { get; set; }

        /// <summary>
        /// 新闻分类
        /// </summary>
        public NewsTypeEnum NewsTypeId { get; set; }

        /// <summary>
        /// 图片处理标识 0未处理 1已处理
        /// </summary>
        public int ImgFlag { get; set; }
    }
}
