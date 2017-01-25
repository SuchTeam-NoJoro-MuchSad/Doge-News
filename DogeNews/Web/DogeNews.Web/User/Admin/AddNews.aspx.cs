using System;

using DogeNews.Web.User.Admin.Models;
using DogeNews.Web.User.Admin.Views;
using DogeNews.Web.User.Admin.Presenters;
using DogeNews.Web.User.Admin.Views.EventArguments;
using DogeNews.Web.Common.Enums;

using WebFormsMvp.Web;
using WebFormsMvp;

namespace DogeNews.Web.User.Admin
{
    [PresenterBinding(typeof(AdminAddNewsPresenter))]
    public partial class AddNews : MvpPage<AddNewsPageModel>, IAddNewsView
    {
        public event EventHandler<AdminAddNewsEventArgs> AddNewsEvent;

        public void AddNewsClick(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var eventData = new AdminAddNewsEventArgs
                {
                    Title = this.Server.HtmlEncode(this.TitleInput.Value),
                    Image = this.ImageFileUpload.PostedFile,
                    Content = this.AddNewsControl.Content,
                    Category = (NewsCategoryType)int.Parse(this.CategorySelect.Value)
                };

                this.AddNewsEvent(this, eventData);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["UserRole"] == null)
            {
                this.Response.Redirect("/");
            }
        }
    }
}