namespace Comm.Global.Exception
{
    /// <summary>
    /// 业务层异常，实现本服务的过程中出现的，需要处理的‘程序员错误’问题
    /// 例如客户端发出了超过skip限制的访问请求
    /// </summary>
    public class UnExpectedBusinessException : BaseException, IExceptionType
    {
        public ExceptionType ExceptionType { get; private set; }
        public UnExpectedBusinessException(string message, System.Exception innerException = null)
            : base(message, innerException)
        {
            ExceptionType = ExceptionType.UnExpectedBusiness;
        }

       
    }
    
}