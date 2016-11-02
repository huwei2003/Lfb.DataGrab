using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfb.DataGrabBll.Dto
{
    /// <summary>
    /// 首页或频道页js数据里的新闻数据dto
    /// </summary>
    public class DtoTouTiaoChannelNews
    {
        public string media_avatar_url;
        public string article_genre;
        public bool is_diversion_page;
        public string title;
        public bool middle_mode;
        public int gallary_image_count;
        public List<DtoImage> image_list;
        public bool more_mode;
        public long behot_time;
        public string source_url;
        public string source;
        public int hot;
        public bool is_feed_ad;
        public int comments_count;
        public bool has_gallery;
        public bool single_mode;
        public string image_url;
        public string group_id;
        public string media_url;
    }

    public class DtoImage
    {
        public string url;
    }
}
