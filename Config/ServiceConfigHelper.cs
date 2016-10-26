using Comm.Global.Enum.Sys;
using Comm.Global.Resource;
using Comm.Global.Exception;

namespace Comm.Global.Config
{
    public static class ServiceConfigHelper
    {
        /// <summary>
        /// 读取指定服务配置
        /// </summary>
        /// <typeparam name="T">返回配置内容类</typeparam>
        /// <param name="serviceConfig"></param>
        /// <param name="serviceName">共有服务</param>
        /// <returns></returns>
        public static T GetServiceFromBuffer<T>(this IServiceConfig serviceConfig, ServiceName serviceName) where T : class
        {
            return serviceConfig.GetServiceFromBuffer<T>(serviceName.ToString());
        }

        /// <summary>
        /// 读取指定服务配置
        /// 先按照私有配置读取，如果失败，再按照共有配置读取
        /// </summary>
        /// <typeparam name="T">返回配置内容类</typeparam>
        /// <param name="serviceConfig"></param>
        /// <param name="serviceName">共有服务</param>
        /// <param name="hostServiceName">私有子服务</param>
        /// <returns></returns>
        public static T GetServiceFromBuffer<T>(this IServiceConfig serviceConfig,ServiceName serviceName, ServiceName hostServiceName) where T : class
        {
            try
            {
                return serviceConfig.GetServiceFromBuffer<T>(string.Format("{0}-{1}", serviceName, hostServiceName));//先获取独立配置
            }
            catch (MissConfigException)//如果没有取到
            {
                return serviceConfig.GetServiceFromBuffer<T>(serviceName.ToString());//再尝试全局配置
            }
        }

       /* public static T GetServiceFromBuffer<T>(this IConfig config, ServiceName serviceName,
            ServiceName hostServiceName, T defaultValue = null) where T : class
        {
            try
            {
                return config.GetServiceFromBuffer<T>(string.Format("{0}-{1}", serviceName, hostServiceName));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static T GetServiceFromBuffer<T>(this IConfig config,ServiceName serviceName, T defaultValue = null) where T : class
        {
            try
            {
                return config.GetServiceFromBuffer<T>(serviceName.ToString());
            }
            catch (Exception)
            {
                return defaultValue;
            }
            
        }*/

        /// <summary>
        /// 全局唯一配置器
        /// </summary>
        public static readonly IServiceConfig GlobalServiceConfig = ServiceConfigFactory.GetInstanse(
            new[]
            {
               ManagerConfig.ConfigItem.  RegistryAddress,
            });


    }
}
