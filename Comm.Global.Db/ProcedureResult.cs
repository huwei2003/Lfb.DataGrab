using System.Data;

namespace Comm.Global.Db
{
    /// <summary>
    /// 存储过程执行返回结果
    /// </summary>
    public class ProcedureResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int Rslt;

        /// <summary>
        /// 返回的字符串信息
        /// </summary>
        public string Msg;

        /// <summary>
        /// 返回的数据
        /// </summary>
        public DataSet Ds;
        public ProcedureResult()
        {
        }

    }

}
