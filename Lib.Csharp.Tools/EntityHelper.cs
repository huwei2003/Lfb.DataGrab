using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// 可以对数据库媒体项编解码
    /// </summary>
    public static class EntityHelper
    {

        static EntityHelper()
        {
        }

        /// <summary>
        /// 复制相同属性的内容到目标
        /// 
        /// </summary>
        /// <param name="src"/><param name="dst"/>
        public static void CopyTo(this object src, object dst)
        {
            Type type = src.GetType();
            foreach (PropertyInfo propertyInfo in dst.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    PropertyInfo property = type.GetProperty(propertyInfo.Name);
                    if (property != null)
                        propertyInfo.SetValue(dst, property.GetValue(src, (object[])null), (object[])null);
                }
            }
        }

        public static void PatchTo(this object src, object dst, ICollection<string> propertyName)
        {
            var type = src.GetType();
            //dst.GetType();
            foreach (string name in (IEnumerable<string>)propertyName)
            {
                PropertyInfo property = type.GetProperty(name);
                type.GetProperty(name).SetValue(dst, property.GetValue(src, (object[])null), (object[])null);
            }
        }

        /// <summary>
        /// 得到一个类中成员属性表达式的名称,数组变为.N格式
        /// 例如 p=&gt;p.Id,得到"Id"
        /// p=&gt;p.Ids[2],2 得到 Ids.2
        /// p=&gt;p.Ids[2].Name,2 得到 Ids.2.Name
        /// </summary>
        /// <param name="propertyExp"></param>
        /// <param name="arrayIndexes"></param>
        /// <returns/>
        public static string E2S<T>(this Expression<Func<T, object>> propertyExp, params int[] arrayIndexes)
        {
            var num1 = arrayIndexes == null ? -1 : arrayIndexes.Length - 1;
            var expression = propertyExp.Body;
            var str1 = (string)null;
            while (true)
            {
                if (expression.NodeType == ExpressionType.Convert)
                {
                    expression = ((UnaryExpression) expression).Operand;
                }
                if (expression.NodeType == ExpressionType.MemberAccess)
                {
                    string str2;
                    if (str1 != null)
                    {
                        str2 = string.Format("{1}.{0}",
                            new object[2] {(object) str1, (object) (expression as MemberExpression).Member.Name});
                    }
                    else
                    {
                        str2 = (expression as MemberExpression).Member.Name;
                    }
                    str1 = str2;
                    expression = (expression as MemberExpression).Expression;
                }
                else if (expression.NodeType == ExpressionType.ArrayIndex)
                {
                    if (num1 >= 0)
                    {
                        var num2 = arrayIndexes[num1--];
                        string str2;
                        if (str1 != null)
                        {
                            str2 = string.Format("{1}.{0}", new object[2]
                            {
                                (object) str1,
                                (object) num2.ToString()
                            });
                        }
                        else
                        {
                            str2 = num2.ToString();
                        }
                        str1 = str2;
                        expression = (expression as BinaryExpression).Left;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    goto label_14;
                }
            }
            throw new InvalidOperationException(string.Format("'{0}'包含数组,但没有下标参数", new object[1]
            {
                (object) expression.ToString()
            }));

        label_14:
            if (expression.NodeType == ExpressionType.Parameter)
            {
                if (str1 != null)
                {
                    return str1;
                }
                throw new InvalidOperationException(string.Format("'{0}'不是正确的属性", new object[1]
                {
                    (object) expression.ToString()
                }));
            }
            else
            {
                throw new InvalidOperationException(string.Format("'{0}'不是属性,非法表达式", new object[1]
                {
                    (object) expression.ToString()
                }));
            }
        }
    }
}
