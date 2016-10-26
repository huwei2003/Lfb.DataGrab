using System;

namespace Comm.Global.Resource
{
    /// <summary>
    /// mysql表命名,全局表名唯一
    /// </summary>
    public static class RdsPrex
    {
        /// <summary>
        /// rds数据库名 xxxDb,只用于配置验证
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static string DbNames(string serviceName)
        {
            return string.Format("{0}db", serviceName.ToLower());

        }

        /// <summary>
        /// rds表名 tbxxxx
        /// </summary>
        /// <param name="tableClass">继承于IBaseTable的类</param>
        /// <returns></returns>
        public static string TableNames(Type tableClass)
        {
            var name = tableClass.Name.Substring(0, tableClass.Name.Length-5);
            return string.Format("tb{0}", name.ToLower());
      
        }



    }
}
