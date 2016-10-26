using System.ServiceProcess;
using Lfb.DataGrab.Tasks;

namespace Lfb.DataGrab
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            News.Start();
        }

        protected override void OnStop()
        {
            News.Exit();
        }
    }
}
