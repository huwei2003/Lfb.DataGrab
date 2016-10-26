namespace Comm.Global.Exception
{
    /// <summary>
    /// 基本异常
    /// </summary>
    public abstract class BaseException : System.Exception
    {
        public string Detail { get; private set; }

        protected BaseException(string message, System.Exception innerException)
            : base(message, innerException)
        {
            if (innerException != null)
                Detail = innerException.ToString();
        }
    }
    
}