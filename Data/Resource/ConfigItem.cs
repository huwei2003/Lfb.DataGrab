using System;
using System.Collections.Generic;
using System.Text;

namespace Comm.Global.Resource
{
    public class ConfigItem
    {
        /// <summary>
        /// 运行环境
        /// </summary>
        public  string Environment;

        /// <summary>
        /// 注册中心文件
        /// </summary>
        public  string RegistryXmlFile;

        /// <summary>
        /// 监控中心配置文件
        /// </summary>
        public  string MonitorXmlFile;

        /// <summary>
        /// 注册中心地址
        /// </summary>
        public  string RegistryAddress;

        /// <summary>
        /// 测试中心地址
        /// </summary>
        public  string TesterAddress;
    }
}
