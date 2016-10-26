﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Comm.Global.Exception;

namespace Comm.Cloud.RDS
{

    public abstract class DbHelperSql
    {

        public string ConnectionString;

        #region 内部方法
        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandText = cmdText;

            if (cmdParms == null) return;
            foreach (var parameter in cmdParms)
            {
                if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                    (parameter.Value == null))
                {
                    parameter.Value = DBNull.Value;
                }
                cmd.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sqlString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string sqlString)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        var obj = cmd.ExecuteScalar();
                        if ((Equals(obj, null)) || (Equals(obj, DBNull.Value)))
                        {
                            return null;
                        }
                        return obj;
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        throw new UtilCrashException("执行sql getsingle异常", ex);
                    }
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sqlString)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        var rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        throw new UtilCrashException("执行sql异常", ex);
                    }
                }
            }
        }
        private int ExecuteSql(string sqlString, params SqlParameter[] cmdParms)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlString, cmdParms);
                        var rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SqlException ex)
                    {
                        throw new UtilCrashException("执行sql 串异常", ex);
                    }
                }
            }
        }
        private bool ExecuteSqlTran(IList<string> sqlStringList)
        {
            var isOk = false;
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    var cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (var item in sqlStringList)
                        {
                            //注意，此处的sql没有安全过滤
                            PrepareCommand(cmd, conn, trans, item, null);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        isOk = true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        isOk = false;
                        throw new UtilCrashException("执行sql事务异常", ex);
                    }
                }
            }
            return isOk;
        }

        private object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] parameters)
        {
            var cmd = new SqlCommand();
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    //SqlCommand cmd = BuildQueryCommand(connection, cmdText, parameters);
                    cmd.CommandType = cmdType;
                    cmd.CommandText = cmdText;
                    PrepareCommand(cmd, connection, null, cmdText, parameters);
                    var val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch (Exception ex)
            {
                throw new UtilCrashException("执行ExecuteScalar异常", ex);
            }
        }

        /// <summary>
        /// 建立属性值字典,key字段返回全部为小写!
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<Dictionary<string, object>> CreateDictinaryList(DbDataReader reader)
        {
            var list = new List<Dictionary<string, object>>();
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



        private List<Dictionary<string, object>> ExecuteReaderDictionaryList(CommandType cmdType, string sqlString, SqlParameter[] cmdParms)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand())
                    {
                        try
                        {
                            PrepareCommand(cmd, connection, null, sqlString, cmdParms);
                            var reader = cmd.ExecuteReader();
                            var list = CreateDictinaryList(reader);
                            return list;
                        }
                        catch (SqlException ex)
                        {
                            throw new UtilCrashException("执行readlist异常", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new UtilCrashException("读数据库泛型集合异常", ex);
            }
            return null;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private bool Exists(string strSql)
        {
            var obj = GetSingle(strSql);
            int cmdresult;
            if ((Equals(obj, null)) || (Equals(obj, DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            return true;
        }

        //添加
        private string MakeSqlForInsert(string tableName, IDictionary<string, object> fields)
        {
            var sb = new StringBuilder(2048);
            sb.Append("INSERT INTO ").Append(tableName).Append(" (");
            foreach (var pi in fields)
            {
                //非空对象需要插入
                if (pi.Value != null)
                {
                    sb.Append(pi.Key).Append(",");
                }
            }
            sb.Length -= 1;
            sb.Append(")");

            sb.Append(" VALUES (");
            foreach (var pi in fields)
            {
                //非空对象需要插入
                if (pi.Value != null)
                {
                    sb.Append("?").Append(pi.Key).Append(",");
                }
            }
            sb.Length -= 1;
            sb.Append(") ");
            return sb.ToString();
        }

        private string MakeSqlForUpdate(string tableName, IDictionary<string, object> fields, string sqlWhere)
        {
            var sb = new StringBuilder(2048);
            sb.Append("UPDATE ").Append(tableName).Append(" SET ");
            foreach (var pi in fields)
            {
                //非空对象需要插入

                //if (pi.Value != null)
                //{
                sb.Append(pi.Key).Append("=?").Append(pi.Key).Append(",");
                //}
            }
            sb.Length -= 1;
            sb.Append(" WHERE ").Append(sqlWhere);
            return sb.ToString();
        }

        private SqlParameter[] MakeInsertSqlParams(string tableName, IDictionary<string, object> fields)
        {
            var paramList = new List<SqlParameter>();
            foreach (var pi in fields)
            {
                //非空对象需要插入
                if (pi.Value != null)
                {
                    paramList.Add(new SqlParameter("?" + pi.Key, pi.Value));
                }
            }
            return paramList.ToArray();
        }
        private SqlParameter[] MakeUpdateSqlParams(string tableName, IDictionary<string, object> fields)
        {
            var paramList = new List<SqlParameter>();
            foreach (var pi in fields)
            {
                //非空对象需要插入
                //if (pi.Value != null)
                //{
                paramList.Add(new SqlParameter("?" + pi.Key, pi.Value));
                //}
            }
            return paramList.ToArray();
        }

        private string MakeSqlForInsert2(string tableName, IDictionary<string, object> fields)
        {
            var sb = new StringBuilder(2048);
            sb.Append("insert into ");
            sb.Append(tableName);
            sb.Append("(");

            var sbValues = new StringBuilder(1024);
            sbValues.Append(" values (");

            foreach (var pi in fields)
            {
                if (pi.Value != null) //非空对象需要插入
                {
                    var value = pi.Value.ToString().Replace("'", "''"); //单引号转义
                    sb.Append(pi.Key).Append(",");
                    sbValues.Append("'").Append(value).Append("',");
                }
            }
            sb.Length -= 1;
            sb.Append(")");

            sbValues.Length -= 1;
            sb.Append(sbValues).Append(")");
            return sb.ToString();
        }
        #endregion
    }
}
