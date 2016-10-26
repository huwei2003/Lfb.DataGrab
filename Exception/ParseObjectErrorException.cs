namespace Comm.Global.Exception
{
    /// <summary>
    /// 对象异常，指驱动反序列化微服务的返回值时出现错误
    /// </summary>
    public class ParseObjectErrorException : System.Exception, IExceptionType
    {
        public ExceptionType ExceptionType { get; private set; }
        public ParseObjectErrorException(string message)
            : base(message)
        {
            ExceptionType = ExceptionType.ParseObjectError;
        }
    }
    
}