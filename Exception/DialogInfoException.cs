namespace Comm.Global.Exception
{
    /// <summary>
    /// 对话框信息异常，指需要提示给用户的业务指示
    /// </summary>
    public class DialogInfoException : System.Exception, IExceptionType
    {
        public string Detail { get; private set; }
        public ExceptionType ExceptionType { get; private set; }
        public DialogInfoException(string message,string detail=null)
            : base(message)
        {
            ExceptionType=ExceptionType.DialgInfo;
            Detail = detail;
        }
    }
    
}