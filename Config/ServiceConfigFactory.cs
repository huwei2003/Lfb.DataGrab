using System.Collections.Generic;
using Comm.Global.DTO.Factory;

namespace Comm.Global.Config
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class ServiceConfigFactory : BaseFactory<IServiceConfig>
    {
       
        /// <summary>
        /// 工厂生产接口实例，一般用于静态域初始化
        /// 每种下级微服务配备一个具体接口
        /// </summary>
        /// <returns></returns>
        public static IServiceConfig GetInstanse(IList<string> registryServerUrls)
        {
            if (GetMockInstanse != null)
                return GetMockInstanse;
            return new ServiceConfig(registryServerUrls);
        }

       
    }
}
