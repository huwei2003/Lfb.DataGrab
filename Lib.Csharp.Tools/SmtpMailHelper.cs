using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// .net 自带的邮件发送功能封装
    /// </summary>
    public class SmtpMailHelper
    {

        private static SmtpMailConfig _config;

        public SmtpMailHelper(SmtpMailConfig config)
        {
            Init(config);
        }
        public static void Init(SmtpMailConfig config)
        {
            _config = config;
        }
        public static async Task<bool> SendMail(MailData smResult)
        {
            if (_config == null)
            {
                throw new Exception("需要调用构造函数初始化SMTP邮件服务配置");
            }
            try
            {
                var mailMsg = new MailMessage();
                mailMsg.To.Add(new MailAddress(smResult.To));
                mailMsg.From = new MailAddress(_config.FromEail, _config.FromName);
                // 邮件主题
                mailMsg.Subject = smResult.Subject;
                // 邮件正文内容

                if (smResult.MailTextType == MediaTextType.Text)
                {
                    mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(smResult.Content, null,
                        MediaTypeNames.Text.Plain));
                }
                if (smResult.MailTextType == MediaTextType.Html)
                {
                    mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(smResult.Content, null,
                        MediaTypeNames.Text.Html));
                }
                // 添加附件
                if (!string.IsNullOrEmpty(smResult.Attachments))
                {
                    //string file = "D:\\1.txt";
                    var data = new Attachment(smResult.Attachments, MediaTypeNames.Application.Octet);
                    mailMsg.Attachments.Add(data);
                }
                

                //邮件推送的SMTP地址和端口
                var smtpClient = new SmtpClient(_config.SmtpHost, _config.SmtpPort);
                // 使用SMTP用户名和密码进行验证
                var credentials = new System.Net.NetworkCredential(_config.FromEail, _config.Password);
                smtpClient.Credentials = credentials;
                await Task.Run(() =>
                {
                    smtpClient.Send(mailMsg);
                });
                
                return true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                return false;
            }
            //return smResult;
        }

        
    }
    public class MailData
    {
        public string To { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }

        public MediaTextType MailTextType { get; set; }
        public string Attachments { get; set; }
    }

    public enum MediaTextType
    {
        /// <summary>
        /// 文本格式
        /// </summary>
        Text=1,
        /// <summary>
        /// 网页格式
        /// </summary>
        Html=2,
    }

    public class SmtpMailConfig
    {
        /// <summary>
        /// smtp服务器地址
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// smtp服务器 port
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// 发送者地址 对应的密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 来源email地址 (发送者地址)
        /// </summary>
        public string FromEail { get; set; }
        /// <summary>
        /// 发送人（部门）
        /// </summary>
        public string FromName { get; set; }
        /// <summary>
        /// 回复用的邮件地址
        /// </summary>
        public string ReplyEmail { get; set; }
        /// <summary>
        /// 该通道是否可用 true or false
        /// </summary>
        public string SmsCanUse { get; set; }


    }
}
