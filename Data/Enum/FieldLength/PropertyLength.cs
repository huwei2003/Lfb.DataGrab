namespace Comm.Global.Enum.FieldLength
{
    /// <summary>
    /// 数据库中特殊字段默认存储长度
    /// 按属性区分
    /// </summary>
    public static class PropertyLength
    {
        /// <summary>
        /// 长名字
        /// </summary>
        public const int LongName = 16;

        /// <summary>
        /// 密码,16字节->32个16进制数
        /// </summary>
        public const int Password = 32;

        /// <summary>
        /// 手机
        /// </summary>
        public const int Mobile = 11;

        /// <summary>
        /// url链接长度
        /// </summary>
        public const int Url = 256;

        /// <summary>
        /// 位置名
        /// </summary>
        public const int LocationName = 16;

        /// <summary>
        /// 位置名
        /// </summary>
        public const int ChatPileName = 4;
    }
}
