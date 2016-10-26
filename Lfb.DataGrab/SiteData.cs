namespace Lfb.DataGrab
{
    /// <summary>
    /// 抓取的站点配置信息
    /// </summary>
    public class SiteData
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 要抓取的url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 所属新闻类型
        /// </summary>
        public int NewsType { get; set; }

        /// <summary>
        /// 所属新闻类型名
        /// </summary>
        public string NewsTypeName { get; set; }

        /// <summary>
        /// 抓取间隔秒数
        /// </summary>
        public int WaitSeconds { get; set; }

        /// <summary>
        /// 每次(每页)抓取的条数
        /// </summary>
        public int Nums { get; set; }
    }
}
