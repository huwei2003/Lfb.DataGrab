using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Comm.Cloud.RDS
{
    public static class DbDataReaderHelper
    {
        /// <summary>
        /// 建立属性值字典,key字段返回全部为小写!
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ToDictinaryList(this DbDataReader reader)
        {
            var list = new List<Dictionary<string, object>>();
            if (reader == null)
                return null;
            while (reader.Read())
            {
                var local = new Dictionary<string, object>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    var val = reader[name];
                    //将dbnull 转成null
                    if (val == DBNull.Value)
                        val = null;
                    local.Add(name.ToLower(), val);
                }
                list.Add(local);
            }
            return list;
        }
    }
}
