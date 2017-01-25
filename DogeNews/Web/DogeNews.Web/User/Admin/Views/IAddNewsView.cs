using System;

using DogeNews.Web.User.Admin.Models;
using DogeNews.Web.User.Admin.Views.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.User.Admin.Views
{
    public interface IAddNewsView : IView<AddNewsPageModel>
    {
        event EventHandler<AdminAddNewsEventArgs> AddNewsEvent;
    }
}