using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Comm.Global.DTO.Service.WebApi
{
    /// <summary>
    /// 按范围返回的列表
    /// </summary>
    /// <typeparam name="TDto">列表内容项类</typeparam>
    public class RangeList<TDto> where TDto : new()
    {
        /// <summary>
        /// 下一个结果的主键，null表示没有下一个
        /// </summary>
        public RangeNextCondition next { get; set; }

       
        /// <summary>
        /// 实际数据列表,注意必须小写
        /// </summary>
       public IList<TDto> value { get; set; }

       
        /// <summary>
        /// 默认构造
        /// </summary>
        public RangeList()
        {
            value = new TDto[0];
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list"></param>
        /// <param name="nextCondition">下一次查询条件</param>
        public RangeList(IList<TDto> list, RangeNextCondition nextCondition=null)
        {
            value=list ?? new TDto[0] ;
            next = nextCondition;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list"></param>
        /// <param name="nextKey">下一个key</param>
        /// <param name="endKey"></param>
        public RangeList(IList<TDto> list, string nextKey, string endKey)
        {
            value = list ?? new TDto[0];
            next = new RangeNextCondition
            {
                NextKey = nextKey,
                EndKey = endKey
            };
        }
    }
}