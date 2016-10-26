using System.Text;

namespace Lib.Csharp.Tools.Web
{
    /// <summary>
    /// MessageBox 的摘要说明
    /// 用于web
    /// </summary>
    public class MessageBox : System.Web.UI.UserControl
    {
        public MessageBox()
        {

        }

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");

        }

        /// <summary>
        /// 显示方法
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="function"></param>
        public static void ShowFunction(System.Web.UI.Page page, string function)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "function", "<script language='javascript' defer>" + function.ToString() + "</script>");
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面环境,一般为this</param>
        /// <param name="msg">要显示的信息</param>
        /// <param name="trueDoSomething">如果点确定后要做的事(JavaScript脚本)</param>
        /// <param name="falseDoSomething">如果点取消后要做的事(JavaScript脚本)</param>
        public static void ShowConfirm(System.Web.UI.Page page, string msg, string trueDoSomething, string falseDoSomething)
        {
            var builder = new StringBuilder();
            builder.Append("<SCRIPT language='JavaScript' defer>");
            builder.Append("if(confirm ('" + msg + "')){");
            builder.Append("" + trueDoSomething + "}");
            builder.Append("else{");
            builder.Append("" + falseDoSomething + "}");
            builder.Append("</SCRIPT>");

            page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转，不跳出框架
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            ShowAndRedirect(page, msg, url, false);
        }
        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        /// <param name="openPage">是否跳出框架</param>
        public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url, bool openPage)
        {
            var builder = new StringBuilder();
            builder.Append("<script language='javascript' defer>");
            builder.AppendFormat("alert('{0}');", msg);
            if (openPage)
                builder.AppendFormat("top.location.href='{0}'", url);
            else
                builder.AppendFormat("location.href='{0}'", url);
            builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());

        }

        /// <summary>
        /// 显示消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="hiddenfieldName">隐藏表单控件名</param>
        public static void Confirm(System.Web.UI.Page page, string msg, string hiddenfieldName)
        {
            var strMsg = msg.Replace("\n", "\\n");
            strMsg = msg.Replace("\"", "'");

            var sb = new StringBuilder();

            sb.Append(@"<INPUT type=hidden value='0' name='" +
              hiddenfieldName + "'>");

            sb.Append(@"<script language='javascript' defer>");

            sb.Append(@" if(confirm( """ + strMsg + @""" ))");
            sb.Append(@" { ");
            sb.Append("document.forms[0]." + hiddenfieldName + ".value='1';"
              + "document.forms[0].submit(); }");
            sb.Append(@" else { ");
            sb.Append("document.forms[0]." + hiddenfieldName + ".value='0'; }");

            sb.Append(@"</script>");

            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString());
        }
        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(System.Web.UI.Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");
        }

        /// <summary>
        /// 页面重载
        /// </summary>
        public static void Location()
        {
            var sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("window.location.href=window.location.href;");
            sb.Append("</script>");
            System.Web.HttpContext.Current.Response.Write(sb.ToString());

        }
        /// <summary>
        /// 弹出消息框，转向另外一页
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public static void ShowAndHref(string msg, string url)
        {
            var sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert('" + msg + "');window.location.href='" + url + "'");

            //sb.Append("alert('" + msg +url+ "');parent.maintarget='" + url + "'");

            sb.Append("</script>");
            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }
        /// <summary>
        /// 弹出消息框，转向另外一页
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public static void ShowAndParnetHref(string msg, string url)
        {
            var sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert('" + msg + "');parent.location.href='" + url + "'");

            //sb.Append("alert('" + msg +url+ "');parent.maintarget='" + url + "'");

            sb.Append("</script>");
            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }
        /// <summary>
        /// 显示一个弹出窗口，并刷新转向上一页
        /// </summary>
        /// <param name="str"></param>
        public static void ShowPre(string str)
        {
            var sb = new StringBuilder();
            sb.Append("<script language=\"javascript\" defer> \n");
            sb.Append("alert(\"" + str.Trim() + "\"); \n");
            sb.Append("var p=document.referrer; \n");
            sb.Append("window.location.href=p;\n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 显示一个弹出窗口，不刷新转向上一页
        /// </summary>
        /// <param name="str"></param>
        public static void ShowBack(string str)
        {
            var sb = new StringBuilder();
            sb.Append("<script language=\"javascript\" defer> \n");
            sb.Append("alert(\"" + str.Trim() + "\"); \n");
            sb.Append("history.back();\n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }


    }
}
