namespace Comm.Global.Exception
{
    /// <summary>
    /// 认证失败产生的异常
    /// </summary>
    public class AuthFailException : System.Exception, IExceptionType
    {
        public ExceptionType ExceptionType { get; private set; }
        public AuthFailException(string message)
            : base(message)
        {
            ExceptionType = ExceptionType.AuthFail;
        }
    }
    
}