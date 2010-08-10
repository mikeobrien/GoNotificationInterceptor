using System;
using GoNotificationInterceptor.Configuration;
using Topshelf;
using Topshelf.Configuration;
using Topshelf.Configuration.Dsl;

namespace GoNotificationInterceptor
{
    class Program
    {
        static void Main(string[] args)
        {
            RunConfiguration cfg = RunnerConfigurator.New(x =>
            {
                x.ConfigureServiceInIsolation<Application>(s =>
                {
                    s.Named("GoNotificationInterceptor");
                    s.HowToBuildService(name => new Application());
                    s.WhenStarted(app => app.Start());
                    s.WhenStopped(app => app.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("ThoughtWorks Go Notification Interceptor");
                x.SetDisplayName("Go Notification Interceptor");
                x.SetServiceName("GoNotificationInterceptor");
            });

            Runner.Host(cfg, args);
        }
    }
}
