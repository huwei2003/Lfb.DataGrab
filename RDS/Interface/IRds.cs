using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comm.Cloud.IRDS
{
    /// <summary>
    /// 基本存储云服务
    /// 所有字段返回全部为小写!
    /// </summary>
    public interface IRds
    {
        
        /// <summary>
        /// 返回记录数量
        /// </summary>
        /// <returns></returns>
        Task<long> GetCountAsync(string table, string sqlWhere);

        /// <summary>
        /// 检查记录是否存在，无记录或异常返回flase
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sqlWhere">条件</param>
        /// <returns>记录是否存在</returns>
        Task<bool> IsExistAsync(string table, string sqlWhere);
       
        
        /// <summary>
        /// 无顺序要求获取列表,字段返回全部为小写!
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<Dictionary<string, object>>> GetAllAsync(string sql);
     
        
        /// <summary>
        /// 执行一个sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<bool> RunSqlAsync(string sql);

        /// <summary>
        /// 在事务内执行一组操作
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        Task<bool> TransactionAsync(IList<string> sqls);
        
        /// <summary>
        /// 创建一条记录
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields">实体字典,忽略字段名大小写</param>
        /// <returns></returns>
        Task<bool> CreateAsync(string table, IDictionary<string, object> fields);

        /// <summary>
        /// 部分修改一条记录
        /// 返回false表示无满足sqlWhere的记录(404)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sqlWhere">Id=xx</param>
        /// <param name="fields">需要修改的字段集合,忽略字段名大小写</param>
        /// <returns></returns>
        Task<bool> PatchAsync(string table, string sqlWhere, IDictionary<string, object> fields);

        /// <summary>
        /// 删除指定记录
        /// 返回false表示无满足sqlWhere的记录(404)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sqlWhere"></param>
        /// <returns>无</returns>
        Task<bool> DeleteAsync(string table,string sqlWhere);

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
