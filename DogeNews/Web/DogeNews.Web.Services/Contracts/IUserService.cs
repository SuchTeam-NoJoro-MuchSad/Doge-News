namespace DogeNews.Web.Services.Contracts
{
    public interface IUserService
    {
        void ChangePassword(string username, string newPassword);
    }
}
