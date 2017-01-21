using System.Web;

using DogeNews.Web.Models;

namespace DogeNews.Web.Services.Contracts
{
    public interface IAuthService
    {
        bool RegisterUser(UserWebModel user);

        UserWebModel LoginUser(string username, string password);

        bool IsUserLoggedIn(HttpCookieCollection cookies);
    }
}