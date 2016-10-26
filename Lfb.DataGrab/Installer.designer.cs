using System.ComponentModel;
using System.ServiceProcess;

namespace Lfb.DataGrab
{
    partial class Installer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceInstaller1 = new ServiceInstaller();
            this.serviceProcessInstaller1 = new ServiceProcessInstaller();
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.Description = "礼佛宝数据抓取服务";
            this.serviceInstaller1.DisplayName = "Lfb.DataGrab";
            this.serviceInstaller1.ServiceName = "Lfb.DataGrab";
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // Installer
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceInstaller1,
            this.serviceProcessInstaller1});

        }

        #endregion

        private ServiceInstaller serviceInstaller1;
        private ServiceProcessInstaller serviceProcessInstaller1;
    }
}