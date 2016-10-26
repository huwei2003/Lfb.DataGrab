namespace Comm.Global.Enum.Nature
{


    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class BoolHelper
    {
        /// <summary>
        /// 枚举转为bool
        /// </summary>
        /// <param name="bool"></param>
        /// <returns></returns>
        public static bool ToBool(this Bool @bool)
        {
            return @bool != Bool.否;
        }

        /// <summary>
        /// bool转为EnumBool
        /// </summary>
        /// <param name="boolean"></param>
        /// <returns></returns>
        public static Bool ToEnumBool(this bool boolean)
        {
            return boolean ? Bool.是 : Bool.否;
        }

     }
}
