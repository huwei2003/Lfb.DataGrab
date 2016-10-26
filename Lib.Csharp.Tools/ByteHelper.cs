using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// byte处理类
    /// </summary>
    public static class ByteHelper
    {
        public static string Md5(this byte[] bytes, bool toUpper = true)
        {
            if (bytes == null)
            {
                return null;
            }
            bytes = MD5.Create().ComputeHash(bytes);
            var md5 = ToHexString(bytes);
            return toUpper ? md5.ToUpper() : md5;
        }

        /// <summary>
        /// 数据库timestamp转化为long类型
        /// </summary>
        public static long TimestampToLong(this byte[] bytes)
        {
            return BitConverter.ToInt64(bytes.Reverse().ToArray(), 0);
        }

        /// <summary>
        /// 把byte[]转成base64编码
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToBase64String(this byte[] bytes)
        {
            var base64Str = Convert.ToBase64String(bytes);
            return base64Str;
        }

        /// <summary>
        /// 从base64字符转byte[]
        /// </summary>
        /// <param name="base64Str"></param>
        /// <returns></returns>
        public static byte[] Base64StringToByte(this string base64Str)
        {
            var myByte = Convert.FromBase64String(base64Str);
            return myByte;
        }

        /// <summary>
        /// 16进制字符串 转 byte[]
        /// </summary>
        public static byte[] ToBytes(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
        /// <summary>
        /// byte[] 转 16进制字符串
        /// </summary>
        public static string ToHexString(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
        ///// <summary>
        ///// 数据库timestamp转化为string类型
        ///// </summary>
        //public static string TimestampToString(this byte[] bytes)
        //{
        //    return "0x" + bytes.ToHexString();
        //}
    }
}
