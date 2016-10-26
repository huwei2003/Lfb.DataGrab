// ReSharper disable InconsistentNaming
namespace Comm.Global.Enum.Sys
{
    /// <summary>
    /// 服务命名帮助类
    /// </summary>
    public static class ServiceNameHelper
    {
        public static bool IsCloudService(this ServiceName serviceName)
        {
            return (int) serviceName > 0 && (int) serviceName < 100;
        }

        public static bool IsManagerService(this ServiceName serviceName)
        {
            return (int) serviceName > 100 && (int) serviceName < 200;
        }

        public static bool IsGatewayService(this ServiceName serviceName)
        {
            return (int) serviceName > 200 && (int) serviceName < 300;
        }

        public static bool IsMicroService(this ServiceName serviceName)
        {
            return (int) serviceName > 300;
        }

    }
}
