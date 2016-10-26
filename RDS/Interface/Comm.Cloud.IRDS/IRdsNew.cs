using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Comm.Global.Db;

namespace Comm.Cloud.IRDS
{
    /// <summary>
    /// 基本存储云服务
    /// 所有字段返回全部为小写!
    /// </summary>
    public interface IRdsNew
    {
        /// <summary>
        /// 创建一条记录 返回自增id
        /// </summary>
        /// <typeparam name="T">实体表类型</typeparam>
        /// <param name="table">实体表的基类</param>
        /// <param name="condition"></param>
        /// <param name="idFieldName">自增字段名,默认为Id</param>
        /// <returns></returns>
        Task<int> InsertIdAsync<T>(Table<T> table, string condition = null,string idFieldName="Id");

        /// <summary>
        /// 创建一条记录 返回成功与否
        /// </summary>
        /// <typeparam name="T">实体表类型</typeparam>
        /// <param name="table">实体表的基类</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        Task<bool> InsertAsync<T>(Table<T> table, string condition = null);

        /// <summary>
        /// 根据条件判断是新增还是更新，返回影响记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> InsertOrUpdateAsync<T>(Table<T> table, string condition = null);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> UpdateAsync<T>(Table<T> table, string condition = null);

        /// <summary>
        /// 更新部分自段，自增或自减，
        /// 只适合 update table set field=field+N where id=1 的情况
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">字段名，值列表</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        Task<int> UpdateFieldStepAsync(string tableName, Dictionary<string, object> fields, string condition = null);

        /// <summary>
        /// 删除指定记录
        /// 返回false表示无满足sqlWhere的记录(404)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="parameters"></param>
        /// <returns>无</returns>
        Task<bool> DeleteAsync(string table, string sqlWhere, List<object> parameters = null);

        /// <summary>
        /// 返回记录数量
        /// </summary>
        /// <returns></returns>
        Task<long> GetCountAsync(string table, string sqlWhere);

        /// <summary>
        /// 检查记录是否存在，无记录或异常返回flase
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="sqlWhere">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns>记录是否存在</returns>
        Task<bool> IsExistAsync(string tableName, string sqlWhere, params object[] parameters);

        /// <summary>
        /// 查询表对象中的一条记录，一般用于id= 或其它字段作条件取某个记录的情况
        /// </summary>
        /// <param name="whereFields">where条件的字段</param>
        /// <param name="parameters">参数值,建议采用parameters参数,parameters防Sql注入</param>
        /// <param name="tableName">表名</param>
        /// <returns>表对象,只绑定选择的字段值</returns>
        Task<T> GetSingleAsync<T>(string tableName, List<string> whereFields, params object[] parameters);

        /// <summary>
        /// 获取表所有数据
        /// </summary>
        /// <typeparam name="T">表类型</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="sqlWhere">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<List<T>> GetListAllAsync<T>(string tableName,string sqlWhere, params object[] parameters);

        /// <summary>
        /// 无顺序要求获取列表,字段返回全部为小写!
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<Dictionary<string, object>>> GetDictionaryAllAsync(string sql);


        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="qrySql">sql,必须包含order by 部分</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="parameters">参数 sql语句中的参数 ?号部分</param>
        /// <returns></returns>
        Task<Global.Db.PagedResult<T>> SelectPageAsync<T>(string qrySql, int pageIndex, int pageSize, params object[] parameters);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qrySql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<T>> SelectAsync<T>(string qrySql, params object[] parameters);

        /// <summary>
        /// 执行一个sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<bool> ExecuteSqlAsync(string sql);

        /// <summary>
        /// 执行sql,得到一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<T> ExecuteScalarAsync<T>(T defaultValue, string strSql, params object[] parameters);

        /// <summary>
        /// 在事务内执行一组操作
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        Task<bool> TransactionAsync(IList<string> sqls);


        /// <summary>
        /// 求某列名的最小值
        /// </summary>
        /// <typeparam name="TR"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        Task<TR> MinAsync<TR>(string tableName, string columnName, TR defaultValue = default(TR));
        
        /// <summary>
        /// 求某列名的最大值
        /// </summary>
        /// <typeparam name="TR"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        Task<TR> MaxAsync<TR>(string tableName, string columnName, TR defaultValue = default (TR));


        Task<ProcedureResult> ExecuteProcedureAsync(string strSql, int retPosition,params object[] parameters);

        /// <summary>
        /// 得到表结构,字段返回全部为小写!
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<Dictionary<string, Type>> GetTableStructAsync(string table);


        /// <summary>
        /// 清空多个表
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        Task<bool> TruncateTablesAsync(IEnumerable<string> tables);

        /// <summary>
        /// 批量删除多个表
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        Task<bool> DropTablesAsync(IEnumerable<string> tables);

        /// <summary>
        /// 建立多个表
        /// </summary>
        /// <param name="tableDefines"></param>
        /// <returns></returns>
        Task<bool> CreateTablesAsync(IEnumerable<DbTableDefine> tableDefines);

        /// <summary>
        /// 建立多个表间外键
        /// </summary>
        /// <param name="foreignKeyDefines"></param>
        /// <returns></returns>
        Task<bool> CreateTableForeignKeysAsync(IEnumerable<DbForeignKeyDefine> foreignKeyDefines);

       
    }
}
