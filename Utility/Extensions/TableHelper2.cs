using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Diagnostics;
using Comm.Tools.Utility.Data;


namespace Comm.Tools.Utility
{
    public static class TableHelper2
    {
        private static readonly Log Log = new Log("SqlServer");
        private static readonly ConcurrentDictionary<string, string> Cloumns = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 批量添加记录
        /// </summary>
        public static int Insert<T, TK>(this IEnumerable<Table2<T, TK>> rows)
        {
            if (rows == null)
            {
                return -1;
            }
            return rows.DeleteInsert("");
        }

        /// <summary>
        /// 批量添加记录
        /// </summary>
        public static int InsertNoTrans<T, TK>(this IEnumerable<Table2<T, TK>> rows)
        {
            if (rows == null)
            {
                return -1;
            }
            return rows.DeleteInsertNoTrans("");
        }

        /// <summary>
        /// 删除后重新添加
        /// </summary>
        public static int DeleteInsert<T, TK>(this IEnumerable<Table2<T, TK>> rows, string deleteCondition)
        {
            try
            {
                if (rows == null)
                {
                    return -1;
                }
                var arrRows = rows.ToArray();
                if (arrRows.Length == 0)
                {
                    return 0;
                }
                var sw = Stopwatch.StartNew();
                var option = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                    Timeout = new TimeSpan(0, 2, 0)
                };
                using (var trans = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    using (var conn = DbBase2<TK>.GetSqlConnection(false))
                    {
                        using (var bulkCopy = new SqlBulkCopy(conn))
                        {
                            var tableName = arrRows[0].TableName;
                            var logStr = new StringBuilder();
                            try
                            {
                                var cmd = Sql2.CreateDbCommand("SELECT TOP 0 * FROM " + tableName, conn);
                                var sda = Sql2.CreateDbDataAdapter(cmd);
                                var dtSource = new DataTable();
                                sda.Fill(dtSource);
                                var dt = arrRows.Select(a => a.__EditColumns).ToDataTable(dtSource);

                                if (deleteCondition.HasValue())
                                {
                                    var sql = "DELETE FROM {0} WHERE {1}".Formats(tableName, deleteCondition);
                                    cmd = Sql2.CreateDbCommand(sql, conn);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();
                                    logStr.Append(sql).Append(";");
                                }

                                bulkCopy.DestinationTableName = tableName;
                                bulkCopy.BatchSize = dt.Rows.Count;
                                bulkCopy.WriteToServer(dt);
                                trans.Complete();
                                return arrRows.Length;
                            }
                            catch (Exception e)
                            {
                                Log.Error(e.Message);
                                return -1;
                            }
                            finally
                            {
                                DbBase2.Close(conn);
                                bulkCopy.Close();
                                sw.Stop();
                                var useTime = sw.ElapsedMilliseconds;
                                logStr.AppendFormat("{0}.Insert({1}) 执行时间 {2} 毫秒", tableName, arrRows.Length, useTime.ToString("F0"));
                                if (useTime > 5000)
                                {
                                    Log.Warn(logStr.ToString());
                                }
                                Log.Debug(logStr.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 删除后重新添加
        /// </summary>
        public static int DeleteInsertNoTrans<T, TK>(this IEnumerable<Table2<T, TK>> rows, string deleteCondition)
        {
            if (rows == null)
            {
                return -1;
            }
            var arrRows = rows.ToArray();
            if (arrRows.Length == 0)
            {
                return 0;
            }

            var tableName = arrRows[0].TableName;
            using (var conn = DbBase2<TK>.GetSqlConnection(false))
            {
                var cmd = Sql2.CreateDbCommand("SELECT TOP 0 * FROM " + tableName, conn);
                var sda = Sql2.CreateDbDataAdapter(cmd);
                var dtSource = new DataTable();
                sda.Fill(dtSource);
                var dt = arrRows.Select(a => a.__EditColumns).ToDataTable(dtSource);

                if (deleteCondition.HasValue())
                {
                    var sql = "DELETE FROM {0} WHERE {1}".Formats(tableName, deleteCondition);
                    cmd = Sql2.CreateDbCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                using (var bulkCopy = new SqlBulkCopy(conn))
                {
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BatchSize = dt.Rows.Count;
                    bulkCopy.WriteToServer(dt);
                    return arrRows.Length;
                }
            }
        }

        public static int InsertNoTrans(this IEnumerable<TableBase2> rows, string tableName)
        {
            if (rows == null)
            {
                return -1;
            }
            var arrRows = rows.ToArray();
            if (arrRows.Length == 0)
            {
                return 0;
            }
            var dbType = arrRows.First().DbType;
            using (var conn = DbBase2.GetSqlConnection(dbType))
            {
                var cmd = Sql2.CreateDbCommand("SELECT TOP 0 * FROM " + tableName, conn);
                var sda = Sql2.CreateDbDataAdapter(cmd);
                var dtSource = new DataTable();
                sda.Fill(dtSource);
                var dt = arrRows.Select(a => a.__EditColumns).ToDataTable(dtSource);

                using (var bulkCopy = new SqlBulkCopy(conn))
                {
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BatchSize = dt.Rows.Count;
                    bulkCopy.WriteToServer(dt);
                    return arrRows.Length;
                }
            }
        }

        /// <summary>
        /// 批量更新记录
        /// </summary>
        public static int Update<T, TK>(this IEnumerable<Table2<T, TK>> tables, Func<T, string> condition)
        {
            if (tables == null)
            {
                return -1;
            }
            var updateTabs = tables.Where(a => a.__EditColumns.Count > 0).ToArray();
            if (updateTabs.Length == 0)
            {
                return 0;
            }

            var sw = Stopwatch.StartNew();
            var result = 0;
            var sqls = new List<KeyValuePair<string, List<DbParameter>>>();
            try
            {
                var sb = new StringBuilder();
                var paraIndex = 0;
                var parameters = new List<DbParameter>();
                foreach (var table in updateTabs)
                {
                    if (table.__EditColumns.Count > 0)
                    {
                        sb.Append("\nupdate ").Append(table.TableName).Append(" set ");
                        foreach (var column in table.__EditColumns)
                        {
                            sb.Append("[").Append(column.Key).Append("]=@p").Append(parameters.Count.ToString()).Append(",");
                            parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + parameters.Count, column.Value));
                            paraIndex += 2;
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(" where ").Append(condition((T)(object)table));

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

                using (var trans = new TransactionScope(TransactionScopeOption.Required, DbBase2.TransactionOption))
                {
                    using (var conn = DbBase2<TK>.GetConnection(false))
                    {
                        foreach (var sql in sqls)
                        {
                            var cmd = Sql2.CreateDbCommand(sql.Key, conn);
                            cmd.CommandType = CommandType.Text;
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
            if (useTime / 1000 > 5)
            {
                Log.Warn(logStr);
            }

            Log.Debug(logStr);

            return result;
        }

        /// <summary>
        /// 更新记录(注意条件，否则批量更新导致无法挽回的错误)
        /// </summary>
        public static int UpdateSetNoTrans<T, TK>(this Table2<T, TK> table, string condition, SqlConnection conn, string set = "")
        {
            if (table == null)
            {
                return -1;
            }
            if (table.__EditColumns.Count == 0)
            {
                throw new Exception("更新字段数至少1个以上");
            }
            var sql = new StringBuilder();
            var parameters = new List<DbParameter>();

            sql.Append("update ").Append(table.TableName).Append(" set ");
            foreach (var column in table.__EditColumns)
            {
                sql.Append("[").Append(column.Key).Append("]=@p").Append(parameters.Count.ToString()).Append(",");
                parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + parameters.Count, column.Value));
            }
            if (set.HasValue())
            {
                sql.Append(set);
            }
            else
            {
                sql.Remove(sql.Length - 1, 1);
            }
            sql.Append(" where ").Append(condition);

            var cmd = Sql2.CreateDbCommand(sql.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddRange(parameters.ToArray());
            var result = cmd.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 根据主键批量更新记录(无事务)
        /// </summary>
        public static int UpdateNoTrans<T, TK>(this IEnumerable<Table2<T, TK>> tables)
        {
            if (tables == null)
            {
                return -1;
            }
            var updateTabs = tables.Where(a => a.__EditColumns.Count > 0).ToArray();
            if (updateTabs.Length == 0)
            {
                return 0;
            }

            var attr = updateTabs.First().GetTableAttribute();
            var type = updateTabs.First().GetType();

            var sqls = new List<KeyValuePair<string, List<DbParameter>>>();
            var sb = new StringBuilder();
            var paraIndex = 0;
            var parameters = new List<DbParameter>();

            foreach (var table in updateTabs)
            {
                if (table.__EditColumns.Count > 0)
                {
                    sb.Append("\nupdate ").Append(table.TableName).Append(" set ");
                    foreach (var column in table.__EditColumns)
                    {
                        sb.Append("[").Append(column.Key).Append("]=@p").Append(parameters.Count.ToString()).Append(",");
                        parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + parameters.Count, column.Value));
                        paraIndex += 2;
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(" where ");
                    foreach (var pk in attr.PrimaryKeys)
                    {
                        sb.Append("[").Append(pk).Append("]=@p").Append(parameters.Count.ToString()).Append(" AND ");
                        parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + parameters.Count, type.GetProperty(pk).GetValue(table)));
                        paraIndex += 2;
                    }
                    sb.Remove(sb.Length - 5, 5);
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

            using (var conn = DbBase2<TK>.GetConnection(false))
            {
                foreach (var sql in sqls)
                {
                    var cmd = Sql2.CreateDbCommand(sql.Key, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddRange(sql.Value.ToArray());
                    cmd.ExecuteNonQuery();
                }
            }

            return updateTabs.Length;
        }

        /// <summary>
        /// 批量更新记录(无事务)
        /// </summary>
        public static int UpdateNoTrans<T, TK>(this IEnumerable<Table2<T, TK>> tables, Func<T, string> condition)
        {
            if (tables == null)
            {
                return -1;
            }
            var updateTabs = tables.Where(a => a.__EditColumns.Count > 0).ToArray();
            if (updateTabs.Length == 0)
            {
                return 0;
            }

            var sqls = new List<KeyValuePair<string, List<DbParameter>>>();
            var sb = new StringBuilder();
            var paraIndex = 0;
            var parameters = new List<DbParameter>();

            foreach (var table in updateTabs)
            {
                if (table.__EditColumns.Count > 0)
                {
                    sb.Append("\nupdate ").Append(table.TableName).Append(" set ");
                    foreach (var column in table.__EditColumns)
                    {
                        sb.Append("[").Append(column.Key).Append("]=@p").Append(parameters.Count.ToString()).Append(",");
                        parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + parameters.Count, column.Value));
                        paraIndex += 2;
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(" where ").Append(condition((T)(object)table));

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

            using (var conn = DbBase2<TK>.GetConnection(false))
            {
                foreach (var sql in sqls)
                {
                    var cmd = Sql2.CreateDbCommand(sql.Key, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddRange(sql.Value.ToArray());
                    cmd.ExecuteNonQuery();
                }
            }

            return updateTabs.Length;
        }

        public static int UpdateNoTrans(this IEnumerable<TableBase2> tables, Func<TableBase2, string> condition)
        {
            if (tables == null)
            {
                return -1;
            }
            var updateTabs = tables.Where(a => a.__EditColumns.Count > 0).ToArray();
            if (updateTabs.Length == 0)
            {
                return 0;
            }

            var dbType = updateTabs.First().DbType;

            var sqls = new List<KeyValuePair<string, List<DbParameter>>>();
            var sb = new StringBuilder();
            var paraIndex = 0;
            var parameters = new List<DbParameter>();

            foreach (var table in updateTabs)
            {
                if (table.__EditColumns.Count > 0)
                {
                    sb.Append("\nupdate ").Append(table.TableName).Append(" set ");
                    foreach (var column in table.__EditColumns)
                    {
                        sb.Append("[").Append(column.Key).Append("]=@p").Append(parameters.Count.ToString()).Append(",");
                        parameters.Add(DbBase2.CreateDbParameter(dbType, "@p" + parameters.Count, column.Value));
                        paraIndex += 2;
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(" where ").Append(condition(table));

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

            using (var conn = DbBase2.GetConnection(dbType))
            {
                foreach (var sql in sqls)
                {
                    var cmd = Sql2.CreateDbCommand(sql.Key, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddRange(sql.Value.ToArray());
                    cmd.ExecuteNonQuery();
                }
            }

            return updateTabs.Length;
        }

        /// <summary>
        /// 批量更新记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK"></typeparam>
        /// <param name="table"></param>
        /// <param name="condition">条件</param>
        /// <returns>更新记录数</returns>
        [Obsolete("已过时,请先查询数据,判断更新或插入,避免不必要的更新")]
        public static int InsertOrUpdate<T, TK>(this Table2<T, TK> table, string condition)
        {
            if (table == null)
            {
                return -1;
            }
            if (table.__EditColumns.Count == 0)
            {
                return 0;
            }

            var tableName = table.TableName;
            var paraIndex = 0;
            var sb = new StringBuilder();
            var parameters = new List<DbParameter>();

            sb.Append("if not exists (select 1 from ").Append(tableName).Append(" where ").Append(condition).Append(")\n");
            sb.Append("insert into ").Append(tableName).Append(" (");
            foreach (var column in table.__EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("],");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")values(");
            var update = new StringBuilder();
            foreach (var column in table.__EditColumns)
            {
                sb.Append("@p").Append(paraIndex).Append(",");
                update.Append("[").Append(column.Key).Append("]=@p").Append(paraIndex).Append(",");

                parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + paraIndex, column.Value));
                paraIndex++;
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")\n");
            sb.Append("else update ").Append(tableName).Append(" set ");
            sb.Append(update);
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" where ").Append(condition).Append("\n");

            return DbBase2<TK>.ExecuteNonQuery(sb.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 批量更新记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK"></typeparam>
        /// <param name="tables"></param>
        /// <param name="condition">条件</param>
        /// <returns>更新记录数</returns>
        [Obsolete("已过时,请先查询数据,判断更新或插入,避免不必要的更新")]
        public static int InsertOrUpdate<T, TK>(this IEnumerable<Table2<T, TK>> tables, Func<T, string> condition)
        {
            if (tables == null)
            {
                return -1;
            }
            var updateTabs = tables.Where(a => a.__EditColumns.Count > 0).ToArray();
            if (updateTabs.Length == 0)
            {
                return 0;
            }

            var sw = Stopwatch.StartNew();
            var tableName = updateTabs[0].TableName;
            var sqls = new List<KeyValuePair<string, List<DbParameter>>>();
            try
            {
                var paraIndex = 0;
                var sb = new StringBuilder();
                var parameters = new List<DbParameter>();

                foreach (var table in updateTabs)
                {
                    var where = condition((T)(object)table);
                    sb.Append("if not exists (select 1 from ").Append(tableName).Append(" where ").Append(where).Append(")\n");
                    sb.Append("insert into ").Append(tableName).Append(" (");
                    foreach (var column in table.__EditColumns)
                    {
                        sb.Append("[").Append(column.Key).Append("],");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(")values(");
                    foreach (var column in table.__EditColumns)
                    {
                        sb.Append("@p").Append(paraIndex).Append(",");
                        parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + paraIndex, column.Value));
                        paraIndex++;
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(")\n");
                    sb.Append("else update ").Append(tableName).Append(" set ");
                    foreach (var column in table.__EditColumns)
                    {
                        sb.Append("[").Append(column.Key).Append("]=@p").Append(paraIndex).Append(",");
                        parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + paraIndex, column.Value));
                        paraIndex++;
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(" where ").Append(where).Append("\n");

                    if (paraIndex > 1500)
                    {
                        sqls.Add(new KeyValuePair<string, List<DbParameter>>(sb.ToString(), parameters));
                        sb = new StringBuilder();
                        paraIndex = 0;
                        parameters = new List<DbParameter>();
                    }
                }
                if (sb.Length > 0)
                {
                    sqls.Add(new KeyValuePair<string, List<DbParameter>>(sb.ToString(), parameters));
                }

                using (var trans = new TransactionScope(TransactionScopeOption.Required, DbBase2.TransactionOption))
                {
                    using (var conn = DbBase2<TK>.GetConnection(false))
                    {
                        foreach (var sql in sqls)
                        {
                            var cmd = Sql2.CreateDbCommand(sql.Key, conn);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddRange(sql.Value.ToArray());
                            cmd.ExecuteNonQuery();
                        }
                        trans.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace + sqls.Join("\n"));
                return -1;
            }
            finally
            {
                var useTime = sw.ElapsedMilliseconds;
                var logStr = "Table.InsertOrUpdate({0}[{1}]) 执行时间 {2} 毫秒".Formats(tableName, updateTabs.Length, useTime.ToString("F0"));
                if (useTime / 1000 > 5)
                {
                    Log.Warn(logStr);
                }

                Log.Debug(logStr);
            }
            return updateTabs.Length;
        }

        /// <summary>
        /// 批量更新记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK"></typeparam>
        /// <param name="tables"></param>
        /// <param name="condition">条件</param>
        /// <returns>更新记录数</returns>
        [Obsolete("已过时,请先查询数据,判断更新或插入,避免不必要的更新")]
        public static int InsertOrUpdateNoTrans<T, TK>(this IEnumerable<Table2<T, TK>> tables, Func<T, string> condition)
        {
            if (tables == null)
            {
                return -1;
            }
            var updateTabs = tables.Where(a => a.__EditColumns.Count > 0).ToArray();
            if (updateTabs.Length == 0)
            {
                return 0;
            }

            var tableName = updateTabs[0].TableName;
            var sqls = new List<KeyValuePair<string, List<DbParameter>>>();
            var paraIndex = 0;
            var sb = new StringBuilder();
            var parameters = new List<DbParameter>();

            foreach (var table in updateTabs)
            {
                var where = condition((T)(object)table);
                sb.Append("if not exists (select 1 from ").Append(tableName).Append(" where ").Append(where).Append(")\n");
                sb.Append("insert into ").Append(tableName).Append(" (");
                foreach (var column in table.__EditColumns)
                {
                    sb.Append("[").Append(column.Key).Append("],");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(")values(");
                foreach (var column in table.__EditColumns)
                {
                    sb.Append("@p").Append(paraIndex).Append(",");
                    parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + paraIndex, column.Value));
                    paraIndex++;
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(")\n");
                sb.Append("else update ").Append(tableName).Append(" set ");
                foreach (var column in table.__EditColumns)
                {
                    sb.Append("[").Append(column.Key).Append("]=@p").Append(paraIndex).Append(",");
                    parameters.Add(DbBase2<TK>.CreateDbParameter("@p" + paraIndex, column.Value));
                    paraIndex++;
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(" where ").Append(where).Append("\n");

                if (paraIndex > 1500)
                {
                    sqls.Add(new KeyValuePair<string, List<DbParameter>>(sb.ToString(), parameters));
                    sb = new StringBuilder();
                    paraIndex = 0;
                    parameters = new List<DbParameter>();
                }
            }
            if (sb.Length > 0)
            {
                sqls.Add(new KeyValuePair<string, List<DbParameter>>(sb.ToString(), parameters));
            }
            var conn = DbBase2<TK>.GetConnection(false);
            foreach (var sql in sqls)
            {
                var cmd = Sql2.CreateDbCommand(sql.Key, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddRange(sql.Value.ToArray());
                cmd.ExecuteNonQuery();
            }
            conn.Close();

            return updateTabs.Length;
        }

        /// <summary>
        /// 获取字段中文名
        /// </summary>
        public static string GetColumnDescription<T, TK>(this Table2<T, TK> table, string columnEnName)
        {
            if (columnEnName == null)
            {
                return "";
            }
            string columnName;
            if (!Cloumns.TryGetValue(columnEnName, out columnName))
            {
                columnName = table.GetType().GetCustomAttributeValue<DescriptionAttribute>(a => a.Description, columnEnName);
                Cloumns[columnEnName] = columnName;
            }
            if (columnName.IsNullOrEmpty())
            {
                columnName = columnEnName;
            }
            return columnName;
        }
    }
}
