using System;
using System.Collections.Generic;

namespace Comm.Cloud.IRDS
{
    /// <summary>
    /// ���ݿ���������
    /// </summary>
    public struct DbForeignKeyDefine
    {
        public string MasterTableName;
        public string MasterFieldName;
        public string SlaveTableName;
        public string SlaveFieldName;
    }
}