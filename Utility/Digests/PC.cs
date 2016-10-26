using System.Collections.Generic;

namespace Comm.Tools.Utility
{
    /// <summary>
    /// 排列与组合
    /// </summary>
    public class PC
    {
        /// <summary>
        /// 交换两个变量
        /// </summary>
        /// <param name="a">变量1</param>
        /// <param name="b">变量2</param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// 递归算法求数组的组合(私有成员)
        /// </summary>
        /// <param name="list">返回的范型</param>
        /// <param name="t">所求数组</param>
        /// <param name="n">辅助变量</param>
        /// <param name="m">辅助变量</param>
        /// <param name="b">辅助数组</param>
        /// <param name="M">辅助变量M</param>
        private static void GetCombination<T>(ref List<T[]> list, T[] t, int n, int m, int[] b, int M)
        {
            for (int i = n; i >= m; i--)
            {
                b[m - 1] = i - 1;
                if (m > 1)
                {
                    GetCombination(ref list, t, i - 1, m - 1, b, M);
                }
                else
                {
                    if (list == null)
                    {
                        list = new List<T[]>();
                    }
                    T[] temp = new T[M];
                    for (int j = 0; j < b.Length; j++)
                    {
                        temp[j] = t[b[j]];
                    }
                    list.Add(temp);
                }
            }
        }

        /// <summary>
        /// 递归算法求排列(私有成员)
        /// </summary>
        /// <param name="list">返回的列表</param>
        /// <param name="t">所求数组</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        private static void GetPermutation<T>(ref List<T[]> list, T[] t, int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
            {
                if (list == null)
                {
                    list = new List<T[]>();
                }
                T[] temp = new T[t.Length];
                t.CopyTo(temp, 0);
                list.Add(temp);
            }
            else
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    Swap(ref t[startIndex], ref t[i]);
                    GetPermutation(ref list, t, startIndex + 1, endIndex);
                    Swap(ref t[startIndex], ref t[i]);
                }
            }
        }

        /// <summary>
        /// 求从起始标号到结束标号的排列，其余元素不变
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        /// <returns>从起始标号到结束标号排列的范型</returns>
        public static List<T[]> GetPermutation<T>(T[] t, int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex > t.Length - 1)
            {
                return null;
            }
            List<T[]> list = new List<T[]>();
            GetPermutation(ref list, t, startIndex, endIndex);
            return list;
        }

        /// <summary>
        /// 返回数组所有元素的全排列
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <returns>全排列的范型</returns>
        public static List<T[]> GetPermutation<T>(T[] t)
        {
            return GetPermutation(t, 0, t.Length - 1);
        }

        /// <summary>
        /// 求数组中n个元素的排列
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <param name="n">元素个数</param>
        /// <returns>数组中n个元素的排列</returns>
        public static List<T[]> GetPermutation<T>(T[] t, int n)
        {
            if (n > t.Length)
            {
                return null;
            }
            List<T[]> list = new List<T[]>();
            List<T[]> c = GetCombination(t, n);
            for (int i = 0; i < c.Count; i++)
            {
                List<T[]> l = new List<T[]>();
                GetPermutation(ref l, c[i], 0, n - 1);
                list.AddRange(l);
            }
            return list;
        }

        /// <summary>
        /// 求数组中n个元素的组合
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <param name="n">元素个数</param>
        /// <returns>数组中n个元素的组合的范型</returns>
        public static List<T[]> GetCombination<T>(T[] t, int n)
        {
            if (t.Length < n)
            {
                return null;
            }
            var temp = new int[n];
            var list = new List<T[]>();
            GetCombination(ref list, t, t.Length, n, temp, n);
            return list;
        }

        /// <summary>
        /// 乘积函数
        /// </summary>
        /// <param name="t"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int N(int t, int b)
        {
            var returns = b;
            for (var i = b - 1; i > b - t; i--)
            {
                returns *= i;
            }
            return returns;
        }

        /// <summary>
        /// 阶乘函数
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int N(int b)
        {
            return N(b - 1, b);
        }

        /// <summary>
        /// 排列算法，从N个元素取R个进行排列
        /// </summary>
        /// <param name="r">参与选择的元素个数</param>
        /// <param name="n">元素的总个数</param>
        /// <returns></returns>
        public static int P(int r, int n)
        {
            return N(r, n);
        }

        /// <summary>
        /// 组合算法，从N个元素取R个，不进行排列
        /// </summary>
        /// <param name="r">参与选择的元素个数</param>
        /// <param name="n">元素的总个数</param>
        /// <returns></returns>
        public static int C(int r, int n)
        {
            if (r == 0 && n > 0)
            {
                return 1;
            }
            if (r <= 0 || n <= 0)
            {
                return 0;
            }
            return r == n ? 1 : P(r, n) / (r > 1 ? N(r) : 1);
        }
    }
}