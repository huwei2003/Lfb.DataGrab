namespace Comm.Global.DTO.Secrect
{
    /// <summary>
    /// 用户修改手机请求
    /// </summary>
    public class UpdateMobile
    {
        /// <summary>
        /// 老手机
        /// </summary>
        public string OldMobile { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 新手机
        /// </summary>
        public string NewMobile { get; set; }
    }
}
