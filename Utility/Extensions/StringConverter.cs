using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Comm.Tools.Utility
{
    public static class StringConverter
    {
        public static string Base64Decode(this string s)
        {
            return Base64Decode(s, Encoding.UTF8);
        }

        public static string Base64Decode(this string s, Encoding e)
        {
            return e.GetString(Convert.FromBase64String(s));
        }

        public static string Base64Encode(this string s)
        {
            return Base64Encode(s, Encoding.UTF8);
        }

        public static string Base64Encode(this string s, Encoding e)
        {
            return Convert.ToBase64String(e.GetBytes(s));
        }

        /// <summary>
        /// 是否含有Unicode码字符串
        /// </summary>
        public static bool HasUnicode(this string s)
        {
            return s.IsMatch("\\\\u[0-9a-fA-f]{4}");
        }
        /// <summary>
        /// 字符串转为Unicode码字符串
        /// </summary>
        public static string ToUnicode(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            foreach (var c in s)
            {
                sb.Append("\\u").Append(((int)c).ToString("x").PadLeft(4, '0'));
            }
            return sb.ToString();
        }
        /// <summary>
        /// Unicode码字符串转为字符串
        /// </summary>
        public static string ToStringFromUnicode(this string s)
        {
            return Regex.Unescape(s);
        }

        public static bool ToBoolean(this string val, bool d = false)
        {
            bool result;
            if (!bool.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }
        public static DateTime ToDateIssue(this string val)
        {
            if (val.Length < 8)
                return DateTime.MinValue;
            return val.Substring(0, 8).Insert(4, "-").Insert(7, "-").ToDateTime();
        }

        public static DateTime ToDateTime(this string val, DateTime d)
        {
            DateTime result;
            if (!DateTime.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        public static DateTime ToDateTime(this string val)
        {
            DateTime result;
            if (!DateTime.TryParse(val, out result))
            {
                result = DateTime.Now;
            }
            return result;
        }

        /// <summary>
        /// 全角转半角
        /// </summary>
        public static string ToDBC(this string input)
        {
            var chArray = input.ToCharArray();
            for (var i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == '　')
                {
                    chArray[i] = ' ';
                }
                else if ((chArray[i] > 0xff00) && (chArray[i] < 0xff5f))
                {
                    chArray[i] = (char)(chArray[i] - 0xfee0);
                }
            }
            return new string(chArray);
        }

        /// <summary>
        /// 半角转全角
        /// </summary>
        public static string ToSBC(this string input)
        {
            var chArray = input.ToCharArray();
            for (var i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == ' ')
                {
                    chArray[i] = '　';
                }
                else if (chArray[i] < '\x007f')
                {
                    chArray[i] = (char)(chArray[i] + 0xfee0);
                }
            }
            return new string(chArray);
        }

        public static decimal ToDecimal(this string val)
        {
            var result = 0M;
            decimal.TryParse(val, out result);
            return result;
        }

        public static decimal ToDecimal(this string val, decimal d = 0M)
        {
            decimal result;
            if (!decimal.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        public static double ToDouble(this string val, double d = 0d)
        {
            double result;
            if (!double.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        /// <summary>
        /// 唯一性的标识符
        /// </summary>
        public static Guid ToGuid(this string val)
        {
            var empty = Guid.Empty;
            if (!string.IsNullOrEmpty(val))
            {
                empty = new Guid(val);
            }
            return empty;
        }

        public static int ToInt32(this string val, int d = 0)
        {
            int result;
            if (!int.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }
        public static long ToInt64(this object val, long d = 0L)
        {
            try
            {
                return Convert.ToInt64(val);
            }
            catch
            {
                return d;
            }
        }
        public static int ToInt32(this object val, int d = 0)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return d;
            }
        }

        public static string ToString(this string val, string d = "")
        {
            var result = d;
            if (!string.IsNullOrEmpty(val))
            {
                result = val;
            }
            return result;
        }

        public static byte ToByte(this string val, byte d = 0)
        {
            byte result;
            if (!byte.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        public static float ToFloat(this string val, float d = 0F)
        {
            float result;
            if (!float.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        public static int ToInt32(this char val, int d = 0)
        {
            return ToInt32(val.ToString(), d);
        }

        public static long ToLong(this string val, long d = 0L)
        {
            long result;
            if (!long.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        public static DateTime? ToNullableDateTime(this string val)
        {
            var minValue = DateTime.MinValue;
            if (!DateTime.TryParse(val, out minValue))
            {
                return null;
            }
            if (minValue == DateTime.MinValue)
            {
                return null;
            }
            return minValue;
        }

        public static short ToShort(this string val, short d = (short)0)
        {
            short result;
            if (!short.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }
    }
}