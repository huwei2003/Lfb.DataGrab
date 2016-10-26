using Comm.Global.DTO.Service.WebApi;

namespace Comm.Global.Exception
{
    /// <summary>
    /// 访问微服务返回400结果，调用方主动抛出该异常
    /// </summary>
    public class MicroServiceException : System.Exception, IExceptionType
    {
        public ExceptionType ExceptionType { get; private set; }

         public GError400 GError400 { get; private set; }
        public MicroServiceException(GError400 gError400)
            : base(gError400.Error)
        {
            ExceptionType=ExceptionType.MicroService;
            GError400 = gError400;
        }
    }
    
}