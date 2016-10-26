namespace Comm.Global.Exception
{
    /// <summary>
    /// 异常的类型
    /// </summary>
    public interface IExceptionType
    {
        ExceptionType ExceptionType { get; }
    }

    public enum ExceptionType
    {
        DialgInfo,
        NoResource,
        AuthFail,
        ParseObjectError,
        MicroService,
        MissConfig,
        DriverFail,
        UnExpectedBusiness,
        UtilCrash
    }
}