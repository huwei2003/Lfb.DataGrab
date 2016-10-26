using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Comm.Cloud.IRDS;
using Comm.Global.Db;
using Comm.Global.Exception;
using Comm.Tools.Utility;

namespace Comm.Cloud.RDS
{

    public class DbSqlServer : DbBase
    {
        private static readonly Log Logger = new Log("DbSqlServer");

        ///// <summary>
        ///// 获取数据库连接
        ///// </summary>
        //public override SqlConnection GetSqlConnection(string dbConnectstring)
        //{
        //    SqlConnection conn = null;
        //    try
        //    {
        //        var sb = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings[dbConnectstring])
        //        {
        //            ApplicationIntent = ApplicationIntent.ReadWrite
        //        };
        //        conn = new SqlConnection(sb.ConnectionString);
        //        conn.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        Close(conn);
        //        Loger.Error(ex.Message + ex.StackTrace);
        //    }
        //    return conn;
        //}


        ///// <summary>
        ///// 获取数据库连接
        ///// </summary>
        //public override DbConnection GetConnection(string dbConnectstring)
        //{
        //    DbConnection conn = null;
        //    try
        //    {
        //        var sb = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings[dbConnectstring])
        //        {
        //            ApplicationIntent = ApplicationIntent.ReadWrite
        //        };
        //        conn = new SqlConnection(sb.ConnectionString);

        //    }
        //    catch (Exception ex)
        //    {
        //        Close(conn);
        //        Loger.Error(ex.Message + ex.StackTrace);
        //    }
        //    return conn;
        //}

        #region ===== db sys =====
        public override string PrepareParameter(string strSql, object[] parameters, int paraStartIndex)
        {
            if (strSql.Contains("?"))
            {
                var ss = strSql.Split('?');
                if (ss.Length - 1 == parameters.Length)
                {
                    var sb = new StringBuilder();
                    for (var i = 0; i < ss.Length - 1; i++)
                    {
                        sb.Append(ss[i]).Append("@p").Append((i + paraStartIndex).ToString());
                        parameters[i] = CreateDbParameter("@p" + (i + paraStartIndex), parameters[i]);
                    }
                    sb.Append(ss[ss.Length - 1]);
                    strSql = sb.ToString();
                }
                else
                {
                    throw new Exception("Sql.PrepareParameter strSql(" + strSql + ") 参数数量不对");
                }
            }

            return strSql;
        }

        public override DbParameter CreateDbParameter(string name, object value)
        {
            DbParameter para = new SqlParameter();

            para.ParameterName = name;
            if (value == null)
            {
                para.IsNullable = true;
                para.Value = DBNull.Value;
            }
            else
            {
                para.Value = value;
            }
            return para;
        }


        public DbSqlServer(string dbConnectstring)
        {


            ConnectionString = dbConnectstring;
            IsOn = ConnectionString.HasValue();
            if (!IsOn)
            {
                return;
            }

            var sb = new SqlConnectionStringBuilder(ConnectionString);
            ConnectionString = sb.ConnectionString;
            IsOn = ConnectionString.HasValue();
            if (!IsOn)
            {
                return;
            }
            DbName = sb.InitialCatalog;
            EnableReadOnly = ConnectionString.Contains("ApplicationIntent");

        }



        public override DbCommand CreateDbCommand(string strSql, DbConnection conn)
        {
            if (!IsOn)
            {
                return null;
            }
            DbCommand cmd = new SqlCommand(strSql);

            cmd.Connection = conn;
            return cmd;
        }

