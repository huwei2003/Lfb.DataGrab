using System;

namespace Comm.Tools.Utility.Data
{
    /// <summary>
    /// 数据库表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TableAttribute : Attribute
    {
        public string[] PrimaryKeys;
        public TableAttribute(string primaryKeys)
        {
            PrimaryKeys = primaryKeys.Split(",");
        }
    }
}
