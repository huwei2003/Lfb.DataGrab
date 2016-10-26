using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Comm.Global.DTO.Service.WebApi
{
    /// <summary>
    /// 分页返回的列表
    /// </summary>
    /// <typeparam name="TDto">列表内容项类</typeparam>
    public class PageList<TDto> where TDto : class,new()
    {
        /// <summary>
        /// 数据库中总数
        /// </summary>
        public long total { get; set; }

        /// <summary>
        /// 实际数据列表,注意必须小写
        /// </summary>
       public IList<TDto> value { get; set; }

       
        /// <summary>
        /// 默认构造
        /// </summary>
        public PageList()
        {
            value = new TDto[0];
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list"></param>
        /// <param name="totalCount">0表示使用本次集合数量</param>
        public PageList(IList<TDto> list, long totalCount = 0)
        {
            value=list ?? new TDto[0] ;
            if (totalCount < value.Count)
                totalCount = value.Count;
            total = totalCount;
        }

        /// <summary>
        /// 根据查询结果(已经实施了skip,top)和预期的skip,top创建返回列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="skip"></param>
        /// <param name="top"></param>
        public PageList(IList<TDto> list, int skip, int top)
        {
            value = list ?? new TDto[0];
            if (value.Count < top) //列表内容少于预期
                total = skip + value.Count;//返回实际数目
            else //列表内容等于预期
                total = skip + value.Count + 1;//返回多一条,希望下次再取
        }

      
    }
}