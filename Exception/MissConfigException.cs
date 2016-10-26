namespace Comm.Global.Exception
{
    /// <summary>
    /// 配置异常，指从配置服务器获取数据或得到指定服务的配置失败
    /// </summary>
    public class MissConfigException : BaseException, IExceptionType
    {
        public ExceptionType ExceptionType { get; private set; }
       
        public MissConfigException(string message)
            : base(message,null)
        {
            ExceptionType = ExceptionType.MissConfig;
        }
    }
    
}