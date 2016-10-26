using System.Web.UI;

namespace Comm.Tools.Utility.Web
{
    public class JavaScript
    {
        public static void Alert(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "",
                                                    "<script type='text/javascript'>alert('" + msg + "');</script>");
        }

        public static void Alert(Page page, string msg, string redirectUrl)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "",
                                                    "<script type='text/javascript'>alert('" + msg +
                                                    "');location.href='" + redirectUrl + "';</script>");
        }

        public static void Alert(Page page, string msg, string redirectUrl, string target)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "",
                                                    "<script type='text/javascript'>alert('" + msg + "');" + target +
                                                    ".location.href='" + redirectUrl + "';</script>");
        }
    }
}