        public override DbDataAdapter CreateDbDataAdapter(DbCommand cmd)
        {
            if (!IsOn)
            {
                return null;
            }
            DbDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = cmd;
            return da;
        }



        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="isReadOnly">是否只读</param>
        public override DbConnection GetConnection(bool isReadOnly)
        {
            if (!IsOn)
            {
                return null;
            }
            DbConnection conn = null;
            try
            {

                var sb = new SqlConnectionStringBuilder(ConnectionString)
                {
                    ApplicationIntent = EnableReadOnly && isReadOnly ? ApplicationIntent.ReadOnly : ApplicationIntent.ReadWrite
                };
                conn = new SqlConnection(sb.ConnectionString);

                conn.Open();
            }
            catch (Exception ex)
            {
                Close(conn);
                Logger.Error(ex.Message + ex.StackTrace);
            }
            return conn;
        }
        #endregion

        ///// <summary>
        ///// 获取数据库连接
        ///// </summary>
        ///// <param name="isReadOnly">是否只读</param>
        //public override SqlConnection GetSqlConnection(bool isReadOnly)
        //{
        //    if (!IsOn)
        //    {
        //        return null;
        //    }
        //    SqlConnection conn = null;
        //    try
        //    {
        //        var sb = new SqlConnectionStringBuilder(ConnectionString)
        //        {
        //            ApplicationIntent = isReadOnly ? ApplicationIntent.ReadOnly : ApplicationIntent.ReadWrite
        //        };
        //        conn = new SqlConnection(sb.ConnectionString);
        //        conn.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        Close(conn);
        //        Log.Error(ex.Message + ex.StackTrace);
        //    }
        //    return conn;
        //}


        /// <summary>
        /// 获取某个表的所有记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetDataAll<T>()
        {
            var strSql = "SELECT * FROM " + typeof(T).Name;
            var dt = SelectDataTable(strSql);
            return dt.ToList<T>();
        }

        public override string GetInsertIdSql<T>(Table<T> table, string condition, string idFieldName, List<object> parameters)
        {
            //parameters = new List<object>();
            if (table.EditColumns.Count == 0)
            {
                return "";
            }

            var sb = new StringBuilder();

            if (condition.NotNullOrEmpty())
            {
                sb.Append("if not exists(select 1 from ").Append(table.TableName).Append(" where ").Append(condition).Append(")\nbegin\n");
            }
            sb.Append("insert into ").Append(table.TableName).Append(" (");
            foreach (var column in table.EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("],");
                parameters.Add(column.Value);
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")values(");
            var columnCount = table.EditColumns.Count;
            for (var i = 0; i < columnCount; i++)
            {
                sb.Append("?,");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")\n");
            if (condition.NotNullOrEmpty())
            {
                sb.Append("end\n");
            }
            if (string.IsNullOrWhiteSpace(idFieldName))
            {
                sb.Append("select max(").Append("Id").Append(") from ").Append(table.TableName);
            }
            else
            {
                sb.Append("select max(").Append(idFieldName).Append(") from ").Append(table.TableName);
            }
            //sb.Append(")\nselect SCOPE_IDENTITY()\n");
            if (condition.NotNullOrEmpty())
            {
                sb.Append(" where ").Append(condition);
            }

            table.EditColumns.Clear();
            return sb.ToString();
        }


        public override string GetInsertSql<T>(Table<T> table, string condition, List<object> parameters)
        {
            if (table.EditColumns.Count == 0)
            {
                return "";
            }

            var sb = new StringBuilder();

            if (condition.NotNullOrEmpty())
            {
                sb.Append("if not exists(select 1 from ").Append(table.TableName).Append(" where ").Append(condition).Append(")\nbegin\n");
            }
            sb.Append("insert into ").Append(table.TableName).Append(" (");
            foreach (var column in table.EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("],");
                parameters.Add(column.Value);
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")values(");
            var columnCount = table.EditColumns.Count;
            for (var i = 0; i < columnCount; i++)
            {
                sb.Append("?,");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")\n");
            if (condition.NotNullOrEmpty())
            {
                sb.Append("end");

            }

            table.EditColumns.Clear();
            return sb.ToString();
        }

