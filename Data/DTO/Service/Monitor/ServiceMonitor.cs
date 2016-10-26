using Comm.Global.DTO.Service.Config;
using Comm.Global.DTO.Service.WebApi;

namespace Comm.Global.DTO.Service.Monitor
{
    /// <summary>
    /// ͨ��webapi���������ͳ��
    /// </summary>
    public class ServiceMonitor
    { 
        /// <summary>
        /// ��������
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// �����̶�
        /// </summary>
        public int HealthPercent { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public BaseServiceConfig BaseServiceConfig { get; set; }

        /// <summary>
        /// webapiͳ��
        /// </summary>
        public ServiceHealthSummery ServiceHealthSummery { get; set; }
       

    }
}