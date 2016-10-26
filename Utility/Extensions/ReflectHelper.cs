using System;
using System.Reflection;

namespace Comm.Tools.Utility
{
    public static class ReflectHelper
    {
        public static object Invoke(this Type type, string method, ref string error, params object[] paras)
        {
            var mi = type.GetMethod(method, BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public);
            if (mi == null)
            {
                error = string.Format("未实现 {0} 方法！", method);
            }
            else
            {
                try
                {
                    return mi.Invoke(null, paras);
                }
                catch (Exception e)
                {
                    error = e.Message + e.StackTrace;
                }
            }
            return null;
        }

        public static object Invoke(this ConstructorInfo constructor, params object[] paras)
        {
            return constructor.Invoke(paras);
        }
    }
}
