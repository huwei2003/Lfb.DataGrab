using System;

namespace Comm.Global.DTO.Media
{
    /// <summary>
    /// 基本的以独立表存在的实体
    /// </summary>
    public class MediaBase
    {/// <summary>
        /// 编号
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// url链接
        /// </summary>
        public string Url { get; set; }
    }
}
