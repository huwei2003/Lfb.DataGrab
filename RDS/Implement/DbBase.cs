using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Transactions;
using Comm.Cloud.IRDS;
using Comm.Global.Db;
using Comm.Global.Exception;
using Comm.Tools.Utility;
using IsolationLevel = System.Transactions.IsolationLevel;
using PagedDataTableResult = Comm.Global.Db.PagedDataTableResult;

namespace Comm.Cloud.RDS
{
    public abstract class DbBase
    {
        private static readonly Log Logger = new Log("DBBase");

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsOn;

        /// <summary>
        /// 是否开启读写分离
        /// </summary>
        protected bool EnableReadOnly;

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString = "";

        /// <summary>
        /// 执行时间警告毫秒数
        /// </summary>
        public int WornigMilliseconds = 1000;

        /// <summary>
        /// 缓存时间  秒
        /// </summary>
        private static int cacheSecond = 5*60;

        #region === base op ===

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close(DbConnection conn)
        {
            if (conn == null)
            {
                return;
            }
            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message + e.StackTrace);
            }
        }


        public abstract DbParameter CreateDbParameter(string name, object value);

        /// <summary>
        /// 事务选项：事务级别为ReadCommitted，2分钟事务超时
        /// </summary>
        public readonly TransactionOptions TransactionOption = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = new TimeSpan(0, 2, 0)
        };

        public abstract DbCommand CreateDbCommand(string strSql, DbConnection conn);


        public abstract DbDataAdapter CreateDbDataAdapter(DbCommand cmd);


        private string PrepareParameter(string strSql, object[] parameters)
        {
            return PrepareParameter(strSql, parameters, 0);
        }

        public abstract string PrepareParameter(string strSql, object[] parameters, int paraStartIndex);
        

        /// <summary>
        /// 添加、更新、删除
        /// </summary>
        /// <param name="strSql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQueryTrans(string strSql, params object[] parameters)
        {
            if (strSql.IsNullOrEmpty())
            {
                return -1;
            }
            var sw = Stopwatch.StartNew();
            if (strSql.Trim().Length == 0)
            {
                return 0;
            }

            DbConnection conn = null;
            DbTransaction trans = null;

            try
            {
                conn = GetConnection(false);
                strSql = PrepareParameter(strSql, parameters);
                trans = conn.BeginTransaction();
                var cmd = CreateDbCommand(strSql, conn);
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var result = cmd.ExecuteNonQuery();
                trans.Commit();

                return result;
            }
            catch (Exception e)
            {
                if (trans != null)
                {
                    try
                    {
                        trans.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("({0})事务回滚错误：{1}".Formats(strSql, ex.Message));
                    }
                }
                Logger.Error("({0})：{1}".Formats(strSql, e.Message));

                return -1;
            }
            finally
            {
                Close(conn);
                var useTime = sw.ElapsedMilliseconds;
                var logStr = "({0}) 执行时间 {1} 毫秒".Formats(strSql, useTime.ToString("F0"));
                if (useTime > WornigMilliseconds)
                {
                    Logger.Warn(logStr);
                }
                Logger.Debug(logStr);
            }
        }

        /// <summary>
        /// 添加、更新、删除(无事务)
        /// </summary>
        /// <param name="strSql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string strSql, params object[] parameters)
        {
            if (strSql.IsNullOrEmpty())
            {
                return -1;
            }
            if (strSql.Trim().Length == 0)
            {
                return 0;
            }
            using (var conn = GetConnection(false))
            {
                strSql = PrepareParameter(strSql, parameters);
                var cmd = CreateDbCommand(strSql, conn);
                cmd.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        /// <summary>
        /// 执行更新并查询（无事物），并返回查询多返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="strSql">查询语句,参数用?号代替</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>结果集中第一行的第一列</returns>
        public T ExecuteScalar<T>(T defaultValue, string strSql, params object[] parameters)
        {
            if (strSql.IsNullOrEmpty())
            {
                return defaultValue;
            }
            if (strSql.Trim().Length == 0)
            {
                return defaultValue;
            }
            using (var conn = GetConnection(false))
            {
                strSql = PrepareParameter(strSql, parameters);
                var cmd = CreateDbCommand(strSql, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var result = cmd.ExecuteScalar();
                return result.ConvertTo(defaultValue);
            }
        }

        /// <summary>
        /// 执行更新并查询（带事物回滚），并返回查询的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="strSql">查询语句,参数用?号代替</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>结果集中第一行的第一列</returns>
        public T ExecuteScalarTrans<T>(T defaultValue, string strSql, params object[] parameters)
        {
            if (strSql.IsNullOrEmpty())
            {
                return defaultValue;
            }
            var sw = Stopwatch.StartNew();
            DbConnection conn = null;
            DbTransaction trans = null;
            object result;
            try
            {
                conn = GetConnection(false);
                strSql = PrepareParameter(strSql, parameters);
                trans = conn.BeginTransaction();
                var cmd = CreateDbCommand(strSql, conn);
                cmd.Transaction = trans;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                result = cmd.ExecuteScalar();
                trans.Commit();
            }
            catch (Exception e)
            {
                if (trans != null)
                {
                    try
                    {
                        trans.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("({0})事务回滚错误：{1}".Formats(strSql, ex.Message));
                    }
                }
                Logger.Error("({0})：{1}".Formats(strSql, e.Message));

                return defaultValue;
            }
            finally
            {
                Close(conn);
                var useTime = sw.ElapsedMilliseconds;
                var logStr = "({0}) 执行时间 {1} 毫秒".Formats(strSql, useTime.ToString("F0"));
                if (useTime > WornigMilliseconds)
                {
                    Logger.Warn(logStr);
                }
                Logger.Debug(logStr);
            }

            return result.ConvertTo(defaultValue);
        }


        public List<Dictionary<string, object>> GetDictionarys(string strSql, params object[] parameters)
        {
            if (strSql.IsNullOrEmpty())
            {
                return null;
            }
            if (strSql.Trim().Length == 0)
            {
                return null;
            }
            using (var conn = GetConnection(false))
            {
                strSql = PrepareParameter(strSql, parameters);
                var cmd = CreateDbCommand(strSql, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var result = cmd.ExecuteReader();
                return result.ToDictinaryList();
                
            }
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="isReadOnly">是否只读</param>
        public abstract DbConnection GetConnection(bool isReadOnly);

        ///// <summary>
        ///// 获取数据库连接
        ///// </summary>
        ///// <param name="isReadOnly">是否只读</param>
        //public abstract SqlConnection GetSqlConnection(bool isReadOnly);

        #endregion

        #region === select op ===
        /// <summary>
        /// 根据查询语句从数据库检索数据
        /// </summary>
        /// <param name="strSql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>有数据则返回DataTable数据集，否则返回null</returns>
        public DataTable SelectDataTable(string strSql, params object[] parameters)
        {
            if (strSql.IsNullOrEmpty())
            {
                return null;
            }

            var sw = Stopwatch.StartNew();
            DbConnection conn = null;

            try
            {
                conn = GetConnection(true);
                strSql = PrepareParameter(strSql, parameters);
                var cmd = CreateDbCommand(strSql, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var da = CreateDbDataAdapter(cmd);
                
                var dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();

                return dt;
            }
            catch (Exception e)
            {
                Logger.Error("({0})：{1} {2}".Formats(strSql, e.Message, e.StackTrace));
                return null;
            }
            finally
            {
                Close(conn);
                var useTime = sw.ElapsedMilliseconds;
                var logStr = "({0}) 执行时间 {1} 毫秒".Formats(strSql, useTime.ToString("F0"));
                if (useTime > WornigMilliseconds)
                {
                    Logger.Warn(logStr);
                }
                Logger.Debug(logStr);
            }
        }

        

        /// <summary>
        /// 查询表对象
        /// </summary>
        /// <param name="strSql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>表对象,只绑定选择的字段值</returns>
        public List<T> Select<T>(string strSql,  params object[] parameters)
        {
            if (strSql.IsNullOrEmpty())
            {
                return null;
            }
            
            var dt = SelectDataTable(strSql, parameters);
            if (dt != null)
            {
                var result = dt.ToList<T>();
                
                return result;
            }
            return null;
        }

        /// <summary>
        /// 查询表对象中的一条记录，一般用于id= 或其它字段作条件取某个记录的情况
        /// </summary>
        /// <param name="whereFields">where条件的字段</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <param name="tableName">表名</param>
        /// <returns>表对象,只绑定选择的字段值</returns>
        public T GetSingle<T>(string tableName,List<string> whereFields, params object[] parameters)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT * FROM " + tableName);
            if (whereFields != null && whereFields.Count > 0)
            {
                sb.Append(" WHERE ");
                foreach (var field in whereFields)
                {
                    sb.Append("[").Append(field).Append("]=? AND ");
                }
                sb.Append(" 1=1 ");
            }
            var strSql = sb.ToString();
            if (strSql.IsNullOrEmpty())
            {
                return default(T);
            }
            var result = Select<T>(strSql, parameters);

            if (result!=null && result.Count > 0)
                return result[0];
            else
                return default(T);
        }

        /// <summary>
        /// 根据查询语句从数据库检索数据
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        public DataSet SelectDataSet(string sql, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
            {
                return null;
            }

            var sw = Stopwatch.StartNew();
            DbConnection conn = null;
            var ds = new DataSet();
            try
            {
                conn = GetConnection(true);
                sql = PrepareParameter(sql, parameters);
                var cmd = CreateDbCommand(sql, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var da = CreateDbDataAdapter(cmd);
                
                da.Fill(ds);

                cmd.Parameters.Clear();
            }
            catch (SystemException e)
            {
                Logger.Error("({0})：{1}".Formats(sql, e.Message));

                return null;
            }
            finally
            {
                Close(conn);
                var useTime = sw.ElapsedMilliseconds;
                var logStr = "({0}) 执行时间 {1} 毫秒".Formats(sql, useTime.ToString("F0"));
                if (useTime > WornigMilliseconds)
                {
                    Logger.Warn(logStr);
                }
                Logger.Debug(logStr);
            }
            return ds;
        }
        /// <summary>
        /// 根据查询语句从数据库检索数据(含事务)
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        public DataSet SelectDataSetTrans(string sql, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
            {
                return null;
            }

            var sw = Stopwatch.StartNew();
            DbConnection conn = null;
            DbTransaction trans = null;

            try
            {
                conn = GetConnection(false);
                trans = conn.BeginTransaction();
                sql = PrepareParameter(sql, parameters);
                var cmd = CreateDbCommand(sql, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.Transaction = trans;
                var da = CreateDbDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds);

                cmd.Parameters.Clear();
                trans.Commit();

                return ds;
            }
            catch (Exception e)
            {
                if (trans != null)
                {
                    try
                    {
                        trans.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("({0})事务回滚错误：{1}".Formats(sql, ex.Message));
                    }
                }
                Logger.Error("({0})：{1}".Formats(sql, e.Message));
                throw new Exception(e.Message.Split("\n")[0]);
            }
            finally
            {
                Close(conn);
                var useTime = sw.ElapsedMilliseconds;
                var logStr = "({0}) 执行时间 {1} 毫秒".Formats(sql, useTime.ToString("F0"));
                if (useTime > WornigMilliseconds)
                {
                    Logger.Warn(logStr);
                }
                Logger.Debug(logStr);
            }
        }

        /// <summary>
        /// 分页：支持参数化{0},{1}...类似Format
        /// </summary>
        public Global.Db.PagedResult<T> SelectPage<T>(string qrySql, int pageSize, int pageIndex, params object[] parameters)
        {
            return SelectPage<T>(qrySql, "", pageSize, pageIndex, parameters);
            
        }
        /// <summary>
        /// 分页：支持参数化{0},{1}...类似Format
        /// </summary>
        private Global.Db.PagedResult<T> SelectPage<T>(string qrySql, string statSql, int pageSize, int pageIndex, params object[] parameters)
        {
            var paged = SelectPage(qrySql, statSql, pageSize, pageIndex, parameters);
            return paged == null ? null : paged.Cast<T>();
        }
        private PagedDataTableResult SelectPage(string qrySql, string statSql, int pageSize, int pageIndex, params object[] parameters)
        {
            var index = pageIndex;
            var ds = SelectPaged(qrySql, statSql, pageSize, index, parameters);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            //Tables[0] 无数据时判断
            var rowCount = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                rowCount = ds.Tables[0].Rows[0][0].ConvertTo(0);
            }
            var pageCount = Math.Ceiling(1M * rowCount / pageSize).ConvertTo(0);
            var pagedResult = new PagedDataTableResult(pageSize, pageIndex)
            {
                PageIndex = index,
                PageCount = pageCount,
                RowCount = rowCount,
                Rows = ds.Tables[1]
            };
            if (statSql.HasValue())
            {
                pagedResult.Stat = ds.Tables[2].Rows[0];
            }
            return pagedResult;
        }

        private  DataSet SelectPaged(string qrySql, string statSql, int pageSize, int pageIndex, params object[] parameters)
        {
            var strSql = GetSelectPagedSql(qrySql, statSql, pageSize, pageIndex);
            var paramsList = new List<object>();
            if (parameters != null)
            {
                paramsList.AddRange(parameters.ToList());
            }

            paramsList.Add(pageSize);
            paramsList.Add(pageIndex);
            parameters = paramsList.ToArray();
            var ds = SelectDataSet(strSql, parameters);
            return ds;
        }

        public abstract string GetSelectPagedSql(string qrySql, string statSql, int pageSize, int pageIndex);

        public TR Min<TR>(string tableName, string columnName, TR defaultValue = default(TR))
        {
            var strSql = "SELECT MIN([{0}]) FROM {1}".Formats(columnName, tableName);
            return ExecuteScalar(defaultValue, strSql);
        }
        public TR Max<TR>(string tableName, string columnName, TR defaultValue = default(TR))
        {
            var strSql = "SELECT MAX([{0}]) FROM {1}".Formats(columnName, tableName);
            return ExecuteScalar(defaultValue, strSql);
        }

        public List<T> GetDataAll<T>(string tableName, string sqlWhere, params object[] parameters)
        {
            var strSql = "SELECT * FROM " + tableName;
            if (!string.IsNullOrWhiteSpace(sqlWhere))
            {
                strSql += " WHERE {0}".Formats(sqlWhere);
            }
            return Select<T>(strSql,parameters);
        }

        public int GetCount(string tableName,string sqlWhere)
        {
            var strSql = "SELECT COUNT(1) FROM " + tableName;
            if (!string.IsNullOrWhiteSpace(sqlWhere))
            {
                strSql += " WHERE {0}".Formats(sqlWhere);
            }
            return ExecuteScalar(0, strSql);
        }

        public bool Exists(string tableName, string sqlWhere, params object[] parameters)
        {
            var strSql = "SELECT COUNT(*) FROM " + tableName;
            if (!string.IsNullOrWhiteSpace(sqlWhere))
            {
                strSql += " WHERE {0}".Formats(sqlWhere);
            }
            var result = ExecuteScalar(0, strSql, parameters);
            if (result > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region === select op by Cache ===
        /// <summary>
        /// 根据查询语句从数据库检索数据
        /// </summary>
        /// <param name="listResult"></param>
        /// <param name="strSql">查询语句,参数用?号代替</param>
        /// <param name="cacheSeconds">缓存秒数</param>
        /// <param name="checkFlag">是否校验UpdateFlag</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>有数据则返回DataTable数据集，否则返回null</returns>
        public DataTable SelectDataTableCache<T>(List<T> listResult, string strSql, int cacheSeconds, bool checkFlag, params object[] parameters)
        {
            if (strSql.IsNullOrEmpty())
            {
                return null;
            }

            var sw = Stopwatch.StartNew();
            DbConnection conn = null;

            try
            {
                var useCache = cacheSeconds > 0;
                var cacheKey = "";
                if (useCache)
                {
                    cacheKey = (strSql + parameters.Join(",")).Md5();
                }

                strSql = PrepareParameter(strSql, parameters);
                DataTable dt = null;
                DbCommand cmd;
                List<T> listData = null;
                var isListResult = listResult != null;
                var tableCache = useCache && !isListResult;

                if (useCache)
                {
                    var lsql = strSql.Trim().ToLower();
                    var hasCache = false;
                    if (tableCache)
                    {
                        dt = Cache.GetCache<DataTable>(cacheKey);
                        hasCache = dt != null && dt.Rows.Count > 0;
                    }
                    else
                    {
                        listData = Cache.GetCache<List<T>>(cacheKey);
                        if (listData != null)
                        {
                            listResult.AddRange(listData);
                            hasCache = true;
                        }
                    }

                    if (hasCache)
                    {
                        if (!checkFlag)
                        {
                            return dt;
                        }
                        string lastMaxFlag;
                        var lastRowCount = 0;
                        if (tableCache)
                        {
                            lastMaxFlag = dt.Select().Max(a => (a["updateflag"] as byte[]).TimestampToString());
                            lastRowCount = dt.Rows.Count;
                        }
                        else
                        {
                            //lastMaxFlag = listResult.MaxAsync(a => (a as Table<T, TK>).UpdateFlag);
                            lastMaxFlag = listResult.Max(p => (p as Table<T>).UpdateFlag);
                            lastRowCount = listResult.Count;
                        }
                        var subs = lsql.Split("select", StringSplitOptions.None);
                        var declare = "";
                        var select = "";
                        var columns = "";
                        //最后一个不在括号中的select：右括号数量>=左括号数量
                        for (var i = subs.Length - 1; i > 0; i--)
                        {
                            var sub = subs[i];
                            if (sub.Count(b => b == '(') >= sub.Count(b => b == ')'))
                            {
                                declare = subs.Take(i).Join("select");
                                select = subs.Skip(i).Join("select").Replace("\r\n", " ");
                                break;
                            }
                        }
                        subs = select.Split(" from ");
                        //最后一个不在括号中的from：右括号数量>=左括号数量
                        var condition = "";
                        for (var i = subs.Length - 1; i > 0; i--)
                        {
                            var sub = subs[i];
                            if (sub.Count(b => b == '(') >= sub.Count(b => b == ')'))
                            {
                                columns = subs.Take(i).Join(" from ");
                                condition = subs.Skip(i).Join(" from ");
                                break;
                            }
                        }
                        var flagName = columns.Match(@"([a-z_]{1,30}\.){0,1}updateflag", "updateflag");
                        conn = GetConnection(true);
                        if (columns.Split(",").Any(a => a.Contains(" top ") && !a.Contains("(")))
                        {
                            columns = "{0} {1}".Formats(columns.Match(@"top \d{1,10}", ""), flagName);
                            var sql2 = "{0}select {1} from {2}".Formats(declare, columns, condition);
                            cmd = CreateDbCommand(sql2, conn);
                            if (parameters != null)
                            {
                                cmd.Parameters.AddRange(parameters);
                            }
                            if (conn.State != ConnectionState.Open)
                            {
                                conn.Open();
                            }
                            var da = CreateDbDataAdapter(cmd);
                            var tab = new DataTable();
                            da.Fill(tab);
                            var vt = tab.ToList<TableBase>();
                            var isUpate = vt.Count != lastRowCount || vt.Any(a => a.UpdateFlag.BigerThan(lastMaxFlag));
                            if (!isUpate)
                            {
                                return dt;
                            }
                        }
                        else
                        {
                            if (condition.Contains(" order by "))
                            {
                                condition = condition.Substring(0, condition.LastIndexOf(" order by ", StringComparison.Ordinal));
                            }
                            var andWhere = (condition.Contains(" and ") || condition.Contains(" where ")) ? " and " : " where ";
                            var sql2 = "{0}select count(1) from {1}{2}{3}>{4}".Formats(declare, condition, andWhere, flagName, lastMaxFlag);
                            cmd = CreateDbCommand(sql2, conn);
                            if (parameters != null)
                            {
                                cmd.Parameters.AddRange(parameters);
                            }
                            var result = cmd.ExecuteScalar();
                            var isUpate = result.ConvertTo(1) > 0;
                            if (!isUpate)
                            {
                                return dt;
                            }
                        }
                    }
                }

                if (checkFlag)
                {
                    if (conn == null)
                    {
                        conn = GetConnection(true);
                    }
                    cmd = CreateDbCommand(strSql, conn);
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    var da = CreateDbDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                }
                else
                {
                    var funcTable = new Func<DataTable>(() =>
                    {
                        try
                        {
                            using (var conn2 = GetConnection(true))
                            {
                                cmd = CreateDbCommand(strSql, conn2);
                                if (parameters != null)
                                {
                                    cmd.Parameters.AddRange(parameters);
                                }
                                if (conn2.State != ConnectionState.Open)
                                {
                                    conn2.Open();
                                }
                                var da = CreateDbDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                return dt;
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error("({0})：{1} {2}".Formats(strSql, e.Message, e.StackTrace));
                        }
                        return null;
                    });
                    var hasCache = false;
                    if (tableCache)
                    {
                        dt = Cache.GetCache(cacheKey, funcTable, cacheSeconds);
                        hasCache = dt != null && dt.Rows.Count > 0;
                    }
                    else
                    {
                        var funcList = new Func<List<T>>(() =>
                        {
                            try
                            {
                                dt = funcTable();
                                if (dt == null)
                                {
                                    return null;
                                }
                                listData = dt.ToList<T>();
                                return listData;
                            }
                            catch (Exception e)
                            {
                                Logger.Error("({0})：{1} {2}".Formats(strSql, e.Message, e.StackTrace));
                            }
                            return null;
                        });
                        listData = Cache.GetCache(cacheKey, funcList, cacheSeconds);
                        if (listResult != null && listData != null)
                        {
                            listResult.AddRange(listData);
                            hasCache = true;
                        }
                    }
                    if (hasCache)
                    {
                        return dt;
                    }
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (isListResult)
                    {
                        listData = dt.ToList<T>();
                        listResult.Clear();
                        listResult.AddRange(listData);
                    }
                    if (useCache)
                    {
                        if (checkFlag && !dt.Columns.Cast<DataColumn>().Any(a => a.ColumnName.Equals("updateflag", StringComparison.OrdinalIgnoreCase)))
                        {
                            throw new Exception("保存缓存失败,返回数据集必须包含UpdateFlag字段。");
                        }
                        if (tableCache)
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                Cache.SetCache(cacheKey, dt, cacheSeconds);
                            }
                        }
                        else
                        {
                            if (listData != null && listData.Count > 0)
                            {
                                Cache.SetCache(cacheKey, listData, cacheSeconds);
                            }
                        }
                    }
                }

                return dt;
            }
            catch (Exception e)
            {
                Logger.Error("({0})：{1} {2}".Formats(strSql, e.Message, e.StackTrace));
                return null;
            }
            finally
            {
                Close(conn);
                var useTime = sw.ElapsedMilliseconds;
                var logStr = "({0}) 执行时间 {1} 毫秒".Formats(strSql, useTime.ToString("F0"));
                if (useTime > WornigMilliseconds)
                {
                    Logger.Warn(logStr);
                }
                Logger.Debug(logStr);
            }
        }

        /// <summary>
        /// 根据查询语句从数据库检索数据
        /// </summary>
        /// <param name="strSql">查询语句,参数用?号代替</param>
        /// <param name="cacheSeconds">缓存秒数</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>有数据则返回DataTable数据集，否则返回null</returns>
        public DataTable SelectDataTableCache(string strSql, int cacheSeconds, params object[] parameters)
        {
            return SelectDataTable(strSql, cacheSeconds, true, parameters);
        }

        /// <summary>
        /// 根据查询语句从数据库检索数据
        /// </summary>
        /// <param name="strSql">查询语句,参数用?号代替</param>
        /// <param name="cacheSeconds">缓存秒数</param>
        /// <param name="checkFlag">是否校验UpdateFlag</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>有数据则返回DataTable数据集，否则返回null</returns>
        public DataTable SelectCacheDataTableCache(string strSql, int cacheSeconds, bool checkFlag = true, params object[] parameters)
        {
            return SelectDataTable(strSql, cacheSeconds, checkFlag, parameters);
        }

        

        /// <summary>
        /// 查询表对象
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="defaultValue">返回List类型</param>
        /// <param name="cacheSeconds">缓存秒数</param>
        /// <param name="checkFlag">是否校验UpdateFlag</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>表对象,只绑定选择的字段值</returns>
        public List<T> SelectAsCache<T>(string sql, T defaultValue, int cacheSeconds = 0, bool checkFlag = true, params object[] parameters)
        {
            return SelectCache<T>(sql, cacheSeconds, checkFlag, parameters);
        }

        /// <summary>
        /// 查询表对象
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="cacheSeconds">缓存秒数</param>
        /// <param name="checkFlag">是否校验UpdateFlag</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>表对象,只绑定选择的字段值</returns>
        public List<T> SelectCache<T>(string sql, int cacheSeconds, bool checkFlag = true, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
            {
                return null;
            }
            var result = new List<T>();
            SelectDataTableCache(result, sql, cacheSeconds, checkFlag, parameters);
            return result;
        }

        public List<T> GetDataAllCache<T>(string tableName)
        {
            var strSql = "SELECT * FROM " + tableName;
            return SelectCache<T>(strSql, cacheSecond);
        }

        #endregion

        #region === CUD ===

        public int InsertId<T>(Table<T> table, string condition=null,string idFieldName="Id")
        {
            
            var parameters = new List<object>();
            var strSql = GetInsertIdSql(table,condition,idFieldName, parameters);

            return ExecuteScalar(-1, strSql, parameters.ToArray());
        }


        public bool Insert<T>(Table<T> table, string condition = null)
        {

            var parameters = new List<object>();
            var strSql = GetInsertSql<T>(table, condition, parameters);

            return ExecuteNonQuery(strSql, parameters.ToArray()) > 0;
        }

        public int InsertOrUpdate<T>(Table<T> table, string condition = null)
        {
            
            var parameters = new List<object>();
            var strSql = GetInsertOrUpdateSql<T>(table, condition, parameters);

            return ExecuteNonQuery(strSql, parameters.ToArray());
        }

        public int Update<T>(Table<T> table, string condition = null)
        {

            var parameters = new List<Object>();
            var strSql = GetUpdateSql<T>(table, condition, parameters);

            return ExecuteNonQuery(strSql, parameters.ToArray());
        }

        public int UpdateFieldStep(string tableName, Dictionary<string, object> fields, string condition = null)
        {

            var parameters = new List<Object>();
            var strSql = GetUpdateFieldStepSql(tableName,fields, condition, parameters);

            return ExecuteNonQuery(strSql, parameters.ToArray());
        }
        
        public abstract string GetInsertIdSql<T>(Table<T> table,string condition,string idFieldName, List<object> param);
        public abstract string GetInsertSql<T>(Table<T> table, string condition, List<object> param);

        public abstract string GetInsertOrUpdateSql<T>(Table<T> table, string condition, List<object> param);
        public abstract string GetUpdateSql<T>(Table<T> table, string condition, List<object> parameters);

        public abstract string GetUpdateFieldStepSql(string tableName, Dictionary<string, object> fields, string condition, List<object> parameters);
        public int Delete(string table, string sqlWhere, List<object> parameters = null)
        {
            var strSql = "DELETE FROM " + table + " WHERE " + sqlWhere;
            if (parameters != null)
            {
                return ExecuteNonQuery(strSql, parameters.ToArray());
            }
            else
            {
                return ExecuteNonQuery(strSql, null);
            }
            
        }

        #endregion

        #region === Transaction ===

        public bool ExecuteSqlTrans(IList<string> sqlList)
        {
            var isOk = false;
            if (sqlList == null || sqlList.Count==0)
            {
                return false;
            }
            var sw = Stopwatch.StartNew();
            DbConnection conn = null;
            DbTransaction trans = null;
            object result;
            try
            {
                conn = GetConnection(false);
                
                trans = conn.BeginTransaction();
                foreach (var sql in sqlList)
                {
                    var cmd = CreateDbCommand(sql, conn);
                    cmd.Transaction = trans;
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    result = cmd.ExecuteScalar();
                }
                trans.Commit();
                isOk = true;
            }
            catch (Exception e)
            {
                if (trans != null)
                {
                    try
                    {
                        trans.Rollback();
                        isOk = false;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("({0})事务回滚错误：{1}".Formats(sqlList, ex.Message));
                    }
                }
                Logger.Error("({0})：{1}".Formats(sqlList, e.Message));
            }
            finally
            {
                Close(conn);
                var useTime = sw.ElapsedMilliseconds;
                var logStr = "({0}) 执行时间 {1} 毫秒".Formats(sqlList, useTime.ToString("F0"));
                if (useTime > WornigMilliseconds)
                {
                    Logger.Warn(logStr);
                }
                Logger.Debug(logStr);
            }

            return isOk;
        }

        

        /// <summary>
        /// 复杂事务更新(多表更新保持事务，支持跨数据库事务)
        /// <para>例：更新用户表 + 江苏票表 + 批量sql</para>
        /// <para>Sql.UpdateTrans(() =></para>
        /// <para>{</para>
        /// <para>　　　new List&lt;T_User>().UpdateNoTrans(a => "id=" + a.Id);</para>
        /// <para>　　　new List&lt;TB_LOTTERY_INFO>().UpdateNoTrans(a => "id=" + a.DRAW_FLAG);</para>
        /// <para>　　　Sql3.ExecuteNoTrans("");</para>
        /// <para>});</para>
        /// <para>注：事务内部调用函数不允许有子事务存在，例中均为NoTrans</para>
        /// <para>　　C#事务请参看 Utility.TransactionHelper.ExecuteTrans()</para>
        /// </summary>
        public  bool UpdateTrans(Action action)
        {
            return action.ExecuteTrans();
        }
        #endregion

        #region === db op ===
        public bool TruncateTables(IEnumerable<string> tables)
        {

            var strSql = GetTruncateTablesSql(tables);
            var result = ExecuteNonQueryTrans(strSql,null);
            if (result > 0)
                return true;
            else
                return false;
        }
        public bool DropTables(IEnumerable<string> tables)
        {
            var strSql = GetDropTablesSql(tables);
            var result = ExecuteNonQueryTrans(strSql, null);
            if (result > 0)
                return true;
            else
                return false;
        }


        public bool CreateTables(IEnumerable<DbTableDefine> tableDefines)
        {

            var strSql = GetCreateTablesSql(tableDefines);
            var result = ExecuteNonQueryTrans(strSql, null);
            if (result > 0)
                return true;
            else
                return false;

        }

        public bool CreateTableForeignKeys(IEnumerable<DbForeignKeyDefine> foreignKeyDefines)
        {

            var strSql = GetCreateTableForeignKeysSql(foreignKeyDefines);
            var result = ExecuteNonQueryTrans(strSql, null);
            if (result > 0)
                return true;
            else
                return false;

        }

        public Dictionary<string, Type> GetTableStruct(string tableName,string dbName)
        {
            var dict = new Dictionary<string, Type>();
            
            try
            {
                var strSql = GetTableStructSql(tableName, dbName);

                using (var conn = GetConnection(false))
                {
                    strSql = PrepareParameter(strSql, null);
                    var cmd = CreateDbCommand(strSql, conn);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var name = reader[0].ToString().ToLower();
                        var type = ConvertToCSType(reader[1].ToString());

                        dict.Add(name.ToLower(), type);
                    }

                    return dict;
                }
                
            }
            catch (Exception ex)
            {
                throw new UtilCrashException("取表结构异常");
                //throw new UtilCrashException("取表结构异常" + " strSql=" + sqlString + " error=" + ex.Message + " conn=" + _connectionString, ex);
            }
            return dict;
        }

        

        public abstract string GetTruncateTablesSql(IEnumerable<string> tables);

        public abstract string GetDropTablesSql(IEnumerable<string> tables);

        public abstract string GetCreateTablesSql(IEnumerable<DbTableDefine> tableDefines);

        public abstract string GetCreateTableForeignKeysSql(IEnumerable<DbForeignKeyDefine> foreignKeyDefines);

        public abstract string GetTableStructSql(string tableName, string dbName);

        #endregion

        #region === procedure ===

        /// <summary>
        /// 执行存储过程
        /// 存储过程是以sql语句的形式执行
        /// 一般要求存储过程最后一个参数为output ，且为varchar(N)形式，对应的是out msg
        /// </summary>
        /// <param name="strSql">存储过程对应的sql语句</param>
        /// <param name="retPosition">最后一个output参数的位置</param>
        /// <param name="msg">返回字符中，最后一个参数</param>
        /// <param name="rslt"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ProcedureResult ExecuteProcedure(string strSql, int retPosition, params object[] parameters)
        {
            var result = new ProcedureResult()
            {
                Msg="",
                Rslt=-1,
                Ds = new DataSet(),
            };

            if (strSql.IsNullOrEmpty())
            {
                return null;
            }

            var sw = Stopwatch.StartNew();
            DbConnection conn = null;

            try
            {
                var ds = SelectDataSet(strSql, parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    var dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                    result.Rslt = (int)dr["r"];
                    var colName = "op" + retPosition.ToString();
                    result.Msg = dr[colName].ConvertTo<string>();
                    ds.Tables.RemoveAt(ds.Tables.Count - 1);
                    if (result.Ds != null)
                    {
                        result.Ds.Tables.Clear();
                        result.Ds.Tables.AddRange(ds.Tables.Cast<DataTable>().Select(a => a.Copy()).ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("({0})：{1} {2}".Formats(strSql, e.Message, e.StackTrace));
                return null;
            }
            finally
            {
                Close(conn);
                var useTime = sw.ElapsedMilliseconds;
                var logStr = "({0}) 执行时间 {1} 毫秒".Formats(strSql, useTime.ToString("F0"));
                if (useTime > WornigMilliseconds)
                {
                    Logger.Warn(logStr);
                }
                Logger.Debug(logStr);
                
            }
            return result;
        }

        
        #endregion

        #region === other ===
        /// <summary>
        /// db数据类型转换成c#数据类型
        /// </summary>
        /// <param name="strType"></param>
        /// <returns></returns>
        private Type ConvertToCSType(string strType)
        {
            var type = typeof(string);
            var dbType = strType.ToLower();
            switch (dbType)
            {
                //数值
                case "smallint":
                case "tinyint":
                case "mediumint":
                case "int":
                    type = typeof(int);
                    break;
                case "bigint":
                    type = typeof(long);
                    break;
                case "float":
                    type = typeof(float);
                    break;
                case "double":
                    type = typeof(double);
                    break;
                case "decimal":
                    type = typeof(decimal);
                    break;
                //date
                case "date":
                case "time":
                case "datetime":
                case "timestamp":
                    type = typeof(DateTime);
                    break;
                //byte
                case "tityblob":
                case "blob":
                case "mediumblob":
                case "longblob":
                    type = typeof(byte[]);
                    break;
                //string
                case "char":
                case "varchar":
                case "tinytext":
                case "text":
                case "longtext":
                case "enum":
                case "set":
                    type = typeof(string);
                    break;
            }
            return type;
        }
        #endregion

    }
}
