using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Lib.Csharp.Tools.Security
{
    /// <summary>
    /// 加解密帮组类
    /// </summary>
    public class StringSecurityHelper
    {
        #region 字符串 Des 加解密

        /// <summary>
        /// 字符串加密 Des
        /// </summary>
        /// <param name="strText">要加密的字符串</param>
        /// <param name="strEncrKey">密钥</param>
        /// <returns>返回加密后的字符串</returns>
        public static string DesEncrypt(string strText, string strEncrKey)
        {

            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                var byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, strEncrKey.Length));
                var des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(byKey, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception error)
            {
                return "error:" + error.Message + "\r";
            }
        }

        /// <summary>
        /// 字符串解密 Des
        /// </summary>
        /// <param name="strText">要解密的字符串</param>
        /// <param name="sDecrKey">密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static string DesDecrypt(string strText, string sDecrKey)
        {
            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            try
            {
                var byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                var inputByteArray = Convert.FromBase64String(strText);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(byKey, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = new UTF8Encoding();
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception error)
            {
                return "error:" + error.Message + "\r";
            }

        }

        #endregion

        #region === 3des ===

        /// <summary>
        /// 3DES 加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key">密钥（长度24字符）</param>
        /// <param name="coding">Encoding 编码格式</param>
        /// <returns></returns>
        public static string Des3Encrypt(string input, string key, Encoding coding = null)
        {
            try
            {
                if (coding == null)
                {
                    coding = Encoding.UTF8;
                }
                var data = coding.GetBytes(input);
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = coding.GetBytes(key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                var cryp = des.CreateEncryptor();
                return Convert.ToBase64String(cryp.TransformFinalBlock(data, 0, data.Length));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 3DES 解密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key">密钥（长度24字符）</param>
        /// <param name="coding">Encoding 编码格式</param>
        /// <returns></returns>
        public static string Des3Decrypt(string input, string key, Encoding coding = null)
        {
            try
            {
                if (coding == null)
                {
                    coding = Encoding.UTF8;
                }
                var data = Convert.FromBase64String(input);
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = coding.GetBytes(key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                var cryp = des.CreateDecryptor();
                return coding.GetString(cryp.TransformFinalBlock(data, 0, data.Length));
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 3DES 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">密钥（长度24字符）</param>
        /// <param name="coding">Encoding 编码格式</param>
        /// <returns></returns>
        public static byte[] Des3Encrypt(byte[] data, string key, Encoding coding = null)
        {
            try
            {
                if (coding == null)
                {
                    coding = Encoding.UTF8;
                }
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = coding.GetBytes(key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                var cryp = des.CreateEncryptor();
                return cryp.TransformFinalBlock(data, 0, data.Length);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 3DES 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">密钥（长度24字符）</param>
        /// <param name="coding">Encoding 编码格式</param>
        /// <returns></returns>
        public static byte[] Des3Decrypt(byte[] data, string key, Encoding coding = null)
        {
            try
            {
                if (coding == null)
                {
                    coding = Encoding.UTF8;
                }
                var str1 = coding.GetString(data);

                var str = Des3Decrypt(str1,key);
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = coding.GetBytes(key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                var cryp = des.CreateDecryptor();
                return cryp.TransformFinalBlock(data, 0, data.Length);
            }
            catch (Exception e)
            {
                var str = e.Message;
                return null;
            }
        }
        #endregion

        #region MD5加密

        public static readonly Random Rnd = new Random();

        public static string Md5( string s, Encoding e, bool toUper)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            var buffer = MD5.Create().ComputeHash(e.GetBytes(s));
            var builder = new StringBuilder();
            foreach (var b in buffer)
            {
                builder.Append(b.ToString("x2"));
            }
            return toUper ? builder.ToString().ToUpper() : builder.ToString();
        }

        public static string Md5( string s, bool toUper = true)
        {
            return Md5(s,null, toUper);
        }

        public static string Md5( string s, Encoding e)
        {
            return Md5(s,e, true);
        }

        public static string Md5( string s, int len)
        {
            var md5 = Md5(s);
            switch (len)
            {
                case 16:
                    return md5.Substring(8, 16);
                case 24:
                    return md5.Substring(4, 24);
            }
            return md5;
        }

        #endregion

        #region === HmacSign ===

        public static string HmacSign( string aValue, string aKey)
        {
            var kIpad = new byte[64];
            var kOpad = new byte[64];
            var keyb = Encoding.UTF8.GetBytes(aKey);
            var value = Encoding.UTF8.GetBytes(aValue);
            for (var i = keyb.Length; i < 64; i++)
                kIpad[i] = 54;

            for (var i = keyb.Length; i < 64; i++)
                kOpad[i] = 92;

            for (var i = 0; i < keyb.Length; i++)
            {
                kIpad[i] = (byte)(keyb[i] ^ 0x36);
                kOpad[i] = (byte)(keyb[i] ^ 0x5c);
            }

            var md = new HmacMD5();
            md.update(kIpad, (uint)kIpad.Length);
            md.update(value, (uint)value.Length);
            var dg = md.finalize();
            md.init();
            md.update(kOpad, (uint)kOpad.Length);
            md.update(dg, 16);
            dg = md.finalize();
            if (dg == null) return null;
            var output = new StringBuilder(dg.Length * 2);
            for (var i = 0; i < dg.Length; i++)
            {
                var current = dg[i] & 0xff;
                if (current < 16)
                    output.Append("0");
                output.Append(current.ToString("x"));
            }
            return output.ToString();
        }
        #endregion

        #region === 字节数组加解密 ===


        /// <summary>
        /// 对字节数组加密（DEA Data Encryption Algorithm）
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <param name="keyBytes">密钥keyBytes</param>
        /// <returns>加密后的字节数组</returns>
        public static byte[] DeaEncrypt(string input, byte[] keyBytes)
        {
            return DeaEncrypt(Encoding.UTF8.GetBytes(input),(keyBytes));
            //return Encoding.UTF8.GetBytes(input).DeaEncrypt(keyBytes);
        }

        /// <summary>
        /// 对字节数组加密（DEA Data Encryption Algorithm）
        /// </summary>
        /// <param name="input">流</param>
        /// <param name="keyBytes">密钥keyBytes</param>
        /// <returns>加密后的字节数组</returns>
        public static byte[] DeaEncrypt(Stream input, byte[] keyBytes)
        {
            return DeaEncrypt(StreamHelper.ToBytes(input),keyBytes);
            //return input.ToBytes().DeaEncrypt(keyBytes);
        }

        /// <summary>
        /// 字节数组加密对应的解密（DEA Data Encryption Algorithm）
        /// </summary>
        /// <param name="input">流</param>
        /// <param name="keyBytes">密钥keyBytes</param>
        /// <returns>加密后的字节数组</returns>
        public static string DeaDecrypt(Stream input, byte[] keyBytes)
        {
            return DeaDecrypt(StreamHelper.ToBytes(input), keyBytes);
            //return input.ToBytes().DeaDecrypt(keyBytes);
        }

        /// <summary>
        /// 对字节数组加密（DEA Data Encryption Algorithm）
        /// </summary>
        /// <param name="data">需要加密的字节数组</param>
        /// <param name="keyBytes">密钥keyBytes</param>
        /// <returns>加密后的字节数组</returns>
        public static byte[] DeaEncrypt(byte[] data, byte[] keyBytes)
        {
            if (data == null || data.Length == 0 || keyBytes == null || keyBytes.Length != 32)
            {
                return null;
            }
            var keyLength = 16;
            if (data.Length < keyLength)
            {
                keyLength = data.Length;
            }
            var keyRnd = new int[keyLength];//随机取keyLength个byte
            var result = new byte[keyLength + data.Length];//加密后字节长度
            for (var i = 0; i < keyLength; i++)
            {
                result[i] = (byte)Rnd.Next(0, 256);
                keyRnd[i] = keyBytes[result[i] % keyBytes.Length] % 8;
                if (keyRnd[i] == 0) keyRnd[i] = 1;
            }
            for (var i = 0; i < data.Length; i++)
            {
                var c = keyRnd[i % keyLength];
                result[i + keyLength] = (byte)((data[i] << c) | (data[i] >> (8 - c)));//对字节进行运算
            }
            return result;
        }

        /// <summary>
        /// 字节数组加密对应的解密（DEA Data Encryption Algorithm）
        /// </summary>
        /// <param name="data">加密后的字节数组</param>
        /// <param name="keyBytes">密钥keyBytes</param>
        /// <returns>解密后的字符串</returns>
        public static string DeaDecrypt(byte[] data, byte[] keyBytes)
        {
            if (data == null || data.Length < 2 || keyBytes == null || keyBytes.Length != 32)
            {
                return "";
            }
            var keyLength = 16;
            if (data.Length < keyLength * 2)
            {
                keyLength = data.Length / 2;
            }
            var keyRnd = new int[keyLength];
            for (var i = 0; i < keyLength; i++)
            {
                keyRnd[i] = keyBytes[data[i] % keyBytes.Length] % 8;
                if (keyRnd[i] == 0) keyRnd[i] = 1;
            }
            for (var i = keyLength; i < data.Length; i++)
            {
                var c = keyRnd[i % keyLength];
                data[i] = (byte)((data[i] >> c) | (data[i] << (8 - c)));//对字节进行运算
            }
            return Encoding.UTF8.GetString(data, keyLength, data.Length - keyLength);
        }
        #endregion

        #region CRC校验
        /// <summary>
        /// CRC高位校验码checkCRCHigh
        /// </summary>
        private static readonly byte[] ArrayCrcHigh =
        {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40
        };

        /// <summary>
        /// CRC地位校验码checkCRCLow
        /// </summary>
        private static readonly byte[] CheckCrcLow =
        {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06,
            0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD,
            0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A,
            0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14, 0xD4,
            0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3,
            0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4,
            0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29,
            0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED,
            0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60,
            0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67,
            0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68,
            0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA, 0xBE, 0x7E,
            0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71,
            0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92,
            0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B,
            0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B,
            0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42,
            0x43, 0x83, 0x41, 0x81, 0x80, 0x40
        };
        /// <summary>
        /// 循环冗余CRC16校验数据正确性
        /// </summary>
        /// <param name="data">校验的字节数组</param>
        /// <param name="index">起始字节索引</param>
        /// <param name="count">校验的字节数</param>
        /// <returns>该字节数组的校验结果字节</returns>
        public static byte[] Crc16(byte[] data, int index = 0, int count = 0)
        {
            if (count == 0)
            {
                count = data.Length;
            }
            byte crcHigh = 0xFF;
            byte crcLow = 0xFF;
            var length = index + count;
            var i = index;
            while (length-- > index)
            {
                var j = (byte)(crcHigh ^ data[i++]);
                crcHigh = (byte)(crcLow ^ ArrayCrcHigh[j]);
                crcLow = CheckCrcLow[j];
            }
            return new[] { crcHigh, crcLow };
        }
        #endregion
    }
}
