using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// Stream 处理类
    /// </summary>
    public  class StreamHelper
    {
        public static byte[] ToBytes( Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return bytes;
            }
            using (var ms = CopyStream(stream))
            {
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 流拷贝
        /// </summary>
        private static MemoryStream CopyStream( Stream input)
        {
            const int bufferSize = 4096;
            var output = new MemoryStream();
            var buffer = new byte[bufferSize];
            while (true)
            {
                var read = input.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                {
                    break;
                }
                output.Write(buffer, 0, read);
            }
            output.Position = 0;
            return output;
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy( Stream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy( FileStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy( GZipStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy( DeflateStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy( BufferedStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy( MemoryStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy( SqlFileStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy( UnmanagedMemoryStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy( SslStream input)
        {
            return CopyStream(input);
        }

        public static string Md5( Stream input, bool toUpper = true)
        {
            var bytes = MD5.Create().ComputeHash(input);
            var md5 = ByteHelper.ToHexString(bytes);
            return toUpper ? md5.ToUpper() : md5;
        }
    }
}
