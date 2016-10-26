using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;

namespace Comm.Tools.Utility.Data
{
    public class DbBase2
    {
        protected static Log Log;
        /// <summary>
        /// 执行时间警告毫秒数
        /// </summary>
        internal const int WornigMilliseconds = 1000;
        /// <summary>
        /// 事务选项：事务级别为ReadCommitted，2分钟事务超时
        /// </summary>
        internal static readonly TransactionOptions TransactionOption = new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
            Timeout = new TimeSpan(0, 2, 0)
        };

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        internal static SqlConnection GetSqlConnection(Type dbType)
        {
            SqlConnection conn = null;
            try
            {
                var sb = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings[dbType.Name])
                {
                    ApplicationIntent = ApplicationIntent.ReadWrite
                };
                conn = new SqlConnection(sb.ConnectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                Close(conn);
                Log.Error(ex.Message + ex.StackTrace);
            }
            return conn;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        internal static void Close(DbConnection conn)
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
                Log.Error(e.Message + e.StackTrace);
            }
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        internal static DbConnection GetConnection(Type dbType)
        {
            DbConnection conn = null;
            try
            {
                switch (dbType.Name)
                {
                    default:
                        {
                            var sb = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings[dbType.Name])
                            {
                                ApplicationIntent = ApplicationIntent.ReadWrite
                            };
                            conn = new SqlConnection(sb.ConnectionString);
                        }
                        break;
                    case "Oracle":
                        {
                            conn = new OleDbConnection(ConfigurationManager.AppSettings[dbType.Name]);
                        }
                        break;
                }
                conn.Open();
            }
            catch (Exception ex)
            {
                Close(conn);
                Log.Error(ex.Message + ex.StackTrace);
            }
            return conn;
        }

        internal static DbParameter CreateDbParameter(Type dbType, string name, object value)
        {
            DbParameter para;
            switch (dbType.Name)
            {
                default:
                    para = new SqlParameter();
                    break;
                case "Oracle":
                    para = new OleDbParameter();
                    break;
            }
            para.ParameterName = name;
            if (value == null)
            {
                para.IsNullable = true;
                para.Value = DBNull.Value;
            }
            else
            {
                para.Value = value;
                //为了计划重用,必须设置Size,否则默认为参数实际value长度
                if (value is string)
                {
                    //推导至固定长度, 50 200 1000 max(-1)
                    var length = value.ToString().Length;
                    int size;
                    if (length < 50)
                    {
                        size = 50;
                    }
                    else if (length < 200)
                    {
                        size = 200;
                    }
                    else if (length < 1000)
                    {
                        size = 1000;
                    }
                    else
                    {
                        size = -1;
                    }
                    para.Size = size;
                }
            }
            return para;
        }
    }

    /// <summary>
    /// 数据库
    /// </summary>
    public class DbBase2<TK> : DbBase2
    {
        /// <summary>
        /// 是否开启
        /// </summary>
        public static readonly bool IsOn;
        
        /// <summary>
        /// 是否开启读写分离
        /// </summary>
        protected static readonly bool EnableReadOnly;
        /// <summary>
        /// 数据库名称
        /// </summary>
        public static readonly string DbName;
        /// <summary>
        /// 数据库类型
        /// </summary>
        internal static readonly string DbType;
		private static readonly string ConnectionString;
		
        static DbBase2()
        {
            var connName = typeof(TK).Name;
            DbType = connName.ReplaceRegex(@"\d", "");
            ConnectionString = ConfigurationManager.AppSettings[connName];
            IsOn = ConnectionString.HasValue();
            if (!IsOn)
            {
                return;
            }

            switch (DbType)
            {
                default:
                    {
                        var sb = new SqlConnectionStringBuilder(ConnectionString);
                        ConnectionString = sb.ConnectionString;
                        IsOn = ConnectionString.HasValue();
                        if (!IsOn)
                        {
                            return;
                        }
                        DbName = sb.InitialCatalog;
                        EnableReadOnly = ConnectionString.Contains("ApplicationIntent");
                        Log = new Log("SqlServer");
                    }
                    break;
                case "Oracle":
                    {
                        var sb = new OleDbConnectionStringBuilder(ConnectionString);
                        ConnectionString = sb.ConnectionString;
                        IsOn = ConnectionString.HasValue();
                        if (!IsOn)
                        {
                            return;
                        }
                        DbName = sb["User ID"].ToString();
                        Log = new Log("Oracle");
                    }
                    break;
            }
        }

        internal static DbParameter CreateDbParameter(string name, object value)
        {
            if (!IsOn)
            {
                return null;
            }
            DbParameter para;
            switch (DbType)
            {
                default:
                    para = new SqlParameter();
                    break;
                case "Oracle":
                    para = new OleDbParameter();
                    break;
            }
            para.ParameterName = name;
            if (value == null)
            {
                para.IsNullable = true;
                para.Value = DBNull.Value;
            }
            else
            {
                para.Value = value;
                //为了计划重用,必须设置Size,否则默认为参数实际value长度
                if (value is string)
                {
                    //推导至固定长度, 50 200 1000 max(-1)
                    var length = value.ToString().Length;
                    int size;
                    if (length < 50)
                    {
                        size = 50;
                    }
                    else if (length < 200)
                    {
                        size = 200;
                    }
                    else if (length < 1000)
                    {
                        size = 1000;
                    }
                    else
                    {
                        size = -1;
                    }
                    para.Size = size;
                }
            }
            return para;
        }

        internal static DbCommand CreateDbCommand(string sql, DbConnection conn)
        {
            if (!IsOn)
            {
                return null;
            }
            DbCommand cmd;
            switch (DbType)
            {
                default:
                    cmd = new SqlCommand(sql);
                    break;
                case "Oracle":
                    cmd = new OleDbCommand(sql);
                    break;
            }
            cmd.Connection = conn;
            return cmd;
        }

        internal static DbDataAdapter CreateDbDataAdapter(DbCommand cmd)
        {
            if (!IsOn)
            {
                return null;
            }
            DbDataAdapter da;
            switch (DbType)
            {
                default:
                    da = new SqlDataAdapter();
                    break;
                case "Oracle":
                    da = new OleDbDataAdapter();
                    break;
            }
            da.SelectCommand = cmd;
            return da;
        }

        /// <summary>
        /// 根据查询语句从数据库检索数据
        /// </summary>
        /// <param name="listResult"></param>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="cacheSeconds">缓存秒数</param>
        /// <param name="checkFlag">是否校验UpdateFlag</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>有数据则返回DataTable数据集，否则返回null</returns>
        private static DataTable SelectDataTable<T>(List<T> listResult, string sql, int cacheSeconds, bool checkFlag, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
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
                    cacheKey = (sql + parameters.Join(",")).Md5();
                }
                var prepareParameters = new List<object>();
                sql = PrepareParameter(sql, parameters, prepareParameters);
                DataTable dt = null;
                DbCommand cmd;
                List<T> listData = null;
                var isListResult = listResult != null;
                var tableCache = useCache && !isListResult;

                if (useCache)
                {
                    var lsql = sql.Trim().ToLower();
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
                        int lastRowCount;
                        if (tableCache)
                        {
                            lastMaxFlag = dt.Select().Max(a => (a["updateflag"] as byte[]).TimestampToString());
                            lastRowCount = dt.Rows.Count;
                        }
                        else
                        {
                            lastMaxFlag = listResult.Max(a => (a as Table2<T, TK>).UpdateFlag);
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
                            cmd.Parameters.AddRange(prepareParameters.ToArray());
                            var da = CreateDbDataAdapter(cmd);
                            var tab = new DataTable();
                            da.Fill(tab);
                            var vt = tab.ToList<TableBase2>();
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
                            cmd.Parameters.AddRange(prepareParameters.ToArray());
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
                        conn = GetSqlConnection(true);
                    }
                    cmd = CreateDbCommand(sql, conn);
                    cmd.Parameters.AddRange(prepareParameters.ToArray());
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
                                cmd = CreateDbCommand(sql, conn2);
                                cmd.Parameters.AddRange(prepareParameters.ToArray());
                                var da = CreateDbDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                return dt;
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error("({0})：{1} {2}".Formats(sql, e.Message, e.StackTrace));
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
                                Log.Error("({0})：{1} {2}".Formats(sql, e.Message, e.StackTrace));
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
                Log.Error("({0})：{1} {2}".Formats(sql, e.Message, e.StackTrace));
                return null;
            }
            finally
            {
                Close(conn);
                var logStr = "({0}) {1}".Formats(sql, sw.Elapsed.ToUseSimpleTime());
                if (sw.ElapsedMilliseconds > WornigMilliseconds)
                {
                    Log.Warn(logStr);
                }
                Log.Debug(logStr);
            }
        }

        /// <summary>
        /// 根据查询语句从数据库检索数据
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>有数据则返回DataTable数据集，否则返回null</returns>
        public static DataTable SelectDataTable(string sql, params object[] parameters)
        {
            return SelectDataTable<object>(null, sql, 0, true, parameters);
        }

        /// <summary>
        /// 根据查询语句从数据库检索数据
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="cacheSeconds">缓存秒数</param>
        /// <param name="checkFlag">是否校验UpdateFlag</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>有数据则返回DataTable数据集，否则返回null</returns>
        public static DataTable SelectCacheDataTable(string sql, int cacheSeconds, bool checkFlag = true, params object[] parameters)
        {
            return SelectDataTable<object>(null, sql, cacheSeconds, checkFlag, parameters);
        }

        /// <summary>
        /// 查询表对象
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>表对象,只绑定选择的字段值</returns>
        public static List<T> Select<T>(string sql, params object[] parameters)
        {
            return SelectCache<T>(sql, 0, true, parameters);
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
        public static List<T> SelectAs<T>(string sql, T defaultValue, int cacheSeconds = 0, bool checkFlag = true, params object[] parameters)
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
        public static List<T> SelectCache<T>(string sql, int cacheSeconds, bool checkFlag = true, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
            {
                return null;
            }
            var result = new List<T>();
            SelectDataTable(result, sql, cacheSeconds, checkFlag, parameters);
            return result;
        }

        /// <summary>
        /// 根据查询语句从数据库检索数据
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        public static DataSet SelectDataSet(string sql, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
            {
                return null;
            }

            var sw = Stopwatch.StartNew();
            DbConnection conn = null;

            try
            {
                var prepareParameters = new List<object>();
                sql = PrepareParameter(sql, parameters, prepareParameters);
                conn = GetConnection(true);
                var cmd = CreateDbCommand(sql, conn);
                cmd.Parameters.AddRange(prepareParameters.ToArray());
                var da = CreateDbDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds);

                cmd.Parameters.Clear();

                return ds;
            }
            catch (SystemException e)
            {
                Log.Error("({0})：{1}".Formats(sql, e.Message));

                return null;
            }
            finally
            {
                Close(conn);
                var logStr = "({0}) {1}".Formats(sql, sw.Elapsed.ToUseSimpleTime());
                if (sw.ElapsedMilliseconds > WornigMilliseconds)
                {
                    Log.Warn(logStr);
                }
                Log.Debug(logStr);
            }
        }
        /// <summary>
        /// 根据查询语句从数据库检索数据(含事务)
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        public static DataSet SelectDataSetTransaction(string sql, params object[] parameters)
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
                var prepareParameters = new List<object>();
                sql = PrepareParameter(sql, parameters, prepareParameters);
                conn = GetConnection(false);
                trans = conn.BeginTransaction();
                var cmd = CreateDbCommand(sql, conn);
                cmd.Parameters.AddRange(prepareParameters.ToArray());
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
                        Log.Error("({0})事务回滚错误：{1}".Formats(sql, ex.Message));
                    }
                }
                Log.Error("({0})：{1}".Formats(sql, e.Message));
                throw new Exception(e.Message.Split("\n")[0]);
            }
            finally
            {
                Close(conn);
                var logStr = "({0}) {1}".Formats(sql, sw.Elapsed.ToUseSimpleTime());
                if (sw.ElapsedMilliseconds > WornigMilliseconds)
                {
                    Log.Warn(logStr);
                }
                Log.Debug(logStr);
            }
        }

        /// <summary>
        /// 分页
        /// sp_cursoropen(打开游标) cursor OUTPUT(游标句柄),stmt(sql语句),scrollopt(滚动选项：1-KEYSET),ccopt(并发控制选项：1-READ_ONLY),rowcount OUTPUT(缓冲区行数)
        /// sp_cursorfetch(提取由一行或多行) cursor(游标句柄),fetchtype(提取的游标缓冲区：16-ABSOLUTE),rownum,nrows
        /// </summary>
        [Obsolete("已过时,请用 PagedResult<T> SelectPage<T> 替换")]
        public static List<T> SelectPage<T>(string sql, int pageSize, ref int pageIndex, out int pageCount, out int rowCount)
        {
            var dt = SelectPage(sql, pageSize, ref pageIndex, out pageCount, out rowCount);
            if (dt == null)
            {
                return null;
            }
            var result = dt.ToList<T>();
            return result;
        }
        [Obsolete("已过时,请用 PagedResult<T> SelectPage<T> 替换")]
        public static List<T> SelectPageAs<T>(string sql, T defaultValue, int pageSize, ref int pageIndex, out int pageCount, out int rowCount)
        {
            return SelectPage<T>(sql, pageSize, ref pageIndex, out pageCount, out rowCount);
        }
        [Obsolete("已过时,请用 PagedResult<T> SelectPage<T> 替换")]
        public static void SelectPage<T>(string sql, PagedResult<T> pagedResult)
        {
            var pagedDataTableResult = new PagedDataTableResult(pagedResult.PageSize, pagedResult.PageIndex);
            SelectPage(sql, pagedDataTableResult);
            if (pagedDataTableResult.Rows == null)
            {
                return;
            }
            pagedDataTableResult.ListResult(pagedResult);
        }
        [Obsolete("已过时,请用 PagedDataTableResult SelectPage 替换")]
        public static void SelectPage(string sql, PagedDataTableResult pagedResult)
        {
            SelectPage(sql, "", pagedResult);
        }
        [Obsolete("已过时,请用 PagedDataTableResult SelectPage 替换")]
        public static void SelectPage(string sql, string stats, PagedDataTableResult pagedResult)
        {
            var pageIndex = pagedResult.PageIndex;
            int pageCount;
            int rowCount;
            var ds = SelectPage(sql, stats, pagedResult.PageSize, ref pageIndex, out pageCount, out rowCount);
            if (ds == null)
            {
                return;
            }
            pagedResult.PageIndex = pageIndex;
            pagedResult.PageCount = pageCount;
            pagedResult.RowCount = rowCount;
            pagedResult.Rows = ds.Tables[0];
            if (stats.HasValue())
            {
                pagedResult.Stat = ds.Tables[1].Rows[0];
            }
        }
        [Obsolete("已过时,请用 PagedDataTableResult SelectPage 替换")]
        public static DataTable SelectPage(string sql, int pageSize, ref int pageIndex, out int pageCount, out int rowCount)
        {
            var ds = SelectPage(sql, "", pageSize, ref pageIndex, out pageCount, out rowCount);
            if (ds != null && ds.Tables.Count == 1)
            {
                var dt = ds.Tables[0];
                return dt;
            }
            return null;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="qrySql">查询的Sql：例 SELECT * FROM T_User</param>
        /// <param name="statSql">统计的Sql：例 SELECT SUM(balance)SumBalance,SUM(freeze)SumFreeze FROM T_User</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        private static DataSet SelectPage(string qrySql, string statSql, int pageSize, ref int pageIndex, out int pageCount, out int rowCount)
        {
            var stmt = @"DECLARE @Sql NVARCHAR(MAX)
DECLARE @PageIndex INT
DECLARE @PageSize INT
DECLARE @PageCount INT
DECLARE @RowCount INT
DECLARE @RowIndex INT
SET @Sql=?
SET @PageSize={0}
SET @PageIndex={1}
DECLARE @Cursor INT
EXEC sp_cursoropen @Cursor OUTPUT,@Sql,1,1,@RowCount OUTPUT
SET @PageCount=(@RowCount+@PageSize-1)/@PageSize
IF @PageIndex<1
BEGIN
	SET @PageIndex=1
END
SET @RowIndex=(@PageIndex-1)*@PageSize+1
EXEC sp_cursorfetch @Cursor,16,@RowIndex,@PageSize
EXEC sp_cursorclose @Cursor
SELECT @PageIndex PageIndex,@PageCount [PageCount],@RowCount [RowCount]".Formats(pageSize, pageIndex);

            DataSet ds;
            if (statSql.HasValue())
            {
                stmt += "\r\nEXEC (?)";
                ds = SelectDataSet(stmt, qrySql, statSql);
            }
            else
            {
                ds = SelectDataSet(stmt, qrySql);
            }
            if (ds != null && ds.Tables.Count >= 3)
            {
                var dr = ds.Tables[2].Rows[0];
                pageIndex = dr["PageIndex"].ConvertTo(0);
                pageCount = dr["PageCount"].ConvertTo(0);
                rowCount = dr["RowCount"].ConvertTo(0);
                ds.Tables.RemoveAt(2);
                ds.Tables.RemoveAt(0);
                return ds;
            }
            pageIndex = 0;
            pageCount = 0;
            rowCount = 0;
            return null;
        }

        /// <summary>
        /// 分页：支持参数化{0},{1}...类似Format
        /// </summary>
        public static PagedResult<T> SelectPage<T>(string qrySql, int pageSize, int pageIndex, params object[] parameters)
        {
            return SelectPage<T>(qrySql, "", pageSize, pageIndex, parameters);
        }
        /// <summary>
        /// 分页：支持参数化{0},{1}...类似Format
        /// </summary>
        public static PagedResult<T> SelectPage<T>(string qrySql, string statSql, int pageSize, int pageIndex, params object[] parameters)
        {
            var paged = SelectPage(qrySql, statSql, pageSize, pageIndex, parameters);
            return paged == null ? null : paged.Cast<T>();
        }

        /// <summary>
        /// 分页：支持参数化{0},{1}...类似Format
        /// </summary>
        public static PagedDataTableResult SelectPage(string qrySql, int pageSize, int pageIndex, params object[] parameters)
        {
            return SelectPage(qrySql, "", pageSize, pageIndex, parameters);
        }
        /// <summary>
        /// 分页：支持参数化{0},{1}...类似Format
        /// </summary>
        public static PagedDataTableResult SelectPage(string qrySql, string statSql, int pageSize, int pageIndex, params object[] parameters)
        {
            var index = pageIndex;
            var ds = SelectPaged(qrySql, statSql, pageSize, ref index, parameters);
            if (ds == null || ds.Tables.Count==0)
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

        private static DataSet SelectPaged(string qrySql, string statSql, int pageSize, ref int pageIndex, params object[] parameters)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            //分析Sql
            qrySql = qrySql.Split("\r\n").Select(a => a.Trim()).Join(" ").Replace("  ", " ").Replace("( ", "(").Replace(" )", ")");
            var declare = "";
            var with = "";
            //第一个不在括号中的Select
            var select = qrySql;
            var selectIndexs = qrySql.RegexIndexOfs(@"select[\s*\[\(]", RegexOptions.IgnoreCase);
            foreach (var index in selectIndexs)
            {
                var count = qrySql.Substring(0, index).Count(a => a.In('(', ')'));
                if (count%2 == 0)
                {
                    select = qrySql.Substring(index).Trim();
                    declare = qrySql.Substring(0, index).Trim();
                    with = declare.Match(@"\s*with\s+[\w\d]+\s+as\s*\(.+\)", RegexOptions.IgnoreCase);
                    if (with.HasValue())
                    {
                        declare = declare.Replace(with, "");
                    }
                    break;
                }
            }
            var ifs = select.RegexIndexOfs(@"[\s*\d\]\)]{1}FROM[\s\[\(]{1}", RegexOptions.IgnoreCase);
            var fi = ifs.FirstOrDefault(i => select.Substring(0, i + 1).Count(a => a == '(') == select.Substring(0, i + 1).Count(a => a == ')')) + 5;
            var oi = select.RegexIndexOfs(@"[\s\]\d\)]{1}ORDER ", RegexOptions.IgnoreCase).FirstOrDefault(i => select.Substring(0, i + 1).Count(a => a == '(') == select.Substring(0, i + 1).Count(a => a == ')'));
            if (oi == 0)
            {
                throw new Exception("Sql 语句必须含有排序");
            }
            var from = select.Substring(fi, oi + 1 - fi).Trim();
            //最大参数索引
            var indexs = qrySql.Matches(@"{\d+}");
            var maxPindex = -1;
            if (indexs.Length > 0)
            {
                maxPindex = indexs.Max(a => a.Match(@"\d+").ToInt32());
            }
            var sb = new StringBuilder();
            sb.AppendLine("DECLARE @PageSize INT")
                .AppendLine("DECLARE @PageIndex INT")
                .Append("SET @PageSize={").Append(maxPindex + 1).AppendLine("}")
                .Append("SET @PageIndex={").Append(maxPindex + 2).AppendLine("};");
            if (declare.HasValue())
            {
                sb.AppendLine(declare);
            }
            if (with.HasValue())
            {
                sb.AppendLine(with);
            }
            sb.Append("SELECT COUNT(1) FROM ").Append(from).AppendLine(";");
            if (with.HasValue())
            {
                sb.AppendLine(with);
            }
            sb.AppendLine(select);
            sb.AppendLine("OFFSET (@PageIndex-1)*@PageSize ROWS")
                .AppendLine("FETCH NEXT @PageSize ROWS ONLY");
            if (statSql.HasValue())
            {
                sb.AppendLine(statSql);
            }
            var sql = sb.ToString();
            var ps = parameters.ToList();
            ps.Add(pageSize);
            ps.Add(pageIndex);
            var ds = SelectDataSet(sql, ps.ToArray());
            return ds;
        }

        /// <summary>
        /// 添加、更新、删除
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
            {
                return -1;
            }
            var sw = Stopwatch.StartNew();
            if (sql.Trim().Length == 0)
            {
                return 0;
            }

            DbConnection conn = null;
            DbTransaction trans = null;

            try
            {
                var prepareParameters = new List<object>();
                sql = PrepareParameter(sql, parameters, prepareParameters);
                conn = GetConnection(false);
                trans = conn.BeginTransaction();
                var cmd = CreateDbCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddRange(prepareParameters.ToArray());
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
                        Log.Error("({0})事务回滚错误：{1}".Formats(sql, ex.Message));
                    }
                }
                Log.Error("({0})：{1}".Formats(sql, e.Message));

                return -1;
            }
            finally
            {
                Close(conn);
                var logStr = "({0}) {1}".Formats(sql, sw.Elapsed.ToUseSimpleTime());
                if (sw.ElapsedMilliseconds > WornigMilliseconds)
                {
                    Log.Warn(logStr);
                }
                Log.Debug(logStr);
            }
        }

        /// <summary>
        /// 添加、更新、删除(无事务)
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQueryNoTrans(string sql, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
            {
                return -1;
            }
            if (sql.Trim().Length == 0)
            {
                return 0;
            }
            var prepareParameters = new List<object>();
            sql = PrepareParameter(sql, parameters, prepareParameters);
            using (var conn = GetConnection(false))
            {
                var cmd = CreateDbCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddRange(prepareParameters.ToArray());
                var result = cmd.ExecuteNonQuery();
                return result;
            }
        }
		
		/// <summary>
        /// 执行更新并查询（无事物），并返回查询多返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>结果集中第一行的第一列</returns>
        public static T ExecuteScalarNoTrans<T>(T defaultValue, string sql, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
            {
                return defaultValue;
            }
            if (sql.Trim().Length == 0)
            {
                return defaultValue;
            }
            var prepareParameters = new List<object>();
            sql = PrepareParameter(sql, parameters, prepareParameters);
            using (var conn = GetConnection(false))
            {
                var cmd = CreateDbCommand(sql, conn);
                cmd.Parameters.AddRange(prepareParameters.ToArray());
                var result = cmd.ExecuteScalar();
                return result.ConvertTo(defaultValue);
            }
        }

        /// <summary>
        /// 执行更新并查询（带事物回滚），并返回查询多返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="sql">查询语句,参数用?号代替</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <returns>结果集中第一行的第一列</returns>
        public static T ExecuteScalarTrans<T>(T defaultValue, string sql, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
            {
                return defaultValue;
            }
            var sw = Stopwatch.StartNew();
            DbConnection conn = null;
            DbTransaction trans = null;
            object result;
            try
            {
                var prepareParameters = new List<object>();
                sql = PrepareParameter(sql, parameters, prepareParameters);
                conn = GetConnection(false);
                trans = conn.BeginTransaction();
                var cmd = CreateDbCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.Parameters.AddRange(prepareParameters.ToArray());
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
                        Log.Error("({0})事务回滚错误：{1}".Formats(sql, ex.Message));
                    }
                }
                Log.Error("({0})：{1}".Formats(sql, e.Message));

                return defaultValue;
            }
            finally
            {
                Close(conn);
                var logStr = "({0}) {1}".Formats(sql, sw.Elapsed.ToUseSimpleTime());
                if (sw.ElapsedMilliseconds > WornigMilliseconds)
                {
                    Log.Warn(logStr);
                }
                Log.Debug(logStr);
            }

            return result.ConvertTo(defaultValue);
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="isReadOnly">是否只读</param>
        internal static DbConnection GetConnection(bool isReadOnly)
        {
            if (!IsOn)
            {
                return null;
            }
            DbConnection conn = null;
            try
            {
                switch (DbType)
                {
                    default:
                        {
                            var sb = new SqlConnectionStringBuilder(ConnectionString)
                            {
                                ApplicationIntent = EnableReadOnly && isReadOnly ? ApplicationIntent.ReadOnly : ApplicationIntent.ReadWrite
                            };
                            conn = new SqlConnection(sb.ConnectionString);
                        }
                        break;
                    case "Oracle":
                        {
                            conn = new OleDbConnection(ConnectionString);
                        }
                        break;
                }
                conn.Open();
            }
            catch (Exception ex)
            {
                Close(conn);
                Log.Error(ex.Message + ex.StackTrace);
            }
            return conn;
        }
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="isReadOnly">是否只读</param>
        internal static SqlConnection GetSqlConnection(bool isReadOnly)
        {
            if (!IsOn)
            {
                return null;
            }
            SqlConnection conn = null;
            try
            {
                var sb = new SqlConnectionStringBuilder(ConnectionString)
                {
                    ApplicationIntent = isReadOnly ? ApplicationIntent.ReadOnly : ApplicationIntent.ReadWrite
                };
                conn = new SqlConnection(sb.ConnectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                Close(conn);
                Log.Error(ex.Message + ex.StackTrace);
            }
            return conn;
        }

        internal static string PrepareParameter(string sql, object[] parameters, List<object> prepareParameters)
        {
            if (prepareParameters == null)
            {
                throw new ArgumentNullException("prepareParameters");
            }
            return PrepareParameter(sql, parameters, 0, prepareParameters);
        }

        internal static string PrepareParameter(string sql, object[] parameters, int paraStartIndex, List<object> prepareParameters)
        {
            if (prepareParameters == null)
            {
                throw new ArgumentNullException("prepareParameters");
            }
            if (sql.Contains("?"))
            {
                var ss = sql.Split('?');
                if (ss.Length - 1 == parameters.Length)
                {
                    var sb = new StringBuilder();
                    for (var i = 0; i < ss.Length - 1; i++)
                    {
                        sb.Append(ss[i]).Append("@p").Append((i + paraStartIndex).ToString());
                        prepareParameters.Add(CreateDbParameter("@p" + (i + paraStartIndex), parameters[i]));
                    }
                    sb.Append(ss[ss.Length - 1]);
                    sql = sb.ToString();
                }
                else
                {
                    throw new Exception("Sql.PrepareParameter sql({0}) 参数数量不对".Formats(sql));
                }
            }
            else
            {
                var pcount = sql.Matches(@"{\d+}").Distinct().Count();
                if (pcount > 0)
                {
                    if (pcount == parameters.Length)
                    {
                        //IN({0})处理
                        var ins = sql.Matches(@"IN\s*\(\s*{\d+}\s*\)", RegexOptions.IgnoreCase).Select(a => a.Match(@"\d+").ToInt32()).Distinct().ToArray();
                        var index = 0;
                        for (var i = 0; i < pcount; i++)
                        {
                            if (ins.Contains(i))
                            {
                                var pnames = new List<string>();
                                var invs = parameters[i].ToString().Split(",");
                                foreach (var v in invs)
                                {
                                    pnames.Add("@p" + index);
                                    prepareParameters.Add(CreateDbParameter("@p" + index, v));
                                    index++;
                                }
                                sql = sql.Replace("{" + i + "}", pnames.Join(","));
                            }
                            else
                            {
                                sql = sql.Replace("{" + i + "}", "@p" + index);
                                prepareParameters.Add(CreateDbParameter("@p" + index, parameters[i]));
                                index++;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Sql.PrepareParameter sql({0}) 参数数量不对".Formats(sql));
                    }
                }
            }

            if (parameters.Length > 0 && prepareParameters.Count == 0)
            {
                prepareParameters.AddRange(parameters);
            }

            return sql;
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
        public static bool UpdateTrans(Action action)
        {
            return action.ExecuteTrans();
        }
    }
}