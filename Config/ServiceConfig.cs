using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Comm.Global.DTO.Service.Config;
using Comm.Global.Exception;

namespace Comm.Global.Config
{
    public class ServiceConfig : IServiceConfig
    {
        //private  string DefaultUrl = "http://192.168.1.19:8085/";
        private readonly IList<string> _registryServerUrls;//可用服务器列表
        private string _configCenter; //实际可用的某个服务器地址
        private IDictionary<string, object> _configs;//读到的所有配置内容

        private bool _alreadyLoadConfigs;

        public ServiceConfig(IList<string> registryServerUrls)
        {
            _registryServerUrls = registryServerUrls;
        }


        public void ClearAllServicesInBuffer()
        {
            _configs = null;
            _alreadyLoadConfigs = false;
        }

        public async Task<IDictionary<string, object>> GetAllServicesToBufferAsync()
        {
            if (_alreadyLoadConfigs)
                return _configs;
            _configs = await GetAllServicesAsync();
            if (_configs == null)
                throw new MissConfigException("读配置服务器失败");
            _alreadyLoadConfigs = true;
            return _configs;
        }


     
        public T GetServiceFromBuffer<T>(string name) where T : class
        {
            if (!_alreadyLoadConfigs || _configs.Count == 0)
            {
                throw new MissConfigException(string.Format("本地读取服务({0})的配置时，系统还未连接到配置服务器", name));
            }
           
            object obj;
            if (!_configs.TryGetValue(name, out obj))
                throw new MissConfigException(string.Format("配置服务器没有配置服务({0})", name));
           
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (System.Exception)
            {
                throw new MissConfigException(string.Format("服务({0})的配置内容无法反序列化", name));
            }
        }


        public async Task<RunEnvironment> GetRunEnvironmentConfigAsync()
        {
            var result = await GetRunEnvironmentConfigFromNetAsync();
            if (result == null)
                throw new MissConfigException("读配置服务器(环境变量)失败");
            return result;
        }

        private async Task<RunEnvironment> GetRunEnvironmentConfigFromNetAsync()
        {
            if (_configCenter != null)
                return await GetRunEnvironmentConfigAsync(_configCenter + "manager/registries/environment");

            foreach (var serverUrl in _registryServerUrls)
            {
                var result = await GetRunEnvironmentConfigAsync(serverUrl + "manager/registries/environment");
                if (result != null) //找到一个ok的服务器
                {
                    _configCenter = serverUrl;
                    return result;
                }

            }
            return null;
        }

        private async Task<RunEnvironment> GetRunEnvironmentConfigAsync(string url)
        {
            try
            {
                var obj = await HttpHelper.GetAsync<RunEnvironment>(url, null);
                return obj;
            }
            catch (System.Exception)
            {
                return null;
            }

        }


        private async Task<IDictionary<string, object>> GetAllServicesAsync()
        {
            if (_configCenter != null)
                return await GetAllServicesAsync(_configCenter + "manager/registries");
            
            foreach (var serverUrl in _registryServerUrls)
            {
                var result = await GetAllServicesAsync(serverUrl + "manager/registries");
                if (result != null) //找到一个ok的服务器
                {
                    _configCenter = serverUrl;
                    return result;
                }
            
            }
            return null;
        }

        private async Task<IDictionary<string, object>> GetAllServicesAsync(string url)
        {
            IList<AnyServiceConfig<object>> list;
            try
            {
                list = await HttpHelper.GetAsync<IList<AnyServiceConfig<object>>>(url);
                if (list == null)
                    return null;
            }
            catch (System.Exception )//网络错误
            {
                return null;
            }
            try
            {
                return list.ToDictionary(cfg => cfg.ServiceName, cfg => cfg.ServiceConfig);
            }
            catch (System.Exception)
            {
                throw new MissConfigException("读到的配置表异常,可能有重复项或格式错误");
            }
            
        
        }

        /*     /// <summary>
             /// var g = await ServiceHelper.ServiceUtil.GetServicesByName("UserService"); UserService为服务名称
             /// </summary>
             /// <param name="serviceName">服务名称</param>
             /// <returns>JObject</returns>
             private static async Task<T> GetServiceAsync<T>(ServiceName serviceName) where T : class 
             {
                 var url = string.Format("{0}/{1}",ConfigCenter ,serviceName);
                 var urlParam = new
                 {
                     token = 1,
                 };

                 try//读取web参数会锁死不返回
                 {
                     var obj = await HttpHelper.GetAsync<T>(url, urlParam);
                     return obj;
                 }
                 catch
                 {
                     return null;
                 }
             }
     */
        /* private static T GetService<T>(ServiceName serviceName, T defaultValue = null) where T : class
         {
             var value = GetServiceAsync<T>(serviceName).Result;
             if (value == null)
                 return defaultValue;
             return value;
         }*/


        /*   /// <summary>
           /// 注册一个服务
           /// </summary>
           /// <param name="serviceName">服务名称</param>
           /// <param name="serviceConfig">一个配置对象,不能嵌套子对象</param>
           /// <returns>失败信息</returns>
           private static async Task<string> RegisterServiceAsync(string serviceName, object serviceConfig)
           {
               if (string.IsNullOrEmpty(serviceName))
                   return "服务名称不能为空白";

                if( serviceConfig == null)
                    return "服务参数不能为null";

               var url= ConfigCenter;
               var urlParam = new
               {
                   token = 1,
                   serviceName=serviceName.Replace('.', '-'),
               };
               try
               {
                   await HttpHelper.PostAsync<object>(url, serviceConfig,urlParam);
                   return null; //ok
               }
               catch (Exception ex)
               {
                   return ex.Message;
               }
           }*/
    }
}
