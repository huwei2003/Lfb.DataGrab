namespace Comm.Global.Enum.Nature
{


    /// <summary>
    /// ö�ٰ�����
    /// </summary>
    public static class BoolHelper
    {
        /// <summary>
        /// ö��תΪbool
        /// </summary>
        /// <param name="bool"></param>
        /// <returns></returns>
        public static bool ToBool(this Bool @bool)
        {
            return @bool != Bool.��;
        }

        /// <summary>
        /// boolתΪEnumBool
        /// </summary>
        /// <param name="boolean"></param>
        /// <returns></returns>
        public static Bool ToEnumBool(this bool boolean)
        {
            return boolean ? Bool.�� : Bool.��;
        }

     }
}
