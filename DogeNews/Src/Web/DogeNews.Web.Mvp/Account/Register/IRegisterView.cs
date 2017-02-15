using System;

using DogeNews.Web.Mvp.Account.Register.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.Account.Register
{
    public interface IRegisterView : IView<RegisterViewModel>
    {
        event EventHandler<CreateUserEventArgs> CreateUser;
    }
}
