using System;

using DogeNews.Web.Mvp.Account.Login.EventArguments;
using WebFormsMvp;

namespace DogeNews.Web.Mvp.Account.Login
{
    public interface ILoginView : IView<LoginViewModel>
    {
        event EventHandler<LoginEventArgs> LoginUser;
    }
}
