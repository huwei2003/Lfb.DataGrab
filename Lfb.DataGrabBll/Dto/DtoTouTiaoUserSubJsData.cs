using System.Collections.Generic;

namespace Lfb.DataGrabBll.Dto
{
    /// <summary>
    /// 用户订阅js返回的数据实例
    /// </summary>
    public class DtoTouTiaoUserSubJsData
    {
        public bool has_more;
        public string message;
        public int total_cnt;
        public List<DtoTouTiaoUserSubData> data;
    }

    
}
