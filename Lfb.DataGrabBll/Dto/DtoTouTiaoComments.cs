using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfb.DataGrabBll.Dto
{
    /// <summary>
    /// toutiao 评论的data部分 实体
    /// </summary>
    public class DtoTouTiaoCommentsData
    {
        public bool has_more;

        public int total;

        public List<DtoTouTiaoComment> comments;
    }
}
