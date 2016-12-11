using System.Collections.Generic;

namespace Lfb.DataGrabBll.Dto
{
    /// <summary>
    /// 百家号主页的取数据js返回的数据dto
    /// </summary>
    public class DtoBaijiahaoAuthorJsData
    {
        public int total;
        public int count;
        public List<DtoBaijiahaoAuthorNews> items;
    }
    
}
