using System;

using DogeNews.Web.Auth.Models;
using DogeNews.Web.Auth.Views.EventArgumentss;

using WebFormsMvp;

namespace DogeNews.Web.Auth.Views
{
    public interface IRegisterView : IView<RegisterPageModel>
    {
        event EventHandler<RegisterEventArgs> RegisterUser;
    }
}