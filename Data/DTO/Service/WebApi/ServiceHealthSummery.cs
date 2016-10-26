using System;

namespace Comm.Global.DTO.Service.WebApi
{
    /// <summary>
    /// 通过webapi处理的请求统计
    /// </summary>
    public class ServiceHealthSummery
    {
        /// <summary>
        /// 服务启动时间
        /// </summary>
        public DateTime StarTime { get; set; }

        
        /// <summary>
        /// 服务运行错误
        /// </summary>
        public string ServiceRunningError { get; set; }

        /// <summary>
        /// 服务运行错误详情
        /// </summary>
        public string ServiceRunningErrorDetail { get; set; }

        /// <summary>
        /// 发送到webapi的请求数量
        /// </summary>
        public long TotalRequestCount { get; set; }
        
       
        /// <summary>
        /// 失败处理的数量
        /// 指返回401的所有请求
        /// </summary>
        public long AuthFailCount { get; set; }

        /// <summary>
        /// 失败处理的数量
        /// 指返回所有4xx系列的所有请求
        /// </summary>
        public long TotalFailCount { get; set; }



    }
}