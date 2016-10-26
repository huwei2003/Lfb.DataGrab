using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Comm.Cloud.IRDS;
using Comm.Cloud.RDS.DTO;
using Comm.Global.Config;
using Comm.Global.Db;
using Comm.Global.Enum.Sys;

namespace Comm.Cloud.RDS
{
    public class RdsNew : IRdsNew
    {
        //private static readonly ILogger Logger = LoggerFactory.GetInstanse(typeof(RdsNew));
        private string _connectionString;

        private PublicCloudRdsConfig _config;

        private DbBase db;

        /// <summary>
        /// 每个微服务一个rds云实例/或共享云实例中的一个数据库
        /// </summary>
        /// <param name="hostServiceName">宿主微服务</param>
        public RdsNew(ServiceName hostServiceName)
        {
            var config = ServiceConfigHelper.GlobalServiceConfig.
                GetServiceFromBuffer<PublicCloudRdsConfig>(
                ServiceName.Cloud_RdsService, hostServiceName);
            Init(config);

        }
        public RdsNew(ServiceName hostServiceName, DbType dbType)
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
        public RdsNew(PublicCloudRdsConfig config)
        {
            Init(config);
        }
        /// <summary>
        /// 只用于集成测试构造
        /// </summary>
        /// <param name="config"></param>
        /// <param name="dbType"></param>
        public RdsNew(PublicCloudRdsConfig config, DbType dbType)
        {
            Init(config, dbType);
        }
        private void Init(PublicCloudRdsConfig config)
        {
            _config = config;
            _connectionString = config.GetDbConnectString();

        }
        private void Init(PublicCloudRdsConfig config, DbType dbType)
        {
            _config = config;
            //_connectionString = config.GetDbConnectString();
            switch (dbType)
            {
                case DbType.SqlServer:
                    db = new DbSqlServer(config.GetSqlServerConnectString())
                    {
                        DbName = _config.Database
                    };
                    break;
                case DbType.MySql:
                    db = new DbMySql(config.GetMySqlConnectString())
                    {
                        DbName = _config.Database
                    };
                    break;
                case DbType.Oracle:
                    //db = new DbOracleServer(_connectionString);
                    break;
            }
        }


        public async Task<int> InsertIdAsync<T>(Table<T> table, string idFieldName = "Id", string condition = null)
        {
            var ret = await Task.Run(() => db.InsertId<T>(table, condition, idFieldName));
            return ret;
        }

        public async Task<bool> InsertAsync<T>(Table<T> table, string condition = null)
        {
            var ret = await Task.Run(() => db.Insert<T>(table, condition));
            return ret;
        }

        public async Task<int> InsertOrUpdateAsync<T>(Table<T> table, string condition = null)
        {
            var ret = await Task.Run(() => db.InsertOrUpdate<T>(table, condition));
            return ret;
        }

        public async Task<int> UpdateAsync<T>(Table<T> table, string condition = null)
        {
            var ret = await Task.Run(() => db.Update<T>(table, condition));
            return ret;
        }

        public async Task<int> UpdateFieldStepAsync(string tableName, Dictionary<string, object> fields, string condition = null)
        {
            var ret = await Task.Run(() => db.UpdateFieldStep(tableName, fields, condition));
            return ret;
        }

