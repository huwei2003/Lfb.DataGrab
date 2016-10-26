using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Comm.Tools.Utility
{
    public static class StringSecurity
    {
        public static readonly Random Rnd = new Random();

        public static string Md5(this string s, Encoding e, bool toUper)
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

        public static string Md5(this string s, bool toUper = true)
        {
            return s.Md5(null, toUper);
        }

        public static string Md5(this string s, Encoding e)
        {
            return s.Md5(e, true);
        }

        public static string Md5(this string s, int l)
        {
            var md5 = s.Md5();
            switch (l)
            {
                case 16:
                    return md5.Substring(8, 16);
                case 24:
                    return md5.Substring(4, 24);
            }
            return md5;
        }

        public static string HmacSign(this string aValue, string aKey)
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

        /*/// <summary>
        /// 3DES 加密过程
        /// </summary>
        /// <returns>加密后的字符串</returns>
        public static string Des3Encrypt(this string input, string key)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(input);//如果加密中文，不能用ASCII码 
                var keys = Encoding.ASCII.GetBytes(key.Md5().Substring(4, 24));
                var des = new TripleDESCryptoServiceProvider();
                des.Key = keys;//16位或24位
                des.Mode = CipherMode.ECB;//设置运算模式 
                var cryp = des.CreateEncryptor();//加密 
                return cryp.TransformFinalBlock(data, 0, data.Length).ToHexString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 3DES 解密过程
        /// </summary>
        /// <returns>返回被解密的字符串</returns>
        public static string Des3Decrypt(this string input, string key)
        {
            try
            {
                var data = input.ToBytes();
                var keys = Encoding.ASCII.GetBytes(key.Md5().Substring(4, 24));
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = keys,
                    Mode = CipherMode.ECB,//设置运算模式 
                    Padding = PaddingMode.PKCS7
                };
                var cryp = des.CreateDecryptor();//解密 
                return Encoding.UTF8.GetString(cryp.TransformFinalBlock(data, 0, data.Length));
            }
            catch
            {
                return null;
            }
        }*/


        /// <summary>
        /// 3DES 加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key">密钥（长度24字符）</param>
        /// <param name="coding">Encoding 编码格式</param>
        /// <returns></returns>
        public static string Des3Encrypt(this string input, string key, Encoding coding = null)
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
        public static string Des3Decrypt(this string input, string key, Encoding coding = null)
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
        public static byte[] Des3Encrypt(this byte[] data, string key, Encoding coding = null)
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
        public static byte[] Des3Decrypt(this byte[] data, string key, Encoding coding = null)
        {
            try
            {
                if (coding == null)
                {
                    coding = Encoding.UTF8;
                }
                var str1 = coding.GetString(data);
                var arr = str1.JsonToArray(typeof(string[])).ToArrays();
                var str2 = arr[0];
                var str = str1.Des3Decrypt(key);
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


        /// <summary>
        /// 16进制字符串 转 byte[]
        /// </summary>
        public static byte[] ToBytes(this string hex)
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
        public static string ToHexString(this byte[] bytes)
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

        /// <summary>
        /// 加密密码
        /// </summary>
        public static string EncryptPass(this string input)
        {
            return (input + "7ien.shovesoft.shove 中国深圳 2007-10-25").Md5();
        }

        /// <summary>
        /// 神通征信加密密码
        /// </summary>
        public static string Md5StPass(this string input)
        {
            return (input + "4750A3353CDADFB21F07DC8AE1037758").Md5();
        }

        /// <summary>
        /// 对字节数组加密（DEA Data Encryption Algorithm）
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <param name="keyBytes">密钥keyBytes</param>
        /// <returns>加密后的字节数组</returns>
        public static byte[] DeaEncrypt(this string input, byte[] keyBytes)
        {
            return Encoding.UTF8.GetBytes(input).DeaEncrypt(keyBytes);
        }

        /// <summary>
        /// 对字节数组加密（DEA Data Encryption Algorithm）
        /// </summary>
        /// <param name="input">流</param>
        /// <param name="keyBytes">密钥keyBytes</param>
        /// <returns>加密后的字节数组</returns>
        public static byte[] DeaEncrypt(this Stream input, byte[] keyBytes)
        {
            return input.ToBytes().DeaEncrypt(keyBytes);
        }

        /// <summary>
        /// 字节数组加密对应的解密（DEA Data Encryption Algorithm）
        /// </summary>
        /// <param name="input">流</param>
        /// <param name="keyBytes">密钥keyBytes</param>
        /// <returns>加密后的字节数组</returns>
        public static string DeaDecrypt(this Stream input, byte[] keyBytes)
        {
            return input.ToBytes().DeaDecrypt(keyBytes);
        }

        /// <summary>
        /// 对字节数组加密（DEA Data Encryption Algorithm）
        /// </summary>
        /// <param name="data">需要加密的字节数组</param>
        /// <param name="keyBytes">密钥keyBytes</param>
        /// <returns>加密后的字节数组</returns>
        public static byte[] DeaEncrypt(this byte[] data, byte[] keyBytes)
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
        public static string DeaDecrypt(this byte[] data, byte[] keyBytes)
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
        public static byte[] Crc16(this byte[] data, int index = 0, int count = 0)
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

public class HmacMD5
{
    private uint[] count;
    private uint[] state;
    private byte[] buffer;
    private byte[] Digest;

    public HmacMD5()
    {
        count = new uint[2];
        state = new uint[4];
        buffer = new byte[64];
        Digest = new byte[16];
        init();
    }

    public void init()
    {
        count[0] = 0;
        count[1] = 0;
        state[0] = 0x67452301;
        state[1] = 0xefcdab89;
        state[2] = 0x98badcfe;
        state[3] = 0x10325476;
    }

    public void update(byte[] data, uint length)
    {
        var left = length;
        var offset = (count[0] >> 3) & 0x3F;
        var bit_length = (uint)(length << 3);
        uint index = 0;

        if (length <= 0)
            return;

        count[0] += bit_length;
        count[1] += (length >> 29);
        if (count[0] < bit_length)
            count[1]++;

        if (offset > 0)
        {
            var space = 64 - offset;
            var copy = (offset + length > 64 ? 64 - offset : length);
            Buffer.BlockCopy(data, 0, buffer, (int)offset, (int)copy);

            if (offset + copy < 64)
                return;

            transform(buffer);
            index += copy;
            left -= copy;
        }

        for (; left >= 64; index += 64, left -= 64)
        {
            Buffer.BlockCopy(data, (int)index, buffer, 0, 64);
            transform(buffer);
        }

        if (left > 0)
            Buffer.BlockCopy(data, (int)index, buffer, 0, (int)left);

    }

    private static byte[] pad = new byte[64] { 0x80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public byte[] finalize()
    {
        var bits = new byte[8];
        encode(ref bits, count, 8);
        var index = (uint)((count[0] >> 3) & 0x3f);
        var padLen = (index < 56) ? (56 - index) : (120 - index);
        update(pad, padLen);
        update(bits, 8);
        encode(ref Digest, state, 16);

        for (var i = 0; i < 64; i++)
            buffer[i] = 0;

        return Digest;
    }

    public string md5String()
    {
        var s = "";

        for (var i = 0; i < Digest.Length; i++)
            s += Digest[i].ToString("x2");

        return s;
    }

    #region Constants for MD5Transform routine.

    private const uint S11 = 7;
    private const uint S12 = 12;
    private const uint S13 = 17;
    private const uint S14 = 22;
    private const uint S21 = 5;
    private const uint S22 = 9;
    private const uint S23 = 14;
    private const uint S24 = 20;
    private const uint S31 = 4;
    private const uint S32 = 11;
    private const uint S33 = 16;
    private const uint S34 = 23;
    private const uint S41 = 6;
    private const uint S42 = 10;
    private const uint S43 = 15;
    private const uint S44 = 21;
    #endregion

    private void transform(byte[] data)
    {
        var a = state[0];
        var b = state[1];
        var c = state[2];
        var d = state[3];
        var x = new uint[16];

        decode(ref x, data, 64);

        // Round 1
        FF(ref a, b, c, d, x[0], S11, 0xd76aa478); /* 1 */
        FF(ref d, a, b, c, x[1], S12, 0xe8c7b756); /* 2 */
        FF(ref c, d, a, b, x[2], S13, 0x242070db); /* 3 */
        FF(ref b, c, d, a, x[3], S14, 0xc1bdceee); /* 4 */
        FF(ref a, b, c, d, x[4], S11, 0xf57c0faf); /* 5 */
        FF(ref d, a, b, c, x[5], S12, 0x4787c62a); /* 6 */
        FF(ref c, d, a, b, x[6], S13, 0xa8304613); /* 7 */
        FF(ref b, c, d, a, x[7], S14, 0xfd469501); /* 8 */
        FF(ref a, b, c, d, x[8], S11, 0x698098d8); /* 9 */
        FF(ref d, a, b, c, x[9], S12, 0x8b44f7af); /* 10 */
        FF(ref c, d, a, b, x[10], S13, 0xffff5bb1); /* 11 */
        FF(ref b, c, d, a, x[11], S14, 0x895cd7be); /* 12 */
        FF(ref a, b, c, d, x[12], S11, 0x6b901122); /* 13 */
        FF(ref d, a, b, c, x[13], S12, 0xfd987193); /* 14 */
        FF(ref c, d, a, b, x[14], S13, 0xa679438e); /* 15 */
        FF(ref b, c, d, a, x[15], S14, 0x49b40821); /* 16 */

        // Round 2 
        GG(ref a, b, c, d, x[1], S21, 0xf61e2562); /* 17 */
        GG(ref d, a, b, c, x[6], S22, 0xc040b340); /* 18 */
        GG(ref c, d, a, b, x[11], S23, 0x265e5a51); /* 19 */
        GG(ref b, c, d, a, x[0], S24, 0xe9b6c7aa); /* 20 */
        GG(ref a, b, c, d, x[5], S21, 0xd62f105d); /* 21 */
        GG(ref d, a, b, c, x[10], S22, 0x2441453); /* 22 */
        GG(ref c, d, a, b, x[15], S23, 0xd8a1e681); /* 23 */
        GG(ref b, c, d, a, x[4], S24, 0xe7d3fbc8); /* 24 */
        GG(ref a, b, c, d, x[9], S21, 0x21e1cde6); /* 25 */
        GG(ref d, a, b, c, x[14], S22, 0xc33707d6); /* 26 */
        GG(ref c, d, a, b, x[3], S23, 0xf4d50d87); /* 27 */
        GG(ref b, c, d, a, x[8], S24, 0x455a14ed); /* 28 */
        GG(ref a, b, c, d, x[13], S21, 0xa9e3e905); /* 29 */
        GG(ref d, a, b, c, x[2], S22, 0xfcefa3f8); /* 30 */
        GG(ref c, d, a, b, x[7], S23, 0x676f02d9); /* 31 */
        GG(ref b, c, d, a, x[12], S24, 0x8d2a4c8a); /* 32 */

        // Round 3
        HH(ref a, b, c, d, x[5], S31, 0xfffa3942); /* 33 */
        HH(ref d, a, b, c, x[8], S32, 0x8771f681); /* 34 */
        HH(ref c, d, a, b, x[11], S33, 0x6d9d6122); /* 35 */
        HH(ref b, c, d, a, x[14], S34, 0xfde5380c); /* 36 */
        HH(ref a, b, c, d, x[1], S31, 0xa4beea44); /* 37 */
        HH(ref d, a, b, c, x[4], S32, 0x4bdecfa9); /* 38 */
        HH(ref c, d, a, b, x[7], S33, 0xf6bb4b60); /* 39 */
        HH(ref b, c, d, a, x[10], S34, 0xbebfbc70); /* 40 */
        HH(ref a, b, c, d, x[13], S31, 0x289b7ec6); /* 41 */
        HH(ref d, a, b, c, x[0], S32, 0xeaa127fa); /* 42 */
        HH(ref c, d, a, b, x[3], S33, 0xd4ef3085); /* 43 */
        HH(ref b, c, d, a, x[6], S34, 0x4881d05); /* 44 */
        HH(ref a, b, c, d, x[9], S31, 0xd9d4d039); /* 45 */
        HH(ref d, a, b, c, x[12], S32, 0xe6db99e5); /* 46 */
        HH(ref c, d, a, b, x[15], S33, 0x1fa27cf8); /* 47 */
        HH(ref b, c, d, a, x[2], S34, 0xc4ac5665); /* 48 */

        // Round 4
        II(ref a, b, c, d, x[0], S41, 0xf4292244); /* 49 */
        II(ref d, a, b, c, x[7], S42, 0x432aff97); /* 50 */
        II(ref c, d, a, b, x[14], S43, 0xab9423a7); /* 51 */
        II(ref b, c, d, a, x[5], S44, 0xfc93a039); /* 52 */
        II(ref a, b, c, d, x[12], S41, 0x655b59c3); /* 53 */
        II(ref d, a, b, c, x[3], S42, 0x8f0ccc92); /* 54 */
        II(ref c, d, a, b, x[10], S43, 0xffeff47d); /* 55 */
        II(ref b, c, d, a, x[1], S44, 0x85845dd1); /* 56 */
        II(ref a, b, c, d, x[8], S41, 0x6fa87e4f); /* 57 */
        II(ref d, a, b, c, x[15], S42, 0xfe2ce6e0); /* 58 */
        II(ref c, d, a, b, x[6], S43, 0xa3014314); /* 59 */
        II(ref b, c, d, a, x[13], S44, 0x4e0811a1); /* 60 */
        II(ref a, b, c, d, x[4], S41, 0xf7537e82); /* 61 */
        II(ref d, a, b, c, x[11], S42, 0xbd3af235); /* 62 */
        II(ref c, d, a, b, x[2], S43, 0x2ad7d2bb); /* 63 */
        II(ref b, c, d, a, x[9], S44, 0xeb86d391); /* 64 */

        state[0] += a;
        state[1] += b;
        state[2] += c;
        state[3] += d;

        for (var i = 0; i < 16; i++)
            x[i] = 0;
    }

    #region encode - decode
    private void encode(ref byte[] output, uint[] input, uint len)
    {
        uint i, j;
        if (System.BitConverter.IsLittleEndian)
        {
            for (i = 0, j = 0; j < len; i++, j += 4)
            {
                output[j] = (byte)(input[i] & 0xff);
                output[j + 1] = (byte)((input[i] >> 8) & 0xff);
                output[j + 2] = (byte)((input[i] >> 16) & 0xff);
                output[j + 3] = (byte)((input[i] >> 24) & 0xff);
            }
        }
        else
        {
            for (i = 0, j = 0; j < len; i++, j += 4)
            {
                output[j + 3] = (byte)(input[i] & 0xff);
                output[j + 2] = (byte)((input[i] >> 8) & 0xff);
                output[j + 1] = (byte)((input[i] >> 16) & 0xff);
                output[j] = (byte)((input[i] >> 24) & 0xff);
            }
        }
    }

    private void decode(ref uint[] output, byte[] input, uint len)
    {
        uint i, j;
        if (System.BitConverter.IsLittleEndian)
        {
            for (i = 0, j = 0; j < len; i++, j += 4)
                output[i] = ((uint)input[j]) | (((uint)input[j + 1]) << 8) |
                    (((uint)input[j + 2]) << 16) | (((uint)input[j + 3]) << 24);
        }
        else
        {
            for (i = 0, j = 0; j < len; i++, j += 4)
                output[i] = ((uint)input[j + 3]) | (((uint)input[j + 2]) << 8) |
                    (((uint)input[j + 1]) << 16) | (((uint)input[j]) << 24);
        }
    }
    #endregion

    private uint rotate_left(uint x, uint n)
    {
        return (x << (int)n) | (x >> (int)(32 - n));
    }

    #region F, G, H and I are basic MD5 functions.
    private uint F(uint x, uint y, uint z)
    {
        return (x & y) | (~x & z);
    }

    private uint G(uint x, uint y, uint z)
    {
        return (x & z) | (y & ~z);
    }

    private uint H(uint x, uint y, uint z)
    {
        return x ^ y ^ z;
    }

    private uint I(uint x, uint y, uint z)
    {
        return y ^ (x | ~z);
    }
    #endregion

    #region  FF, GG, HH, and II transformations for rounds 1, 2, 3, and 4.
    private void FF(ref uint a, uint b, uint c, uint d, uint x, uint s, uint ac)
    {
        a += F(b, c, d) + x + ac;
        a = rotate_left(a, s) + b;
    }

    private void GG(ref uint a, uint b, uint c, uint d, uint x, uint s, uint ac)
    {
        a += G(b, c, d) + x + ac;
        a = rotate_left(a, s) + b;
    }

    private void HH(ref uint a, uint b, uint c, uint d, uint x, uint s, uint ac)
    {
        a += H(b, c, d) + x + ac;
        a = rotate_left(a, s) + b;
    }

    private void II(ref uint a, uint b, uint c, uint d, uint x, uint s, uint ac)
    {
        a += I(b, c, d) + x + ac;
        a = rotate_left(a, s) + b;
    }
    #endregion
}