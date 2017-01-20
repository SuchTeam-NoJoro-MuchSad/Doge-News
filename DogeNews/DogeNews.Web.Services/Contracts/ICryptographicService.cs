namespace DogeNews.Web.Services.Contracts
{
    public interface ICryptographicService
    {
        byte[] GetSalt();

        byte[] HashPassword(string password, byte[] salt);

        string ByteArrayToString(byte[] array);

        bool IsValidPassword(string passwordToCheck, string passHash, string salt);
    }
}
