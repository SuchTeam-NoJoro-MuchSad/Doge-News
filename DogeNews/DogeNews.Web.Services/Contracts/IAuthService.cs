using System.Web;
using System.Collections.Generic;

using DogeNews.Web.Models;

namespace DogeNews.Web.Services.Contracts
{
    public interface IAuthService
    {
        bool RegisterUser(UserWebModel user);

        UserWebModel LoginUser(string username, string password);

        bool IsUserLoggedIn(UserWebModel userModel, IEnumerable<HttpCookie> cookies);
    }
}