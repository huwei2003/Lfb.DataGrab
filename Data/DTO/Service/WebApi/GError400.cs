namespace Comm.Global.DTO.Service.WebApi
{
    /// <summary>
    /// 系统返回400错误时的实体类
    /// </summary>
    public class GError400
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public string Error { get; set; } 
        /// <summary>
        /// 详细内容
        /// </summary>
        public string Detail { get;  set; } 
        /// <summary>
        /// 主机地址和端口
        /// </summary>
        public string ServerUrl { get; set; } 
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务版本
        /// </summary>
        public string ServiceVersion { get; set; }
        /// <summary>
        /// 错误级别
        /// 0:UI信息 1:期望的自定义异常 2:期望外的异常
        /// </summary>
        public GError400Level Level { get; set; } 
       
    }

    public enum GError400Level
    {
        友好提示=0,
        对象异常=1,
        业务异常=2,
        服务崩溃=3,
        其他异常=9
    }
   
   
}
