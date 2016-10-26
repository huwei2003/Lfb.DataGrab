using System;
using System.Collections.Generic;

namespace Comm.Global.DTO.News
{
    /// <summary>
    /// 新闻类带media部分
    /// </summary>
    public class DtoNewsAll
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title;

        /// <summary>
        /// 正文内容
        /// </summary>
        public string Contents;

        /// <summary>
        /// 发布日期
        /// </summary>
        public string PubTime;

        /// <summary>
        /// 新闻来源(url)
        /// </summary>
        public string FromUrl;

        /// <summary>
        /// 来源站点名称
        /// </summary>
        public string FromSiteName;
        
        /// <summary>
        /// logo图地址
        /// </summary>
        public string LogoUrl;

        /// <summary>
        /// logo图的原地址
        /// </summary>
        public string LogoOriginalUrl;

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime;

        /// <summary>
        /// 作者
        /// </summary>
        public string Author;

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow;

        /// <summary>
        /// 总评论数
        /// </summary>
        public int TotalComments;

        /// <summary>
        /// 总点击数
        /// </summary>
        public int TotalClick;

        /// <summary>
        /// 总收藏数
        /// </summary>
        public int TotalCollection;

        /// <summary>
        /// 新闻分类
        /// </summary>
        public int NewsTypeId;

        /// <summary>
        /// 图片处理标识 0未处理 1已处理
        /// </summary>
        public int ImgFlag;

        /// <summary>
        /// 媒体列表
        /// </summary>
        public List<DtoNewsMedia> NewsMedia;
    }
}
