using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfb.DataGrabBll.Dto
{
    /// <summary>
    /// 作者主页的取数据js返回的数据dto
    /// </summary>
    public class DtoTouTiaoAuthorJsData
    {
        public long media_id;
        public int has_more;
        public DtoToutiaoNext next;
        public int page_type;
        public string message;
        public List<DtoTouTiaoAuthorNews> data;
    }
    
}
