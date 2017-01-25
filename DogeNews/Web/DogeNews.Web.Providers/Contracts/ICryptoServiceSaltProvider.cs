namespace DogeNews.Web.Providers.Contracts
{
    public interface ICryptoServiceSaltProvider
    {
        byte[] GetBytes(byte[] array);        
    }
}
