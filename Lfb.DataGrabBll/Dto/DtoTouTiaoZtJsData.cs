using System.Collections.Generic;

namespace Lfb.DataGrabBll.Dto
{
    /// <summary>
    /// 组图列表js返回的数据实例
    /// </summary>
    public class DtoTouTiaoZtJsData
    {
        public bool has_more;
        public string message;
        public List<DtoTouTiaoZtNews> data;
        public DtoToutiaoNext next;
    }

    
}
