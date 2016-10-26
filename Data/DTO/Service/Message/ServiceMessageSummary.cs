namespace Comm.Global.DTO.Service.Message
{
    /// <summary>
    /// ͨ����Ϣ�ܵ�/webapi�������Ϣͳ��
    /// </summary>
    public class ServiceMessageSummary
    {
        /// <summary>
        /// ���͵�webapi������
        /// </summary>
        public long ToWebapiCount { get; set; }

        /// <summary>
        /// �Ƿ���Ϣ����webapi����������
        /// �������Ϣ����������޷������
        /// </summary>
        public long DiscardWebapiCount { get; set; }
        

        /// <summary>
        /// ��webapi�յ�������
        /// </summary>
        public long FromWebapiCount { get; set; }

        /// <summary>
        /// ����Ϣ�ܵ��յ�������
        /// </summary>
        public long FromChannelCount { get; set; }

        /// <summary>
        /// �ɹ����������
        /// </summary>
        public long SuccessCount { get; set; }

        /// <summary>
        /// ʧ�ܴ��������
        /// </summary>
        public long FailCount { get; set; }
    }
}