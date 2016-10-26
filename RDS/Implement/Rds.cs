using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comm.Cloud.IRDS;
using Comm.Cloud.RDS.DTO;
using Comm.Global.Config;
using Comm.Global.Enum.Sys;
using Comm.Global.Exception;
using MySql.Data.MySqlClient;
using DbType = Comm.Global.Enum.Sys.DbType;

namespace Comm.Cloud.RDS
{
    public class Rds : IRds
    {
        //private static readonly ILogger Logger = LoggerFactory.GetInstanse(typeof(Rds));
        private string _connectionString;
        
        private PublicCloudRdsConfig _config;

        /// <summary>
        /// 每个微服务一个rds云实例/或共享云实例中的一个数据库
        /// </summary>
        /// <param name="hostServiceName">宿主微服务</param>
        public Rds(ServiceName hostServiceName)
        {
            var config =ServiceConfigHelper.GlobalServiceConfig.
                GetServiceFromBuffer<PublicCloudRdsConfig>(
                ServiceName.Cloud_RdsService,hostServiceName);
            Init(config);

        }
        public Rds(ServiceName hostServiceName,DbType dbType)
        {
            var config = ServiceConfigHelper.GlobalServiceConfig.
                GetServiceFromBuffer<PublicCloudRdsConfig>(
                ServiceName.Cloud_RdsService, hostServiceName);
            Init(config, dbType);
        }
        /// <summary>
        /// 只用于集成测试构造
        /// </summary>
        /// <param name="config"></param>
        public Rds(PublicCloudRdsConfig config)
        {
            Init(config);
        }
        private void Init(PublicCloudRdsConfig config)
        {
            _config = config;
            _connectionString = config.GetDbConnectString();
           
            //注意所有mysql使用同一个静态连接串，表示一个服务进程只有一个mysql连接才不会出问题
            // DbHelperMySql.ConnectionString = config.GetDbConnectString();
        }

        private void Init(PublicCloudRdsConfig config, DbType dbType)
        {
            _config = config;
            _connectionString = config.GetDbConnectString();

            
        }
    
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

        /// <summary>
        /// c#数据类型转换成db数据类型
        /// </summary>
        /// <param name="csType"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string ConvertToMysqlType(Type csType,int length)
        {
            if (csType.IsEnum || csType == typeof(int) || csType == typeof(uint))
                return "int (11) NOT NULL DEFAULT '0'";
            if (csType == typeof(string))
                return String.Format("varchar ({0})", length);
            if (csType == typeof(long) || csType == typeof(ulong))
                return "bigint(20) NOT NULL DEFAULT '0'";
            if (csType == typeof(DateTime))
                return "datetime NOT NULL DEFAULT CURRENT_TIMESTAMP";
            if (csType == typeof(decimal))
                return "decimal(15,2) NOT NULL DEFAULT '0.00'";
            if (csType == typeof(float))
                return "float NOT NULL DEFAULT '0.0'";
            if (csType == typeof(double))
                return "double NOT NULL DEFAULT '0.0'";
            throw new UtilCrashException(string.Format("不支持类型{0}转为数据库类型",csType.Name));
        }

        


        public async Task<long> GetCountAsync(string table, string sqlWhere)
        {
            return await Task.Run(() =>
            {
                var sql = string.IsNullOrWhiteSpace(sqlWhere)
                    ?"SELECT count(*) FROM " + table
                    : string.Format("SELECT count(*) FROM {0} where {1}",table,sqlWhere);
                var obj = DbHelperMySql.ExecuteScalar(CommandType.Text, sql, null);
                long recordNum = 0;
                long.TryParse(obj.ToString(), out recordNum);
                return recordNum;
            });
        }

        public async Task<bool> IsExistAsync(string table, string sqlWhere)
        {
            var ret = await Task.Run(() =>
            {
                var sql = "SELECT COUNT(*) FROM " + table + " WHERE " + sqlWhere;
                var obj = DbHelperMySql.Exists(sql);
                return obj;
            });
            return ret;
        }


        public async Task<List<Dictionary<string, object>>> GetAllAsync(string sql)
        {
            return await Task.Run(() =>
            {
                var obj = DbHelperMySql.ExecuteReaderDictionaryList(CommandType.Text, sql, null);
                return obj;
            });
        }


        public async Task<bool> RunSqlAsync(string sql)
        {
            var ret = await Task.Run(() =>
            {
                var obj = DbHelperMySql.ExecuteSql(sql);
                return obj;
            });
            return ret > 0;
        }

        public async Task<bool> TransactionAsync(IList<string> sqls)
        {
            var ret = await Task.Run(() =>
            {
                var obj = DbHelperMySql.ExecuteSqlTran(sqls);
                return obj;
            });
            return ret;
        }

