namespace Comm.Global.DTO.Service.Config
{
    /// <summary>
    /// 虚拟微服务配置内容，指负载均衡前的虚拟地址
    /// </summary>
    public class AnyServiceConfig<T> where T : class
    {
        public string ServiceName { get; set; }
        public T ServiceConfig { get; set; }
    }
}