        public override string GetInsertOrUpdateSql<T>(Table<T> table, string condition, List<object> parameters)
        {
            if (table == null)
            {
                return "";
            }
            if (table.EditColumns.Count == 0)
            {
                return "";
            }

            var tableName = table.TableName;
            var paraIndex = 0;
            var sb = new StringBuilder();


            sb.Append("if not exists (select 1 from ").Append(tableName).Append(" where ").Append(condition).Append(")\n");
            sb.Append("insert into ").Append(tableName).Append(" (");
            foreach (var column in table.EditColumns)
            {
                sb.Append("[").Append(column.Key).Append("],");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")values(");
            StringBuilder update = new StringBuilder();

            ;
            foreach (var column in table.EditColumns)
            {
                sb.Append("@p").Append(paraIndex).Append(",");
                update.Append("[").Append(column.Key).Append("]=@p").Append(paraIndex).Append(",");

                parameters.Add(CreateDbParameter("@p" + paraIndex, column.Value));
                paraIndex++;
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(")\n");
            sb.Append("else update ").Append(tableName).Append(" set ");
            sb.Append(update);
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" where ").Append(condition).Append("\n");
            return sb.ToString();
        }

        public override string GetUpdateSql<T>(Table<T> table, string condition, List<Object> parameters)
        {
            if (table.EditColumns.Count == 0)
            {
                return "";
            }

            var sb = new StringBuilder();
            sb.Append("update ").Append(table.TableName).Append(" set ");
            foreach (var column in table.EditColumns)
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

            table.EditColumns.Clear();

            return sb.ToString();
        }

        public override string GetUpdateFieldStepSql(string tableName, Dictionary<string, object> fields,
            string condition, List<Object> parameters)
        {
            if (fields.Count == 0)
            {
                return "";
            }

            var sb = new StringBuilder();
            sb.Append("update ").Append(tableName).Append(" set ");
            foreach (var column in fields)
            {
                sb.Append("[").Append(column.Key).Append("] = [").Append(column.Key).Append("]+" + column.Value + ",");
                //parameters.Add(column.Value);
            }
            if (sb.ToString().EndsWith(","))
            {
                sb.Remove(sb.Length - 1, 1);
            }

            if (condition.HasValue())
            {
                sb.Append(" where ").Append(condition);
            }

            return sb.ToString();
        }

        public override string GetSelectPagedSql(string qrySql, string statSql, int pageSize, int pageIndex)
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
                if (count % 2 == 0)
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

            //var maxPindex = -1;
            //if (indexs.Length > 0)
            //{
            //    maxPindex = indexs.Max(a => a.Match(@"\d+").ToInt32());
            //}
            var sb = new StringBuilder();
            sb.AppendLine("DECLARE @PageSize INT")
                .AppendLine("DECLARE @PageIndex INT")
                .Append("SET @PageSize=?;")
                .Append("SET @PageIndex=?;");
            //.Append("SET @PageSize={").Append(maxPindex + 1).AppendLine("}")
            //    .Append("SET @PageIndex={").Append(maxPindex + 2).AppendLine("};");
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
            var strSql = sb.ToString();
            //var parameters2 = parameters.ToList();
            //parameters2.Add(pageSize);
            //parameters2.Add(pageIndex);
            //parameters = parameters2.ToArray();
            return strSql;
        }

        public override string GetTruncateTablesSql(IEnumerable<string> tables)
        {
            //没考虑外键约束
            var sb = new StringBuilder();
            foreach (var item in tables)
            {
                sb.Append("TRUNCATE TABLE ").Append(item).Append(" \n");
            }
            
            return sb.ToString();
        }

        public override string GetDropTablesSql(IEnumerable<string> tables)
        {
            //没考虑外键约束
            var sb = new StringBuilder();
            foreach (var item in tables)
            {
                sb.Append("Declare @SQLText Varchar(1000)\n");
                sb.Append("If Exists(Select Top 1 Name From Sysobjects Where Name='").Append(item).Append("' And XType='U')\n");
                sb.Append("Begin\n");
                sb.Append("Set @SQLText='Drop Table ").Append(item).Append("'\n");
                sb.Append("Exec(@SQLText)\n");
                sb.Append("End \n");
            }

            return sb.ToString();
        }

