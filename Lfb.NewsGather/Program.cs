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
                
                x.Service<NewsService>(s =>
                {
                    //  s.ConstructUsing(name => new Service());
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
        }
    }
}
