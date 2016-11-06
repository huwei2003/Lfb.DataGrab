using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfb.DataGrabBll.Dto
{
    public class Related_media
    {
        public string open_url;
        public long media_id;
        public string description;
        public string avatar_url;
        public string name;
        public bool user_verified;
        public List<DtoLatest_article> latest_article;
    }
}
