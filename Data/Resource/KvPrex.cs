namespace Comm.Global.Resource
{
    /// <summary>
    /// kv前缀命名，按服务独立
    /// 需要支持redis3，需要lua批量操作的key前缀以{}包围
    /// {}内值相同的keys在redis3中分配在同一个实例中
    /// </summary>
    public static class KvPrex
    {

        /// <summary>
        /// Mysql表Id管理
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <returns></returns>
        public static string AllocIdNames(int serviceName)
        {
            return string.Format("{{AllocId.{0}}}", serviceName);
        }

        /// <summary>
        /// 令牌
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <returns></returns>
        public static string TokenNames(int serviceName)
        {
            return string.Format("{{Token.{0}}}", serviceName);
        }

        

        /// <summary>
        /// 活跃度
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <param name="func">功能</param>
        /// <returns></returns>
        public static string TimeLimitNames(string func,int serviceName)
        {
            return string.Format("{{TimeLimit.{0}.{1}}}",func, serviceName);
        }

        /// <summary>
        /// 短信
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <returns></returns>
        public static string SmsNames(int serviceName)
        {
            return string.Format("{{Sms.{0}}}", serviceName);
        }

        /// <summary>
        /// 好友
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <returns></returns>
        public static string FriendNames(int serviceName)
        {
            return string.Format("{{Friend.{0}}}", serviceName);
        }

        /// <summary>
        /// 群聊室
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <returns></returns>
        public static string PublicRoomNames(int serviceName)
        {
            return string.Format("{{PublicRoom.{0}}}", serviceName);
        }

        /// <summary>
        /// 客服聊室
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <returns></returns>
        public static string ClerkRoomNames(int serviceName)
        {
            return string.Format("{{ClerkRoom.{0}}}", serviceName);
        }
    }
}
