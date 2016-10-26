using Comm.Global.Enum.Business;

namespace Comm.Global.DTO.News
{
    /// <summary>
    /// 新闻地址列表
    /// </summary>
    public class DtoNewsUrlList
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { set;get;}

        /// <summary>
        /// 详细页地址
        /// </summary>
        public string Url { set; get; }

        /// <summary>
        /// logo图的地址
        /// </summary>
        public string LogoUrl { set; get; }

        /// <summary>
        /// 新闻分类
        /// </summary>
        public NewsTypeEnum NewsType { set; get; }
    }
}
