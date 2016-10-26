using Comm.Cloud.IRDS;
using Comm.Global.DTO.Factory;
using Comm.Global.Enum.Sys;

namespace Comm.Cloud.RDS
{
    /// <summary>
    /// 工厂类
    /// 使用服务名获取独立云配置参数
    /// 每个服务类对应一个Rds实例的一个数据库
    /// 表名保证在服务内(等价于库内)不冲突即可
    /// </summary>
    public  class RdsFactory : BaseFactory<IRds>
    {
        public static IRds GetInstanse(ServiceName hostServiceName)
        {
            if (GetMockInstanse != null)
                return GetMockInstanse;
            return new Rds(hostServiceName);
        }

        public static IRds GetInstanse(ServiceName hostServiceName,DbType dbType)
        {
            if (GetMockInstanse != null)
                return GetMockInstanse;
            return new Rds(hostServiceName, dbType);
        }
    }
}
