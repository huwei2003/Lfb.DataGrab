namespace Comm.Global.Exception
{
    /// <summary>
    /// 驱动类异常，指访问其他服务的接口管道等出现问题
    /// </summary>
    public class DriverFailException : BaseException, IExceptionType
    {
        public ExceptionType ExceptionType { get; private set; }
        public int ResponseCode { get; private set; }
       
        public DriverFailException(string message,  System.Exception innerException = null,int responseCode=0)
            : base(message, innerException)
        {
            ExceptionType = ExceptionType.DriverFail;
            ResponseCode = responseCode;
        }
    }
    
}