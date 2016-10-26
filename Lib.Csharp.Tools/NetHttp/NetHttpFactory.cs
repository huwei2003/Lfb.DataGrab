using Lib.Csharp.Tools.Base;

namespace Lib.Csharp.Tools.NetHttp
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class NetHttpFactory :BaseFactory<INetHttp>
    {
        public static INetHttp GetInstanse()
        {
            if (GetMockInstanse != null)
                return GetMockInstanse;
            return new NetHttp();
        }
    }
}
