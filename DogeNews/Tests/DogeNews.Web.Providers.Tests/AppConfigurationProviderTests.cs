using DogeNews.Web.Providers.Config;
using NUnit.Framework;

namespace DogeNews.Web.Providers.Tests
{
    [TestFixture]
    public class AppConfigurationProviderTests
    {
        [Test]
        public void AuthCookieName_ShouldNotThrow()
        {
            var appConfigProvider = new AppConfigurationProvider();

            Assert.DoesNotThrow(() =>
            {
                var authCookieName = appConfigProvider.AuthCookieName;
            });
        }
        
        [Test]
        public void EncryptionKey_ShouldNotThrow()
        {
            var appConfigProvider = new AppConfigurationProvider();

            Assert.DoesNotThrow(() =>
            {
                var encryptionKey = appConfigProvider.EncryptionKey;
            });
        }

        [Test]
        public void AdminPassword_ShouldNotThrow()
        {
            var appConfigProvider = new AppConfigurationProvider();
            
            Assert.DoesNotThrow(()=>
            {
                var adminPassword = appConfigProvider.AdminPassword;
            });
        }
    }
}