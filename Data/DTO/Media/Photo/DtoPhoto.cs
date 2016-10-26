using System;

namespace Comm.Global.DTO.Media.Photo
{
    /// <summary>
    /// 相片Dto
    /// </summary>
    public class DtoPhoto:PhotoBase
    {
        /// <summary>
        /// 外键编号
        /// </summary>
        public Int32 OwnerId { get; set; }

       
    }
}
