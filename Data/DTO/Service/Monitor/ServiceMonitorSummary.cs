using System.Collections.Generic;

namespace Comm.Global.DTO.Service.Monitor
{
    /// <summary>
    /// 通过webapi处理的请求统计
    /// </summary>
    public class ServiceMonitorSummary
    {
        /// <summary>
        /// 总体健康程度
        /// </summary>
        public int HealthPercent { get; set; }

        /// <summary>
        /// 所有实际服务的集合，需要根据ServiceName分组显示集群
        /// </summary>
        public IList<ServiceMonitor> Healthes { get; set; }

    }
}