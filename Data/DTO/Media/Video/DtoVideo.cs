using System;

namespace Comm.Global.DTO.Media.Video
{
    /// <summary>
    /// 视频Dto
    /// </summary>
    public class DtoVideo:VideoBase
    {
        /// <summary>
        /// 外键编号
        /// </summary>
        public Int32 OwnerId { get; set; }
    }
}
