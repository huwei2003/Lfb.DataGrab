using System;
using Comm.Global.DTO.Interface;

namespace Comm.Global.DTO.Media.Photo
{
    /// <summary>
    /// 私有相片数据表结构
    /// </summary>
    public class TbPrivatePhoto : TbPublicPhoto,IExpiredTime
    {
        /// <summary>
        /// 令牌失效时间,默认值表示不会失效
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }
}
