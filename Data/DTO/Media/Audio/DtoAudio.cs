using System;

namespace Comm.Global.DTO.Media.Audio
{
    /// <summary>
    /// 音频Dto
    /// </summary>
    public class DtoAudio : AudioBase
    {
        /// <summary>
        /// 外键编号
        /// </summary>
        public Int32 OwnerId { get; set; }
    }
}
