using System;
using System.Collections.Generic;

namespace Comm.Cloud.IRDS
{
    /// <summary>
    /// ���ݿ����
    /// </summary>
    public struct DbTableDefine
    {
        public string Name;
        public string PrimaryKey;
        public Dictionary<string,DbFieldDefine> FieldDefines;
    }
}