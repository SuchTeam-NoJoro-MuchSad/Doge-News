namespace DogeNews.Web.Providers.Contracts
{
    public interface IAppConfigurationProvider
    {
        string EncryptionKey { get; }

        string AuthCookieName { get; }
    }
}
