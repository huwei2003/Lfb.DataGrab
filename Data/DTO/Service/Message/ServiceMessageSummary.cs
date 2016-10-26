namespace Comm.Global.DTO.Service.Message
{
    /// <summary>
    /// 通过消息管道/webapi处理的消息统计
    /// </summary>
    public class ServiceMessageSummary
    {
        /// <summary>
        /// 发送到webapi的数量
        /// </summary>
        public long ToWebapiCount { get; set; }

        /// <summary>
        /// 非法消息，在webapi丢弃的数量
        /// 例如空消息，队列溢出无法处理的
        /// </summary>
        public long DiscardWebapiCount { get; set; }
        

        /// <summary>
        /// 从webapi收到的数量
        /// </summary>
        public long FromWebapiCount { get; set; }

        /// <summary>
        /// 从消息管道收到的数量
        /// </summary>
        public long FromChannelCount { get; set; }

        /// <summary>
        /// 成功处理的数量
        /// </summary>
        public long SuccessCount { get; set; }

        /// <summary>
        /// 失败处理的数量
        /// </summary>
        public long FailCount { get; set; }
    }
}