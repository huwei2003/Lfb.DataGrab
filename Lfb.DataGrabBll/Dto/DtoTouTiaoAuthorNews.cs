using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfb.DataGrabBll.Dto
{
    public class DtoTouTiaoAuthorNews
    {
        public int play_effective_count;
        public int gallery_pic_count;
        //阅读量
        public int go_detail_count;
        public string title;
        public List<DtoTouTiaoImageList> image_list;
        //public string abstract;
        public int show_play_effective_count;
        public bool middle_mode;
        public bool has_video;
        public string source_url;
        public DateTime datetime;
        public string live_status;
        public bool more_mode;
        public int comments_count;
        public string video_duration_str;
        public bool has_gallery;
        public string pc_image_url;
        
    }
}