        public override string GetCreateTablesSql(IEnumerable<DbTableDefine> tableDefines)
        {
            var list = new List<string>();
            var sb = new StringBuilder(1024);
            foreach (var dbTableDefine in tableDefines)
            {
                sb.Clear();
                sb.Append("CREATE TABLE [dbo].[").Append(dbTableDefine.Name).Append("] (\n");
                foreach (var fieldDefine in dbTableDefine.FieldDefines)
                {
                    var dbField = ConvertToSqlServerType(fieldDefine.Value.Type, fieldDefine.Value.Length);
                    sb.Append("[").Append(fieldDefine.Key).Append("] ").Append("").Append(dbField).Append(",\n");
                }
                sb.Append("PRIMARY KEY ([").Append(dbTableDefine.PrimaryKey).Append("])");
                sb.Append(")");
                //sb.Append(") DEFAULT CHARSET=utf8");
                list.Add(sb.ToString());
            }

            var sb2 = new StringBuilder();
            foreach (var item in list)
            {
                sb2.Append(item);
            }
            return sb2.ToString();
        }

        public override string GetCreateTableForeignKeysSql(IEnumerable<DbForeignKeyDefine> foreignKeyDefines)
        {
            //未测试
            var list = new List<string>();
            var sb = new StringBuilder(1024);
            foreach (var foreignKeyDefine in foreignKeyDefines)
            {
                sb.Clear();
                sb.Append("ALTER TABLE `").Append(foreignKeyDefine.SlaveTableName)//从表名
                    .Append("` ADD FOREIGN KEY (`").Append(foreignKeyDefine.SlaveFieldName)//从表字段
                    .Append("`) REFERENCES `").Append(foreignKeyDefine.MasterTableName)//主表
                    .Append("` (`").Append(foreignKeyDefine.MasterFieldName).Append("`)");//主表字段
                list.Add(sb.ToString());
            }
            var sb2 = new StringBuilder();
            foreach (var item in list)
            {
                sb2.Append(item);
            }
            return sb2.ToString();

        }

        public override string GetTableStructSql(string tableName, string dbName)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT col.name AS COLUMN_NAME,t.name AS Data_Type FROM dbo.syscolumns col ");
            sb.Append("LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype ");
            sb.Append("inner JOIN dbo.sysobjects obj ON col.id = obj.id ");
            sb.Append("AND obj.xtype = 'U' AND obj.status >= 0 ");
            sb.Append("WHERE obj.name = '").Append(tableName).Append("'");
            sb.Append("ORDER BY col.colorder;");
            sb.Append("");

            return sb.ToString();
        }



        /// <summary>
        /// c#数据类型转换成db数据类型
        /// </summary>
        /// <param name="csType"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string ConvertToSqlServerType(Type csType, int length)
        {
            if (csType.IsEnum || csType == typeof(int) || csType == typeof(uint))
                return "int NOT NULL DEFAULT 0";
            if (csType == typeof(string))
                return String.Format("varchar ({0}) NOT NULL DEFAULT ''", length);
            if (csType == typeof(long) || csType == typeof(ulong))
                return "bigint NOT NULL DEFAULT '0'";
            if (csType == typeof(DateTime))
                return "datetime NOT NULL DEFAULT GETDATE()";
            if (csType == typeof(decimal))
                return "decimal(15,2) NOT NULL DEFAULT '0.00'";
            if (csType == typeof(float))
                return "float NOT NULL DEFAULT '0.0'";
            if (csType == typeof(double))
                return "double NOT NULL DEFAULT '0.0'";
            throw new UtilCrashException(string.Format("不支持类型{0}转为数据库类型", csType.Name));
        }

    }
}
