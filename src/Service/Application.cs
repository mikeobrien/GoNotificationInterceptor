using System.Threading;
using GoNotificationInterceptor.Configuration;

namespace GoNotificationInterceptor
{
    public class Application
    {
        private MessageHandler _handler;
        private SmtpServer _server;

        public Application()
        {
            var application = Manager.Current.Application;
            _handler = new MessageHandler(application.SmtpPort,
                                          application.SmtpServer,
                                          application.RootNodeName,
                                          application.SubjectRegex,
                                          application.BodyRegex,
                                          application.SubjectStylesheetPath,
                                          application.BodyStylesheetPath);
            _server = new SmtpServer(application.ListenPort, _handler.HandleMessage);
        }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(x => _server.Start());
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}
