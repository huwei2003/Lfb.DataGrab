using System;

namespace Comm.Global.DTO.Media.Logo
{
    /// <summary>
    /// Logo Dto
    /// </summary>
    public class DtoLogo
    {
        /// <summary>
        /// 外键编号
        /// </summary>
        public Int32 OwnerId { get; set; }

        /// <summary>
        /// url链接
        /// </summary>
        public string Url { get; set; }
    }
}
