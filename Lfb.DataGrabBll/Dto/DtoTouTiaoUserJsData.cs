using System.Collections.Generic;

namespace Lfb.DataGrabBll.Dto
{
    /// <summary>
    /// 用户相关推荐取数据js返回的数据dto
    /// </summary>
    public class DtoTouTiaoUserJsData
    {
        public string message;
        public List<DtoTouTiaoUserData> data;
    }

    public class DtoTouTiaoUserData
    {
        public string open_url;
        public long media_id;
        public string description;
        public string avatar_url;
        public long user_id;
        public string name;
    }
}