        public async Task<bool> CreateAsync(string table, IDictionary<string, object> fields)
        {
            var sql = DbHelperMySql.MakeSqlForInsert(table, fields);
            var param = DbHelperMySql.MakeInsertSqlParams(table, fields);

            var ret = await Task.Run(() => DbHelperMySql.ExecuteSql(sql, param) > 0);
            return ret;
        }

        public async Task<bool> PatchAsync(string table, string sqlWhere, IDictionary<string, object> fields)
        {
            var sql = DbHelperMySql.MakeSqlForUpdate(table, fields, sqlWhere);
            var param = DbHelperMySql.MakeUpdateSqlParams(table, fields);


            var ret = await Task.Run(() => DbHelperMySql.ExecuteSql(sql, param) > 0);
            return ret;
        }
     
        public async Task<bool> DeleteAsync(string table, string sqlWhere)
        {
            var sql = "DELETE FROM " + table + " WHERE " + sqlWhere;
            var ret = await Task.Run(() =>
            {
                var obj = DbHelperMySql.ExecuteSql(sql);
                return obj;
            });
            return ret > 0;
        }
        public async Task<bool> TruncateTablesAsync(IEnumerable<string> tables)
        {
            var list = new List<string> { "SET foreign_key_checks=0" };//去除外键关联
            
            list.AddRange(tables.Select(table => "TRUNCATE TABLE " + table));
            list.Add("SET foreign_key_checks=1");//重新外键关联

            return await Task.Run(() => DbHelperMySql.ExecuteSqlTran(list));
        }
        public async Task<bool> DropTablesAsync(IEnumerable<string> tables)
        {
            var list = new List<string> { "SET foreign_key_checks=0" };//去除外键关联
            list.AddRange(tables.Select(table => "DROP TABLE IF EXISTS " + table));
            list.Add("SET foreign_key_checks=1");//重新外键关联
            return await Task.Run(() => DbHelperMySql.ExecuteSqlTran(list));
        }


        public async Task<bool> CreateTablesAsync(IEnumerable<DbTableDefine> tableDefines)
        {
            var list = new List<string>();
            var sb=new StringBuilder(1024);
            foreach (var dbTableDefine in tableDefines)
            {
                sb.Clear();
                sb.Append("CREATE TABLE `").Append(dbTableDefine.Name).Append("` (");
                foreach (var fieldDefine in dbTableDefine.FieldDefines)
                {
                    var dbField = ConvertToMysqlType(fieldDefine.Value.Type, fieldDefine.Value.Length);
                    sb.Append("`").Append(fieldDefine.Key).Append("` ").Append(dbField).Append(',');
                }
                sb.Append("PRIMARY KEY (`").Append(dbTableDefine.PrimaryKey).Append("`)");
                sb.Append(") DEFAULT CHARSET=utf8");
                list.Add(sb.ToString());
            }
            return await Task.Run(() => DbHelperMySql.ExecuteSqlTran(list));

        }

        public async Task<bool> CreateTableForeignKeysAsync(IEnumerable<DbForeignKeyDefine> foreignKeyDefines)
        {
            var list = new List<string>();
            var sb = new StringBuilder(1024);
            foreach (var foreignKeyDefine in foreignKeyDefines)
            {
                sb.Clear();
                sb.Append("ALTER TABLE `").Append(foreignKeyDefine.SlaveTableName)//从表名
                    .Append("` ADD FOREIGN KEY (`").Append(foreignKeyDefine.SlaveFieldName)//从表字段
                    .Append("`) REFERENCES `").Append(foreignKeyDefine.MasterTableName)//主表
                    .Append("` (`").Append(foreignKeyDefine.MasterFieldName) .Append("`)");//主表字段
                list.Add(sb.ToString());
            }
            return await Task.Run(() => DbHelperMySql.ExecuteSqlTran(list));

        }

        public async Task<Dictionary<string, Type>> GetTableStructAsync(string table)
        {
            var dict = new Dictionary<string, Type>();
            var sqlString = "SELECT COLUMN_NAME,Data_Type FROM information_schema.COLUMNS WHERE table_name = '" +
                                table + "' and table_schema='" + _config.Database + "'";
            try
            {
                return await Task.Run(() =>
                {
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        using (var cmd = new MySqlCommand())
                        {

                            DbHelperMySql.PrepareCommand(cmd, connection, null, sqlString, null);
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                var name = reader[0].ToString().ToLower();
                                var type = ConvertToCSType(reader[1].ToString());

                                dict.Add(name.ToLower(), type);


                            }

                        }
                    }
                    return dict;
                });
            }
            catch (Exception ex)
            {
                throw new UtilCrashException("取表结构异常");
                //throw new UtilCrashException("取表结构异常" + " sql=" + sqlString + " error=" + ex.Message + " conn=" + _connectionString, ex);
            }
            return dict;
        }

    }
}
