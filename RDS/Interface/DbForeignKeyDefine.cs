using System;
using System.Collections.Generic;

namespace Comm.Cloud.IRDS
{
    /// <summary>
    /// 数据库表外键定义
    /// </summary>
    public struct DbForeignKeyDefine
    {
        public string MasterTableName;
        public string MasterFieldName;
        public string SlaveTableName;
        public string SlaveFieldName;
    }
}