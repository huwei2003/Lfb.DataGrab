namespace Comm.Global.Exception
{
    /// <summary>
    /// 资源不存在异常，对应404错误,需要提示给用户
    /// </summary>
    public class NoResourceException : System.Exception, IExceptionType
    {
        public string Detail { get; private set; }
        public ExceptionType ExceptionType { get; private set; }
        public NoResourceException(string message, string detail = null)
            : base(message)
        {
            ExceptionType = ExceptionType.NoResource;
            Detail = detail;
        }
    }
    
}