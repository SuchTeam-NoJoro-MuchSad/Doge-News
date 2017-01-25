namespace DogeNews.Web.Services.Contracts
{
    public interface ICryptographicService
    {
        byte[] GetSalt();

        byte[] HashPassword(string password, byte[] salt);

        string ByteArrayToString(byte[] array);

        byte[] Base64StringToByteArray(string base64string);

        bool IsValidPassword(string passwordToCheck, string passHash, string salt);
    }
}
