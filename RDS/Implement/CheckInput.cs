namespace Comm.Cloud.RDS
{
    public class CheckInput
    {
        /// <summary>
        /// 检测输入参数 是否有效
        /// </summary>
        /// <param name="inputstr">原字符串</param>
        /// <param name="Maxlength">最大长度</param>
        /// <param name="bylength">是否检查超最大长度</param>
        /// <returns>true 有效 flase 无效</returns>
        public static bool IsValidInput(string inputstr, int Maxlength, string bylength)
        {
            var isvalid = true;
            if (bylength == "1")
            {
                if (inputstr.Length > Maxlength)
                {
                    isvalid = false;
                }
            }
            //正常的sql语句的关键字在前面，去掉后如果还有则有可能注入
            if (inputstr.Length > 10)
            {
                inputstr = inputstr.Substring(5);
            }
            if (inputstr.ToLower().IndexOf("declare ") > -1)
            {
                isvalid = false;
                return isvalid;
            }
            if (inputstr.ToLower().IndexOf("update ") > -1)
            {
                isvalid = false;
                return isvalid;
            }
            if (inputstr.ToLower().IndexOf("exec ") > -1)
            {
                isvalid = false;
                return isvalid;
            }
            if (inputstr.ToLower().IndexOf("delete ") > -1)
            {
                isvalid = false;
                return isvalid;
            }
            if (inputstr.ToLower().IndexOf("truncate ") > -1)
            {
                isvalid = false;
                return isvalid;
            }
            if (inputstr.ToLower().IndexOf("xp_cmdshell ") > -1)
            {
                isvalid = false;
                return isvalid;
            }
            if (inputstr.ToLower().IndexOf("insert ") > -1)
            {
                isvalid = false;
                return isvalid;
            }
            return isvalid;
        }
    }
}
