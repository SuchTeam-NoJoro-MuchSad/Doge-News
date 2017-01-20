namespace DogeNews.Web.Providers.Contracts
{
    public interface IEncryptionProvider
    {
        string Encrypt(string text, string key);

        string Decrypt(string text, string key);
    }
}