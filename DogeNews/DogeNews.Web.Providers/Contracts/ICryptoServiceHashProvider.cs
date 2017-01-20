namespace DogeNews.Web.Providers.Contracts
{
    public interface ICryptoServiceHashProvider 
    {
        byte[] GetHashBytes(string stringToHash, byte[] salt, int iterationsCount, int hashBytesCount);
    }
}
