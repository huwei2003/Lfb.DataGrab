using System.Collections.Generic;

namespace Comm.Global.DTO.Service.Monitor
{
    /// <summary>
    /// ͨ��webapi���������ͳ��
    /// </summary>
    public class ServiceMonitorSummary
    {
        /// <summary>
        /// ���彡���̶�
        /// </summary>
        public int HealthPercent { get; set; }

        /// <summary>
        /// ����ʵ�ʷ���ļ��ϣ���Ҫ����ServiceName������ʾ��Ⱥ
        /// </summary>
        public IList<ServiceMonitor> Healthes { get; set; }

    }
}