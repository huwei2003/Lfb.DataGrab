using System;

namespace Comm.Tools.Utility
{
    public static class GuidHelper
    {
        public static long ToLong(this Guid id)
        {
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        }
    }
}
