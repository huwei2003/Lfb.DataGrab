using System;
using Comm.Global.DTO.Interface;

namespace Comm.Global.DTO.Media.Audio
{
    /// <summary>
    /// 音频数据表结构
    /// </summary>
    public class TbAudio : DtoAudio, IId, IOwnerId, IExpiredTime, IUrl
    {
        /// <summary>
        /// 令牌失效时间,默认值表示不会失效
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }
}
