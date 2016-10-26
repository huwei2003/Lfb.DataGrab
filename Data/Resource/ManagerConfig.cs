namespace Comm.Global.Resource
{

    /// <summary>
    /// 管理服务器地址
    /// </summary>
    public static class ManagerConfig
    {
       

        /// <summary>
        /// 运行环境
        /// </summary>
#if online //线上
        public static ConfigItem ConfigItem = new ConfigItem
        {
            MonitorXmlFile = "services_online.xml",
            Environment= "online",
            RegistryXmlFile = "services_online.xml",
            RegistryAddress =    "http://10.160.24.199:8085/",
            //线上没有测试功能，不需要测试地址
        };
       
#elif test145 //前端开发测试
        public static ConfigItem ConfigItem = new ConfigItem
        {
            Environment= "test145",
            RegistryXmlFile = "services_test145.xml",
            MonitorXmlFile = "services_test145.xml",
            RegistryAddress =    "http://192.168.1.145:8085/",
            TesterAddress =     "http://192.168.1.145:8096/",
        };
#elif test147 //前端开发测试
        public static ConfigItem ConfigItem = new ConfigItem
        {
            Environment= "test147",
            RegistryXmlFile = "services_test147.xml",
            MonitorXmlFile = "services_test147.xml",
            RegistryAddress =    "http://192.168.1.147:8085/",
            TesterAddress =     "http://192.168.1.147:8096/",
        };
        
#else //默认为dev
        public static ConfigItem ConfigItem = new ConfigItem
        {
            Environment= "dev",
            RegistryXmlFile = "services_dev.xml",
            MonitorXmlFile = "services_dev.xml",
            RegistryAddress = "http://192.168.1.19:8085/",
            TesterAddress = "http://192.168.1.19:8096/",
        };
       
#endif



       
    }
}
