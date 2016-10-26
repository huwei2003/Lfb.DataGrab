using System;

namespace Comm.Global.DTO.Interface
{
    /// <summary>
    /// 带OwnerId外键的类
    /// </summary>
    public interface IOwnerId
    {
        Int32 OwnerId { get; set; }
    }
}
