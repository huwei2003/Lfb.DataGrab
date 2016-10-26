namespace Comm.Global.Exception
{
    /// <summary>
    /// 工具类，包括云服务类，属于工具组件产生的异常
    /// </summary>
    public class UtilCrashException : BaseException, IExceptionType
    {
        public ExceptionType ExceptionType { get; private set; }
        public UtilCrashException(string message, System.Exception innerException = null)
            : base(message, innerException)
        {
            ExceptionType = ExceptionType.UtilCrash;
        }
    }
    
}