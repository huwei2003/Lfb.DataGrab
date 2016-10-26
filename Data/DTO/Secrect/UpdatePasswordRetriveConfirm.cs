namespace Comm.Global.DTO.Secrect
{
    /// <summary>
    /// 用户找回密码确认请求
    /// </summary>
    public class UpdatePasswordRetriveConfirm : UpdatePasswordRetrive
    {
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 短信验证
        /// </summary>
        public string SmsCode { get; set; }
    }
}
