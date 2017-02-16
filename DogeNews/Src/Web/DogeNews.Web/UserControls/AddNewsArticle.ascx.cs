using System;
using DogeNews.Common.Constants;
using DogeNews.Common.Enums;
using DogeNews.Web.Mvp.News.Add;
using DogeNews.Web.Mvp.News.Add.EventArguments;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace DogeNews.Web.UserControls
{
    [PresenterBinding(typeof(AddNewsPresenter))]
    public partial class AddNewsArticle : MvpUserControl<AddNewsViewModel>, IAddNewsView
    {
        public event EventHandler<AddNewsEventArgs> AddNews;

        public void AddNewsClick(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                AddNewsEventArgs eventData = new AddNewsEventArgs
                {
                    Title = this.Server.HtmlEncode(this.TitleInput.Value),
                    Image = this.ImageFileUpload.PostedFile,
                    FileName = this.ImageFileUpload.PostedFile.FileName,
                    Content = this.AddNewsControl.Content,
                    Category = (NewsCategoryType)int.Parse(this.CategorySelect.Value)
                };

                this.AddNews(this, eventData);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Context.User.IsInRole(Roles.Admin))
            {
                this.Response.Redirect("/");
            }
        }
    }
}