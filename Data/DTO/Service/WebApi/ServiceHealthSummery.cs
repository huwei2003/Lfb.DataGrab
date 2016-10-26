using System;

namespace Comm.Global.DTO.Service.WebApi
{
    /// <summary>
    /// ͨ��webapi���������ͳ��
    /// </summary>
    public class ServiceHealthSummery
    {
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public DateTime StarTime { get; set; }

        
        /// <summary>
        /// �������д���
        /// </summary>
        public string ServiceRunningError { get; set; }

        /// <summary>
        /// �������д�������
        /// </summary>
        public string ServiceRunningErrorDetail { get; set; }

        /// <summary>
        /// ���͵�webapi����������
        /// </summary>
        public long TotalRequestCount { get; set; }
        
       
        /// <summary>
        /// ʧ�ܴ��������
        /// ָ����401����������
        /// </summary>
        public long AuthFailCount { get; set; }

        /// <summary>
        /// ʧ�ܴ��������
        /// ָ��������4xxϵ�е���������
        /// </summary>
        public long TotalFailCount { get; set; }



    }
}