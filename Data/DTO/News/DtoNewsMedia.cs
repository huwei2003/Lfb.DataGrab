namespace Comm.Global.DTO.News
{
    /// <summary>
    /// 新闻类的多媒体信息
    /// </summary>
    public class DtoNewsMedia
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id;

        /// <summary>
        /// 所属新闻id
        /// </summary>
        public int NewsId;

        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicUrl;

        /// <summary>
        /// 图片原路径
        /// </summary>
        public string PicOriginalUrl;

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string ThumbnailUrl;

        /// <summary>
        /// 图片描述
        /// </summary>
        public string Description;

        /// <summary>
        /// 排序
        /// </summary>
        public int Orders;

        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow;

    }
}
