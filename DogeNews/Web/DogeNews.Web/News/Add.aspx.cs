using System;

using DogeNews.Web.MVP.News.Add;
using DogeNews.Web.MVP.News.Add.EventArguments;
using DogeNews.Web.Common.Enums;
using DogeNews.Common.Constants;

using WebFormsMvp;
using WebFormsMvp.Web;

namespace DogeNews.Web.News
{
    [PresenterBinding(typeof(AddNewsPresenter))]
    public partial class Add : MvpPage<AddNewsViewModel>, IAddNewsView
    {
        public event EventHandler<AddNewsEventArgs> AddNews;

        public void AddNewsClick(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var eventData = new AddNewsEventArgs
                {
                    Title = this.Server.HtmlEncode(this.TitleInput.Value),
                    Image = this.ImageFileUpload.PostedFile,
                    Content = this.AddNewsControl.Content,
                    Category = (NewsCategoryType)int.Parse(this.CategorySelect.Value)
                };

                this.AddNews(this, eventData);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.User.IsInRole(Roles.Admin))
            {
                this.Response.Redirect("/");
            }
        }
    }
}