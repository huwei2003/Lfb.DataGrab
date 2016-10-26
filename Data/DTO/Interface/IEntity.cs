using System;
using System.Collections.Generic;

namespace Comm.Global.DTO.Interface
{
    /// <summary>
    /// 对应mysql数据库表结构的实体
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 返回主键域名
        /// </summary>
        /// <returns></returns>
        string GetPrimaryKeyFieldName();

        /// <summary>
        /// 定义字符串(varchar)/金额(x.y的x)等特殊字段的长度
        /// 未定义的特殊字段默认为8
        /// 无定义返回null
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> GetFieldLengths();
    }
}
