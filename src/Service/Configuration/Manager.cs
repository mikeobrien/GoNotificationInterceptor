using System.Configuration;

namespace GoNotificationInterceptor.Configuration
{
    public class Manager
    {
        private const string ConfigSection = "goNotificationInterceptor";

        private static Manager _manager;

        private Section _section;

        public static Manager Current
        {
            get
            {
                if (_manager == null) _manager = new Manager();
                return _manager;
            }
        }

        public Section Application
        {
            get
            {
                LazyLoadSection(ref _section, ConfigSection);
                return _section;
            }
        }

        private static void LazyLoadSection<TSectionType>(ref TSectionType section, string sectionName) where TSectionType : class
        {
            if (section == null)
                section = (TSectionType)ConfigurationManager.GetSection(sectionName);
        }
    }
}


