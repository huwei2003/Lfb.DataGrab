using System.Security.Cryptography;

namespace Comm.Tools.Utility
{
    public static class ByteHelper
    {
        public static string Md5(this byte[] bytes, bool toUpper = true)
        {
            if (bytes == null)
            {
                return null;
            }
            bytes = MD5.Create().ComputeHash(bytes);
            var md5 = bytes.ToHexString();
            return toUpper ? md5.ToUpper() : md5;
        }

        ///// <summary>
        ///// 数据库timestamp转化为long类型
        ///// </summary>
        //public static long TimestampToLong(this byte[] bytes)
        //{
        //    return BitConverter.ToInt64(bytes.Reverse().ToArray(), 0);
        //}

        /// <summary>
        /// 数据库timestamp转化为string类型
        /// </summary>
        public static string TimestampToString(this byte[] bytes)
        {
            return "0x" + bytes.ToHexString();
        }
    }
}
