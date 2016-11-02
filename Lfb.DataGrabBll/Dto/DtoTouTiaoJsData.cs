﻿using System.Collections.Generic;

namespace Lfb.DataGrabBll.Dto
{
    /// <summary>
    /// 头条频道或首页列表js返回的数据实例
    /// </summary>
    public class DtoTouTiaoJsData
    {
        public bool has_more;
        public string message;
        public List<DtoTouTiaoChannelNews> data;
        public DtoToutiaoNext next;
    }

    
}
