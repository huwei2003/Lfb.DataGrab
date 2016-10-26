using System.Data.SqlTypes;
using System.IO;
using System.IO.Compression;
using System.Net.Security;
using System.Security.Cryptography;

namespace Comm.Tools.Utility
{
    public static class StreamHelper
    {
        public static byte[] ToBytes(this Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return bytes;
            }
            return stream.CopyStream().ToArray();
        }
        /// <summary>
        /// 流拷贝
        /// </summary>
        private static MemoryStream CopyStream(this Stream input)
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
        public static MemoryStream Copy(this Stream input)
        {
            return input.CopyStream();
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this FileStream input)
        {
            return input.CopyStream();
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this GZipStream input)
        {
            return input.CopyStream();
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this DeflateStream input)
        {
            return input.CopyStream();
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this BufferedStream input)
        {
            return input.CopyStream();
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this MemoryStream input)
        {
            return input.CopyStream();
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this SqlFileStream input)
        {
            return input.CopyStream();
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this UnmanagedMemoryStream input)
        {
            return input.CopyStream();
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this SslStream input)
        {
            return input.CopyStream();
        }

        public static string Md5(this Stream input, bool toUpper = true)
        {
            var bytes = MD5.Create().ComputeHash(input);
            var md5 = bytes.ToHexString();
            return toUpper ? md5.ToUpper() : md5;
        }
    }
}
