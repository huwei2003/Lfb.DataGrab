using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfb.DataGrabBll.Dto
{
    /// <summary>
    /// toutiao评论内容部分
    /// </summary>
    public class DtoTouTiaoComment
    {

        public DtoTouTiaoCommentUser user;
        public long id;
        public long dongtai_id;
    }

    public class DtoTouTiaoCommentUser
    {
        public string avatar_url;
        public long user_id;
        public string name;
    }
}
