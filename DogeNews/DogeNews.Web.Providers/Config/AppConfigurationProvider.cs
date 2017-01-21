using System.Web.Configuration;

using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Providers.Config
{
    public class AppConfigurationProvider : IAppConfigurationProvider
    {
        public string AuthCookieName
        {
            get
            {
                return WebConfigurationManager.AppSettings["AuthCookieName"];
            }
        }

        public string EncryptionKey
        {
            get
            {
                return WebConfigurationManager.AppSettings["EncryptionKey"];
            }
        }

        public string AdminPassword
        {
            get
            {
                return WebConfigurationManager.AppSettings["AdminPassword"];
            }
        }
    }
}
