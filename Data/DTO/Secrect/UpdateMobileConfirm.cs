namespace Comm.Global.DTO.Secrect
{
    /// <summary>
    /// 用户修改手机确认请求
    /// </summary>
    public class UpdateMobileConfirm : UpdateMobile
    {
        /// <summary>
        /// 短信验证
        /// </summary>
        public string SmsCode { get; set; }
    }
}
