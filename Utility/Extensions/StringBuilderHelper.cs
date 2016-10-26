using System.Text;

namespace Comm.Tools.Utility
{
    public static class StringBuilderHelper
    {
        /// <summary>
        /// 在此实例的结尾追加指定字符串的副本。
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="t">空格数</param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static StringBuilder Append(this StringBuilder sb, int t, object o)
        {
            for (var i = 0; i < t; i++)
            {
                sb.Append("\t");
            }

            return sb.Append(o);
        }

        /// <summary>
        /// 在此实例的结尾追加指定字符串的副本。
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="t">空格数</param>
        /// <param name="o"></param>
        /// <param name="rn">回车数</param>
        /// <returns></returns>
        public static StringBuilder Append(this StringBuilder sb, int t, object o, int rn)
        {
            for (var i = 0; i < t; i++)
            {
                sb.Append("\t");
            }

            sb.Append(o);

            for (var i = 0; i < rn; i++)
            {
                sb.Append("\r\n");
            }

            return sb;
        }

        /// <summary>
        /// 在此实例的结尾追加指定字符串的副本。
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="o">字符串</param>
        /// <param name="rn">回车数</param>
        /// <returns></returns>
        public static StringBuilder Append(this StringBuilder sb, object o, int rn)
        {
            sb.Append(o);

            for (var i = 0; i < rn; i++)
            {
                sb.Append("\r\n");
            }

            return sb;
        }
    }
}