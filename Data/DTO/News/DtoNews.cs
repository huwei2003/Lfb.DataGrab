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
        public DateTime PubTime;

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
        public int IsShow;

        /// <summary>
        /// 总评论数
        /// </summary>
        public int TotalComments;

        /// <summary>
        /// 新闻分类
        /// </summary>
        public int NewsTypeId;

        /// <summary>
        /// 刷新处理标识 0未处理 1已处理
        /// </summary>
        public int IsDeal;

        /// <summary>
        /// 刷新时间间隔
        /// </summary>
        public int IntervalMinutes;

        /// <summary>
        /// 最后刷新时间
        /// </summary>
        public DateTime LastDealTime;

        /// <summary>
        /// 是否爆文标识
        /// </summary>
        public int IsHot;

        /// <summary>
        /// 新闻热度级别
        /// 如果是当天发稿的文章，每15分钟刷新一次阅读量，如果5、6、7级，则改为小时更新；7天内发稿的文章，每一小时更新一次阅读数；7天以上，每几刷新；
        ///（这个可以按欢迎度级别优化，如15分钟阅读增加在10000以上为1级，5000以上为2级，2500以上为3级，1000以上为4级，500以上为5级，100以上为6级，100以下为7级）
        /// </summary>
        public int NewsHotClass;

        /// <summary>
        /// 上次阅读量
        /// </summary>
        public int LastReadTimes;
        /// <summary>
        /// 当前阅读量
        /// </summary>
        public int CurReadTimes;

        /// <summary>
        /// tags
        /// </summary>
        public string Tags;

        /// <summary>
        /// 作者Id
        /// </summary>
        public string AuthorId;

        /// <summary>
        /// 刷新次数
        /// </summary>
        public int RefreshTimes;

        /// <summary>
        /// groupid
        /// </summary>
        public string GroupId;

        /// <summary>
        /// FeedId
        /// </summary>
        public string FeedId;
    }

    /// <summary>
    /// 新闻类
    /// </summary>
    public class DtoNewsSimple
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id;
        /// <summary>
        /// FeedId
        /// </summary>
        public string FeedId;

        /// <summary>
        /// author
        /// </summary>
        public string AuthorId;
    }
}
