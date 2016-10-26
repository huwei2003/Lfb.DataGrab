using System;

namespace Comm.Global.DTO.Interface
{
    /// <summary>
    /// 失效时间
    /// </summary>
    public interface IExpiredTime
    {
        DateTime ExpiredTime { get; set; }
    }
}
