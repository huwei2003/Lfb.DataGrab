namespace Comm.Global.DTO.Service.Config
{
    /// <summary>
    /// 微服务配置内容
    /// </summary>
    public class BaseServiceConfig
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public ushort Port { get; set; }
    }
}
