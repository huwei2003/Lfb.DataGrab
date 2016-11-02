using System;
using System.IO;
using log4net.Config;
using Topshelf;

namespace Lfb.NewsGather
{
    class Program
    {
        static void Main(string[] args)
        {
            string logConfig = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log4net.config";
            XmlConfigurator.ConfigureAndWatch(
                new FileInfo(logConfig));
            Console.WriteLine(logConfig);

            var host = HostFactory.New(x =>
            {
                //x.EnableDashboard(); //有问题则注释掉该行
                x.Service<NewsService>(s =>
                {
                    s.ConstructUsing(name => new NewsService());
                    s.WhenStarted(tc =>
                    {
                        tc.Start();
                    });
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("Lfb.NewsGather start");
                x.SetDisplayName("Lfb.NewsGather");
                x.SetServiceName("Lfb.NewsGather");
            });

            host.Run();
            

            //HostFactory.Run(x =>
            //{
            //    x.Service<ZipPackService>(s =>
            //    {
            //        s.ConstructUsing(name => new ZipPackService(new ServiceRepository(new FileHelper())));
            //        s.WhenStarted((tc, hostControl) => tc.Start(hostControl));
            //        s.WhenStopped((tc, hostControl) => tc.Stop(hostControl));
            //    });
            //    x.RunAsLocalSystem();
            //    x.StartAutomaticallyDelayed();
            //    x.SetDescription("9 Angle Zip Refresh");
            //    x.SetDisplayName("ZipPack");
            //    x.SetServiceName("ZipPack");
            //});
        }
    }
}
