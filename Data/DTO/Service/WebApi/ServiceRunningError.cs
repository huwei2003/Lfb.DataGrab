using System;

namespace Comm.Global.DTO.Service.WebApi
{
    /// <summary>
    /// 服务的启动状态
    /// </summary>
    [Flags]
    public enum ServiceRunningError
    {
        正常 = 0,

        /// <summary>
        /// 系统正在初始化，一般在联系配置服务器，初始化后自动清除标志
        /// </summary>
        初始化中 = 1,

        /// <summary>
        /// 初始化完毕，正在自检，一般在测试所有依赖的驱动是否装入,自检后清除标志
        /// </summary>
        自检中 = 2,

        /// <summary>
        /// 人工设置了暂停标志，可以人工取消，继续运行
        /// </summary>
        人工暂停 = 4,

        /// <summary>
        /// 服务在依赖注入解析发生严重异常，无法再继续服务
        /// </summary>
        依赖注入解析异常 = 8,

        /// <summary>
        /// 服务初始化时无法读取配置服务器环境配置，无法再继续服务
        /// </summary>
        读环境配置失败 = 16,

        /// <summary>
        /// 环境的注册服务器配置值和微服务值不同，无法再继续服务
        /// </summary>
        环境配置错误 = 32,

        /// <summary>
        /// 服务初始化时无法读取配置服务器服务配置，无法再继续服务
        /// </summary>
        读服务配置失败 = 64,

        /// <summary>
        /// 服务自检时发现某个依赖的服务没有配置项或配置项内容错误，无法再继续服务
        /// </summary>
        子服务配置错误 = 128,

        /// <summary>
        /// 服务运行时发现某个服务/数据库结构配置异常，无法再继续服务
        /// </summary>
        子服务异常或数据库结构错误 = 256
    }
}
