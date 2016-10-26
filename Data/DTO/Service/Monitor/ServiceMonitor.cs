using Comm.Global.DTO.Service.Config;
using Comm.Global.DTO.Service.WebApi;

namespace Comm.Global.DTO.Service.Monitor
{
    /// <summary>
    /// 通过webapi处理的请求统计
    /// </summary>
    public class ServiceMonitor
    { 
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 健康程度
        /// </summary>
        public int HealthPercent { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        public BaseServiceConfig BaseServiceConfig { get; set; }

        /// <summary>
        /// webapi统计
        /// </summary>
        public ServiceHealthSummery ServiceHealthSummery { get; set; }
       

    }
}