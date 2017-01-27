using System;

using DogeNews.Web.MVP.Account.Register.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.MVP.Account.Register
{
    public interface IRegisterView : IView<RegisterViewModel>
    {
        event EventHandler<CreateUserEventArgs> CreateUser;
    }
}