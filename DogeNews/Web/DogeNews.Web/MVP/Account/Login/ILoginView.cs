using System;

using DogeNews.Web.MVP.Account.Login.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.MVP.Account.Login
{
    public interface ILoginView : IView<LoginViewModel>
    {
        event EventHandler<LoginEventArgs> LoginUser;
    }
}