        public async Task<bool> DeleteAsync(string tableName, string sqlWhere, List<object> parameters = null)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.Delete(tableName, sqlWhere, parameters);
                return obj;
            });
            return ret > 0;
        }

        public async Task<T> GetSingleAsync<T>(string tableName, List<string> whereFields, params object[] parameters)
        {
            return await Task.Run(() =>
            {
                var obj = db.GetSingle<T>(tableName, whereFields, parameters);
                return obj;
            });
        }

        public async Task<long> GetCountAsync(string tableName, string sqlWhere)
        {
            return await Task.Run(() =>
            {
                var obj = db.GetCount(tableName, sqlWhere);
                return obj;

            });
        }

        public async Task<bool> IsExistAsync(string tableName, string sqlWhere, params object[] parameters)
        {

            var ret = await Task.Run(() =>
            {
                var obj = db.Exists(tableName, sqlWhere, parameters);
                return obj;
            });
            return ret;
        }

        /// <summary>
        /// 返回表中所有数据
        /// </summary>
        public async Task<List<T>> GetListAllAsync<T>(string tableName, string sqlWhere, params object[] parameters)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.GetDataAll<T>(tableName, sqlWhere, parameters);
                return obj;
            });

            return ret;
        }

        public async Task<List<Dictionary<string, object>>> GetDictionaryAllAsync(string strSql)
        {
            return await Task.Run(() =>
            {
                var obj = db.GetDictionarys(strSql, null);
                return obj;
            });
        }


        public async Task<TR> MinAsync<TR>(string tableName, string columnName, TR defaultValue = default (TR))
        {
            //var sql = "SELECT MIN([{0}]) FROM {1}".Formats(columnName, typeof(T).Name);
            //return SqlExecute.ExecuteScalar(defaultValue, sql);
            var ret = await Task.Run(() =>
            {
                var obj = db.Min<TR>(tableName, columnName, defaultValue);
                return obj;
            });
            return ret;
        }

        /// <summary>
        /// 列最大值
        /// </summary>
        public async Task<TR> MaxAsync<TR>(string tableName, string columnName, TR defaultValue = default (TR))
        {
            //var sql = "SELECT MAX([{0}]) FROM {1}".Formats(columnName, typeof(T).Name);
            //return SqlExecute.ExecuteScalar(defaultValue, sql);
            var ret = await Task.Run(() =>
            {
                var obj = db.Max<TR>(tableName, columnName, defaultValue);
                return obj;
            });
            return ret;
        }

        public async Task<bool> ExecuteSqlAsync(string strSql)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.ExecuteNonQuery(strSql);
                return obj;
            });
            return ret > 0;
        }

        public async Task<T> ExecuteScalarAsync<T>(T defaultValue, string strSql, params object[] parameters)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.ExecuteScalar<T>(defaultValue, strSql, parameters);
                return obj;
            });
            return ret;
        }


        public async Task<PagedResult<T>> SelectPageAsync<T>(string qrySql, int pageIndex, int pageSize,
            params object[] parameters)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.SelectPage<T>(qrySql, pageSize, pageIndex, parameters);
                return obj;
            });
            return ret;
        }

        public async Task<List<T>> SelectAsync<T>(string qrySql, params object[] parameters)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.Select<T>(qrySql, parameters);
                return obj;
            });
            return ret;
        }

        public async Task<bool> TransactionAsync(IList<string> strSqls)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.ExecuteSqlTrans(strSqls);
                return obj;
            });
            return ret;
        }

        public async Task<ProcedureResult> ExecuteProcedureAsync(string strSql, int retPosition, params object[] parameters)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.ExecuteProcedure(strSql, retPosition, parameters);
                return obj;
            });
            return ret;
        }

        public async Task<bool> TruncateTablesAsync(IEnumerable<string> tables)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.TruncateTables(tables);
                return obj;
            });

            return false;
        }

        public async Task<bool> DropTablesAsync(IEnumerable<string> tables)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.DropTables(tables);
                return obj;
            });

            return false;
        }

        public async Task<bool> CreateTablesAsync(IEnumerable<DbTableDefine> tableDefines)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.CreateTables(tableDefines);
                return obj;
            });

            return false;

        }

        public async Task<bool> CreateTableForeignKeysAsync(IEnumerable<DbForeignKeyDefine> foreignKeyDefines)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.CreateTableForeignKeys(foreignKeyDefines);
                return obj;
            });

            return false;

        }

        public async Task<Dictionary<string, Type>> GetTableStructAsync(string tableName)
        {
            var ret = await Task.Run(() =>
            {
                var obj = db.GetTableStruct(tableName, _config.Database);
                return obj;
            });

            return ret;
        }


        #region === sync ===

        public int InsertId<T>(Table<T> table, string idFieldName = "Id", string condition = null)
        {
            var ret = db.InsertId<T>(table, condition, idFieldName);
            return ret;
        }

        public bool Insert<T>(Table<T> table, string condition = null)
        {
            var ret = db.Insert<T>(table, condition);
            return ret;
        }

        public int InsertOrUpdate<T>(Table<T> table, string condition = null)
        {
            var ret = db.InsertOrUpdate<T>(table, condition);
            return ret;
        }

        public int Update<T>(Table<T> table, string condition = null)
        {
            var ret = db.Update<T>(table, condition);
            return ret;
        }

        public int UpdateFieldStep(string tableName, Dictionary<string, object> fields, string condition = null)
        {
            var ret = db.UpdateFieldStep(tableName, fields, condition);
            return ret;
        }

        public bool Delete(string tableName, string sqlWhere, List<object> parameters = null)
        {

            var obj = db.Delete(tableName, sqlWhere, parameters);
            return obj > 0;

        }

        public T GetSingle<T>(string tableName, List<string> whereFields, params object[] parameters)
        {

            var obj = db.GetSingle<T>(tableName, whereFields, parameters);
            return obj;
        }

        public long GetCount(string tableName, string sqlWhere)
        {

            var obj = db.GetCount(tableName, sqlWhere);
            return obj;

        }

        public bool IsExist(string tableName, string sqlWhere, params object[] parameters)
        {

            var obj = db.Exists(tableName, sqlWhere, parameters);
            return obj;

        }

        /// <summary>
        /// 返回表中所有数据
        /// </summary>
        public List<T> GetListAll<T>(string tableName, string sqlWhere, params object[] parameters)
        {

            var obj = db.GetDataAll<T>(tableName, sqlWhere, parameters);
            return obj;

        }

        public List<Dictionary<string, object>> GetDictionaryAll(string strSql)
        {

            var obj = db.GetDictionarys(strSql, null);
            return obj;

        }


        public TR Min<TR>(string tableName, string columnName, TR defaultValue = default (TR))
        {

            var obj = db.Min<TR>(tableName, columnName, defaultValue);
            return obj;

        }

        /// <summary>
        /// 列最大值
        /// </summary>
        public TR Max<TR>(string tableName, string columnName, TR defaultValue = default (TR))
        {

            var obj = db.Max<TR>(tableName, columnName, defaultValue);
            return obj;

        }

        public bool ExecuteSql(string strSql)
        {

            var obj = db.ExecuteNonQuery(strSql);
            return obj > 0;

        }

        public T ExecuteScalar<T>(T defaultValue, string strSql, params object[] parameters)
        {
            var obj = db.ExecuteScalar<T>(defaultValue, strSql, parameters);
            return obj;
        }

        public PagedResult<T> SelectPage<T>(string qrySql, int pageIndex, int pageSize,
            params object[] parameters)
        {

            var obj = db.SelectPage<T>(qrySql, pageSize, pageIndex, parameters);
            return obj;

        }

        public List<T> Select<T>(string qrySql,params object[] parameters)
        {
            var obj = db.Select<T>(qrySql, parameters);
            return obj;
        }

        public bool Transaction(IList<string> strSqls)
        {

            var obj = db.ExecuteSqlTrans(strSqls);
            return obj;

        }

        public ProcedureResult ExecuteProcedure(string strSql, int retPosition, params object[] parameters)
        {

            var obj = db.ExecuteProcedure(strSql, retPosition, parameters);
            return obj;

        }

        public bool TruncateTables(IEnumerable<string> tables)
        {

            var obj = db.TruncateTables(tables);
            return obj;

        }

        public bool DropTables(IEnumerable<string> tables)
        {
            var obj = db.DropTables(tables);
            return obj;
        }

        public bool CreateTables(IEnumerable<DbTableDefine> tableDefines)
        {

            var obj = db.CreateTables(tableDefines);
            return obj;


        }

        public bool CreateTableForeignKeys(IEnumerable<DbForeignKeyDefine> foreignKeyDefines)
        {

            var obj = db.CreateTableForeignKeys(foreignKeyDefines);
            return obj;


        }

        public Dictionary<string, Type> GetTableStruct(string tableName)
        {
            var obj = db.GetTableStruct(tableName, _config.Database);
            return obj;
        }
        #endregion
    }
}
