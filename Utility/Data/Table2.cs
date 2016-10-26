using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Comm.Tools.Utility.Data
{
    [Serializable]
    public class TableBase2
    {
        protected static readonly Log Log = new Log("SqlServer");

        /// <summary>
        /// 字段更改副本
        /// </summary>
        internal readonly Dictionary<string, object> __EditColumns = new Dictionary<string, object>();
        protected readonly Dictionary<string, object[]> __UpdateInfo = new Dictionary<string, object[]>();

        /// <summary>
        /// 表名
        /// </summary>
        internal string TableName { get; set; }

        internal string _updateFlag;
        /// <summary>
        /// 更新标识
        /// </summary>
        [System.Web.Script.Serialization.ScriptIgnore]
        public string UpdateFlag
        {
            get
            {
                return _updateFlag;
            }
            set
            {
                _updateFlag = value;
            }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        internal Type DbType;

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="propertyName">区分大小写的，以DAL.cs字段大小写为准</param>
        public object GetPropertyValue(string propertyName)
        {
            var filed = GetType().GetProperty(propertyName);
            return filed == null ? null : filed.GetValue(this, null);
        }

        public TableAttribute GetTableAttribute()
        {
            var attr = GetType().GetCustomAttributes(typeof(TableAttribute), false).Cast<TableAttribute>().FirstOrDefault();
            if (attr == null)
            {
                throw new Exception("表{0}没有标明TableAttribute特性".Formats(TableName));
            }
            return attr;
        }

        /// <summary>
        /// 属性重新赋值(为了使对象的引用不发生变化)
        /// </summary>
        /// <param name="obj">新的值对象</param>
        public void PropertiesUpdate(TableBase2 obj)
        {
            if (obj != null && obj.UpdateFlag != UpdateFlag)
                GetType().GetProperties().Each(a => a.SetValue(this, a.GetValue(obj)));
        }
    }

    [Serializable]
    public class Table2<T, TK> : TableBase2
    {
        protected static Dictionary<string, string> __Summary;

        protected Table2()
        {
            TableName = typeof(T).Name;
            DbType = typeof(TK);
        }

        /// <summary>
        /// 获取属性描述
        /// </summary>
        /// <param name="fieldName">区分大小写的，以DAL.cs字段大小写为准</param>
        public string GetPropertySummary(string fieldName)
        {
            string s;
            return __Summary.TryGetValue(fieldName, out s) ? s : "";
        }

        /// <summary>
        /// 添加记录（存在condition就不添加）
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int Insert(string condition = null)
        {
            if (__EditColumns.Count == 0)
            {
                return -1;
            }

            var sb = new StringBuilder();
            var parameters = new List<object>();
            if (condition.NotNullOrEmpty())
            {
                sb.Append("if not exists(select 1 from ").Append(TableName).Append(" where ").Append(condition).Append(")\n");
            }
            sb.Append("insert into ").Append(TableName).Append(" (");
            foreach (var column in __EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("],");
                parameters.Add(column.Value);
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")values(");
            var columnCount = __EditColumns.Count;
            for (var i = 0; i < columnCount; i++)
            {
                sb.Append("?,");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");

            __EditColumns.Clear();

            return DbBase2<TK>.ExecuteNonQuery(sb.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 添加记录（存在condition就不添加）
        /// </summary>
        /// <returns>返回id，仅支持有自增ID的表</returns>
        public int InsertId(string condition = null)
        {
            if (__EditColumns.Count == 0)
            {
                return -1;
            }

            var sb = new StringBuilder();
            var parameters = new List<object>();
            if (condition.NotNullOrEmpty())
            {
                sb.Append("if not exists(select 1 from ").Append(TableName).Append(" where ").Append(condition).Append(")\nbegin\n");
            }
            sb.Append("insert into ").Append(TableName).Append(" (");
            foreach (var column in __EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("],");
                parameters.Add(column.Value);
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")values(");
            var columnCount = __EditColumns.Count;
            for (var i = 0; i < columnCount; i++)
            {
                sb.Append("?,");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")\nselect SCOPE_IDENTITY()\n");
            if (condition.NotNullOrEmpty())
            {
                sb.Append("end else\nselect max(id) from ").Append(TableName);
                sb.Append(" where ").Append(condition);
            }

            __EditColumns.Clear();

            return DbBase2<TK>.ExecuteScalarTrans(-1, sb.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 添加记录（存在condition就不添加）
        /// </summary>
        /// <returns>返回id，仅支持有自增ID的表</returns>
        public int InsertIdNoTrans(string condition = null)
        {
            if (__EditColumns.Count == 0)
            {
                return -1;
            }

            var sb = new StringBuilder();
            var parameters = new List<object>();
            if (condition.NotNullOrEmpty())
            {
                sb.Append("if not exists(select 1 from ").Append(TableName).Append(" where ").Append(condition).Append(")\nbegin\n");
            }
            sb.Append("insert into ").Append(TableName).Append(" (");
            foreach (var column in __EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("],");
                parameters.Add(column.Value);
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")values(");
            var columnCount = __EditColumns.Count;
            for (var i = 0; i < columnCount; i++)
            {
                sb.Append("?,");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")\nselect SCOPE_IDENTITY()\n");
            if (condition.NotNullOrEmpty())
            {
                sb.Append("end else\nselect max(id) from ").Append(TableName);
                sb.Append(" where ").Append(condition);
            }

            __EditColumns.Clear();

            return DbBase2<TK>.ExecuteScalarNoTrans(-1, sb.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 更新记录（存在更新，不存在添加）
        /// </summary>
        /// <returns>返回id</returns>
        public int InsertOrUpdateId(string condition, string idName = "id")
        {
            if (__EditColumns.Count == 0)
            {
                return -1;
            }

            var sb = new StringBuilder();
            var parameters = new List<object>();
            if (condition.NotNullOrEmpty())
            {
                sb.Append("if not exists(select 1 from ").Append(TableName).Append(" where ").Append(condition).Append(")\nbegin\n");
            }
            sb.Append("insert into ").Append(TableName).Append(" (");
            foreach (var column in __EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("],");
                parameters.Add(column.Value);
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")values(");
            var columnCount = __EditColumns.Count;
            for (var i = 0; i < columnCount; i++)
            {
                sb.Append("?,");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")\nselect SCOPE_IDENTITY()\nend else begin\n");
            sb.Append("update ").Append(TableName).Append(" set ");
            foreach (var column in __EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("] = ?,");
                parameters.Add(column.Value);
            }
            if (sb.ToString().EndsWith(","))
            {
                sb.Remove(sb.Length - 1, 1);
            }
            if (condition.HasValue())
            {
                sb.Append(" where ").Append(condition);
            }
            sb.Append("\nselect max(").Append(idName).Append(") from ").Append(TableName);
            if (condition.NotNullOrEmpty())
            {
                sb.Append(" where ").Append(condition);
            }
            sb.Append(" end");

            __EditColumns.Clear();

            return DbBase2<TK>.ExecuteScalarTrans(-1, sb.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 更新记录（存在更新，不存在添加）
        /// </summary>
        /// <returns>返回id</returns>
        public int InsertOrUpdateIdNoTrans(string condition, string idName = "id")
        {
            if (__EditColumns.Count == 0)
            {
                return -1;
            }

            var sb = new StringBuilder();
            var parameters = new List<object>();
            if (condition.NotNullOrEmpty())
            {
                sb.Append("if not exists(select 1 from ").Append(TableName).Append(" where ").Append(condition).Append(")\nbegin\n");
            }
            sb.Append("insert into ").Append(TableName).Append(" (");
            foreach (var column in __EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("],");
                parameters.Add(column.Value);
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")values(");
            var columnCount = __EditColumns.Count;
            for (var i = 0; i < columnCount; i++)
            {
                sb.Append("?,");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")\nselect SCOPE_IDENTITY()\nend else begin\n");
            sb.Append("update ").Append(TableName).Append(" set ");
            foreach (var column in __EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("] = ?,");
                parameters.Add(column.Value);
            }
            if (sb.ToString().EndsWith(","))
            {
                sb.Remove(sb.Length - 1, 1);
            }
            if (condition.HasValue())
            {
                sb.Append(" where ").Append(condition);
            }
            sb.Append("\nselect max(").Append(idName).Append(") from ").Append(TableName);
            if (condition.NotNullOrEmpty())
            {
                sb.Append(" where ").Append(condition);
            }
            sb.Append(" end");

            __EditColumns.Clear();

            return DbBase2<TK>.ExecuteScalarNoTrans(-1, sb.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 根据主键字段更新记录
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int Update()
        {
            var sb = new StringBuilder();
            var parameters = new List<object>();
            var type = GetType();
            foreach (var pk in GetTableAttribute().PrimaryKeys)
            {
                sb.Append("[").Append(pk).Append("]=? AND ");
                parameters.Add(type.GetProperty(pk).GetValue(this));
            }
            return Update(sb.Remove(sb.Length - 5, 5).ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="condition">条件(格式字段=?),例:b=? and c = ? and d =?</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>受影响的行数</returns>
        public int Update(string condition, params object[] parameters)
        {
            if (__EditColumns.Count == 0 || !condition.HasValue())
            {
                return 0;
            }

            var paras = new List<object>();
            var sb = new StringBuilder();
            sb.Append("update ").Append(TableName).Append(" set ");
            foreach (var column in __EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("] = ?,");
                paras.Add(column.Value);
            }
            if (sb.ToString().EndsWith(","))
            {
                sb.Remove(sb.Length - 1, 1);
            }

            sb.Append(" where ").Append(condition);

            paras.AddRange(parameters);

            Clear();

            return DbBase2<TK>.ExecuteNonQuery(sb.ToString(), paras.ToArray());
        }

        /// <summary>
        /// 根据主键字段更新记录
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int UpdateNoTrans()
        {
            var sb = new StringBuilder();
            var parameters = new List<object>();
            var type = GetType();
            foreach (var pk in GetTableAttribute().PrimaryKeys)
            {
                sb.Append("[").Append(pk).Append("]=? AND ");
                parameters.Add(type.GetProperty(pk).GetValue(this));
            }
            return UpdateNoTrans(sb.Remove(sb.Length - 5, 5).ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 更新记录(无事物)
        /// </summary>
        /// <param name="condition">条件(格式字段=?),例:b=? and c = ? and d =?</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>受影响的行数</returns>
        public int UpdateNoTrans(string condition, params object[] parameters)
        {
            if (__EditColumns.Count == 0 || !condition.HasValue())
            {
                return 0;
            }

            var paras = new List<object>();
            var sb = new StringBuilder();
            sb.Append("update ").Append(TableName).Append(" set ");
            foreach (var column in __EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("] = ?,");
                paras.Add(column.Value);
            }
            if (sb.ToString().EndsWith(","))
            {
                sb.Remove(sb.Length - 1, 1);
            }

            sb.Append(" where ").Append(condition);

            paras.AddRange(parameters);

            Clear();

            return DbBase2<TK>.ExecuteNonQueryNoTrans(sb.ToString(), paras.ToArray());
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="condition">条件(格式字段=?),例:b=? and c = ? and d =?</param>
        /// <param name="set">用于更新时间取数据库时间，例：UpdateTime=GETDATE()</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>受影响的行数</returns>
        public int UpdateSet(string condition, string set, params object[] parameters)
        {
            if (__EditColumns.Count == 0)
            {
                return 0;
            }
            if (set.IsNullOrEmpty())
            {
                return -2;
            }

            var paras = new List<object>();
            var sb = new StringBuilder();
            sb.Append("update ").Append(TableName).Append(" set ").Append(set).Append(",");
            foreach (var column in __EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("] = ?,");
                paras.Add(column.Value);
            }
            if (sb.ToString().EndsWith(","))
            {
                sb.Remove(sb.Length - 1, 1);
            }

            if (condition.HasValue())
            {
                sb.Append(" where ").Append(condition);
            }

            paras.AddRange(parameters);

            Clear();

            return DbBase2<TK>.ExecuteNonQuery(sb.ToString(), paras.ToArray());
        }

        /// <summary>
        /// 批量更新记录 Dictionary[条件,表] 
        /// </summary>
        public static int Update(IEnumerable<KeyValuePair<string, T>> tables)
        {
            var tabs = tables.Select(a => new KeyValuePair<string, Table2<T, TK>>(a.Key, a.Value as Table2<T, TK>));
            var updateTabs = tabs.Where(a => a.Value.__EditColumns.Count > 0).ToArray();
            if (updateTabs.Length == 0)
            {
                return 0;
            }
            var sw = Stopwatch.StartNew();
            var result = -1;
            var sqls = new List<KeyValuePair<string, List<DbParameter>>>();
            try
            {
                var sb = new StringBuilder();
                var paraIndex = 0;
                var parameters = new List<DbParameter>();
                foreach (var table in updateTabs)
                {
                    if (table.Value.__EditColumns.Count > 0)
                    {
                        sb.Append("\nupdate ").Append(table.Value.TableName).Append(" set ");
                        foreach (var column in table.Value.__EditColumns)
                        {
                            sb.Append("[").Append(column.Key).Append("]=@p").Append(parameters.Count.ToString()).Append(",");
                            parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + parameters.Count, column.Value));
                            paraIndex += 2;
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(" where ").Append(table.Key);

                        if (paraIndex > 1500)
                        {
                            sqls.Add(new KeyValuePair<string, List<DbParameter>>(sb.ToString(), parameters));
                            sb = new StringBuilder();
                            paraIndex = 0;
                            parameters = new List<DbParameter>();
                        }
                    }
                }
                if (sb.Length > 0)
                {
                    sqls.Add(new KeyValuePair<string, List<DbParameter>>(sb.ToString(), parameters));
                }

                var option = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                    Timeout = new TimeSpan(0, 2, 0)
                };
                using (var trans = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    using (var conn = DbBase2<TK>.GetSqlConnection(false))
                    {
                        foreach (var sql in sqls)
                        {
                            var cmd = new SqlCommand(sql.Key, conn)
                            {
                                CommandType = CommandType.Text
                            };
                            cmd.Parameters.AddRange(sql.Value.ToArray());
                            result += cmd.ExecuteNonQuery();
                        }
                        trans.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }

            var useTime = sw.ElapsedMilliseconds;
            var logStr = "Table.Update({0}) 执行时间 {1} 毫秒".Formats(sqls.Join("\n"), useTime.ToString("F0"));
            if (useTime > DbBase2<TK>.WornigMilliseconds)
            {
                Log.Warn(logStr);
            }

            Log.Debug(logStr);

            return result;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="condition">条件(格式字段=?),例:b=? and c = ? and d =?</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>受影响的行数</returns>
        public static int Delete(string condition, params object[] parameters)
        {
            var sb = new StringBuilder();

            sb.Append("delete from ").Append(typeof(T).Name);
            sb.Append(" where ").Append(condition);

            return DbBase2<TK>.ExecuteNonQuery(sb.ToString(), parameters);
        }

        /// <summary>
        /// 表最后更新标识
        /// </summary>
        public static string MaxUpdateFlag
        {
            get
            {
                var sql = "SELECT MAX(UpdateFlag) FROM " + typeof(T).Name;
                var flag = DbBase2<TK>.ExecuteScalarNoTrans(new byte[0], sql);
                return flag.TimestampToString();
            }
        }

        /// <summary>
        /// 返回表中上次更新标识之后更新过的数据
        /// </summary>
        public static List<T> GetDataAfterFlag(string lastUpdateFlag)
        {
            var sql = "SELECT * FROM {0} WHERE UpdateFlag>{1}".Formats(typeof(T).Name, lastUpdateFlag);
            return DbBase2<TK>.Select<T>(sql);
        }

        /// <summary>
        /// 返回表中所有数据
        /// </summary>
        public static List<T> GetDataAll()
        {
            var sql = "SELECT * FROM " + typeof(T).Name;
            return DbBase2<TK>.SelectCache<T>(sql, 600);
        }

        /// <summary>
        /// 返回表的记录数
        /// </summary>
        public static int GetCount()
        {
            var sql = "SELECT COUNT(1) FROM " + typeof(T).Name;
            return DbBase2<TK>.ExecuteScalarNoTrans(0, sql);
        }

        /// <summary>
        /// 设置表字段更改副本
        /// </summary>
        public void SetColumn(string name, object oldValue, object newValue)
        {
            if (newValue != null && newValue.Equals(oldValue))
            {
                return;
            }
            __EditColumns[name] = newValue;
            __UpdateInfo[name] = new[] { oldValue, newValue };
        }

        /// <summary>
        /// 获取更改字段的数量
        /// </summary>
        public int GetColumnEditCount()
        {
            return __EditColumns.Count;
        }

        /// <summary>
        /// 清空表字段更改副本
        /// </summary>
        public void Clear()
        {
            __EditColumns.Clear();
        }

        /// <summary>
        /// 列最小值
        /// </summary>
        public static TR Min<TR>(string columnName, TR defaultValue = default (TR))
        {
            var sql = "SELECT MIN([{0}]) FROM {1}".Formats(columnName, typeof(T).Name);
            return DbBase2<TK>.ExecuteScalarNoTrans(defaultValue, sql);
        }

        /// <summary>
        /// 列最大值
        /// </summary>
        public static TR Max<TR>(string columnName, TR defaultValue = default (TR))
        {
            var sql = "SELECT MAX([{0}]) FROM {1}".Formats(columnName, typeof(T).Name);
            return DbBase2<TK>.ExecuteScalarNoTrans(defaultValue, sql);
        }

        /// <summary>
        /// 插入数据内容
        /// </summary>
        public Dictionary<string, object> GetInsertContext()
        {
            return __EditColumns.Count == 0 ? __EditColumns : new[] { new KeyValuePair<string, object>("Id", TableName) }.Concat(__EditColumns).ToDictionary(a => a.Key, a => a.Value);
        }
        /// <summary>
        /// 插入数据描述
        /// </summary>
        public Dictionary<string, object> GetInsertSummary()
        {
            var err = __EditColumns.ToDictionary(a => a.Key, a => GetPropertySummary(a.Key)).GroupBy(a => a.Value)
                .Where(a => a.Count() > 1)
                .Select(a => "表字段{0}描述相同，均为'{1}'".Formats(a.Select(b => b.Key).Join(","), a.Key)).Join("；");
            if (err.HasValue())
            {
                throw new Exception(err);
            }
            return __EditColumns.ToDictionary(a => GetPropertySummary(a.Key), a => a.Value);
        }

        /// <summary>
        /// 更新内容
        /// </summary>
        /// <param name="summaryFields">附加描述字段(尽量唯一)逗号间隔</param>
        public Dictionary<string, object> GetUpdateContext(string summaryFields)
        {
            var result = new Dictionary<string, object>();
            result["TableName"] = TableName;
            result["Id"] = GetPropertyValue("Id");
            summaryFields.Split(",").Where(f => f != "Id").Except(__UpdateInfo.Keys).Each(f =>
            {
                result[f] = GetPropertyValue(f);
            });
            result["Update"] = __UpdateInfo;
            return result;
        }
        /// <summary>
        /// 更新描述
        /// </summary>
        /// <param name="summaryFields">附加描述字段(尽量唯一)逗号间隔</param>
        public Dictionary<string, object> GetUpdateSummary(string summaryFields)
        {
            var summary = summaryFields.Split(",").Except(__UpdateInfo.Keys).Distinct().ToDictionary(a => a, GetPropertySummary);
            var err = summary.GroupBy(a => a.Value)
                .Where(a => a.Count() > 1)
                .Select(a => "表字段{0}描述相同，均为'{1}'".Formats(a.Select(b => b.Key).Join(","), a.Key)).Join("；");
            if (err.HasValue())
            {
                throw new Exception(err);
            }
            var result = new Dictionary<string, object>();
            summary.Each(s =>
            {
                result[s.Value] = GetPropertyValue(s.Key);
            });
            result["数据变动"] = __UpdateInfo.ToDictionary(a => GetPropertySummary(a.Key), a => a.Value);
            return result;
        }

        /// <summary>
        /// 删除描述
        /// </summary>
        /// <param name="summaryFields">附加描述字段(尽量唯一)逗号间隔</param>
        public Dictionary<string, object> GetDeleteSummary(string summaryFields)
        {
            var summary = summaryFields.Split(",").ToDictionary(a => a, GetPropertySummary);
            var err = summary.GroupBy(a => a.Value)
                .Where(a => a.Count() > 1)
                .Select(a => "表字段{0}描述相同，均为'{1}'".Formats(a.Select(b => b.Key).Join(","), a.Key)).Join("；");
            if (err.HasValue())
            {
                throw new Exception(err);
            }
            var result = new Dictionary<string, object>();
            summary.Each(s =>
            {
                result[s.Value] = GetPropertyValue(s.Key);
            });
            return result;
        }
    }
}