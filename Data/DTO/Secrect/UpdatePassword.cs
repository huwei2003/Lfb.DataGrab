namespace Comm.Global.DTO.Secrect
{
    /// <summary>
    /// 用户修改密码请求
    /// </summary>
    public class UpdatePassword
    {
        /// <summary>
        /// 老密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }
    }
}
