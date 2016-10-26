using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Comm.Global.DTO.Service.WebApi
{
    /// <summary>
    /// 带广告的列表
    /// </summary>
    /// <typeparam name="TDto">列表内容项类</typeparam>
    /// <typeparam name="TAdvert">广告类</typeparam>
    public class PageListWithAdvert<TDto,TAdvert> :PageList<TDto>
        where TDto:class,new ()
        where TAdvert:class,new()
    {
        /// <summary>
        /// 广告实体,可省略
        /// </summary>
       public TAdvert advert { get; set; }

        public PageListWithAdvert(IList<TDto> list, int totalCount = 0)
            : base(list, totalCount)
        {
        }

        public PageListWithAdvert(IList<TDto> list, int skip, int top)
            : base(list, skip, top)
        {
        }
    }
}