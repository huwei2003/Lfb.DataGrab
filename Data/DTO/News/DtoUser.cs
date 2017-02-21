using System;
using Comm.Global.Enum.Business;

namespace Comm.Global.DTO.News
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class DtoUser
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name;
        /// <summary>
        /// userid
        /// </summary>
        public long UserId;
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime;
        /// <summary>
        /// 用户表自增id
        /// </summary>
        public int Id;
        /// <summary>
        /// 是否处理标识
        /// </summary>
        public int IsDeal;
        /// <summary>
        /// 最后处理时间
        /// </summary>
        public DateTime LastDealTime;
        /// <summary>
        /// 作者url
        /// </summary>
        public string OpenUrl;
        /// <summary>
        /// 作者AvatarUrl
        /// </summary>
        public string AvatarUrl;
        /// <summary>
        /// 是否显示标识
        /// </summary>
        public int IsShow;
        /// <summary>
        /// mediaid
        /// </summary>
        public string MediaId;
        /// <summary>
        /// 简介
        /// </summary>
        public string Descriptions;
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FansCount;
        /// <summary>
        /// 关注数
        /// </summary>
        public int FollowCount;
    }
}
