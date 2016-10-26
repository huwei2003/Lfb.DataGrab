using System.Collections.Generic;
using System.Threading.Tasks;
using Comm.Global.DTO.Service.Config;

namespace Comm.Global.Config
{
    /// <summary>
    /// 配置调用的接口
    /// </summary>
    public interface IServiceConfig
    {
        /// <summary>
        /// 将内存缓存中的服务清除
        /// </summary>
        /// <returns></returns>
        void ClearAllServicesInBuffer();


        /// <summary>
        /// 读入所有服务配置，并缓存到内部变量，后续使用
        /// 如果失败抛出ConfigException
        /// </summary>
        /// <returns></returns>
        Task<IDictionary<string, object>> GetAllServicesToBufferAsync();

        /// <summary>
        /// 从缓存中得到指定服务,失败抛出异常ConfigException
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName">服务</param>
        /// <returns></returns>
        T GetServiceFromBuffer<T>(string serviceName) where T : class;


        /// <summary>
        /// 获取环境变量,如果失败抛出ConfigException
        /// </summary>
        /// <returns></returns>
        Task<RunEnvironment> GetRunEnvironmentConfigAsync();
        
    
    }
}
