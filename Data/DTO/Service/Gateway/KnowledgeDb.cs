using System.Collections.Generic;

namespace Comm.Global.DTO.Service.Gateway
{
    /// <summary>
    /// 知识库
    /// </summary>
    public class KnowledgeDb
    {
        /// <summary>
        /// 当前知识库信息
        /// </summary>
        public KnowledgeInfo KnowledgeInfo;

        /// <summary>
        /// 需要序列化的所有实体/枚举
        /// </summary>
        public Dictionary<string, object> KnowledgeDictionary;
    }
}