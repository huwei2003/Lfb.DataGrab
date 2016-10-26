using System.Web;
using System.Web.Util;

namespace Comm.Tools.Utility.Web
{
    /// <summary>
    /// .net 4.0 专用: 相当于2.0下 validateRequest="false",4.0下 validateRequest="false" 无效了
    /// </summary>
    public class RequestValidatorDisabled : RequestValidator
    {
        protected override bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
        {
            validationFailureIndex = -1;
            return true;
        }
    }
}
