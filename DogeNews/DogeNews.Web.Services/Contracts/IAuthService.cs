using DogeNews.Web.Models;

namespace DogeNews.Web.Services.Contracts
{
    public interface IAuthService
    {
        bool RegisterUser(UserWebModel user);
    }
}