using System;

using DogeNews.Common.Enums;
using DogeNews.Web.Mvp.News.Edit;
using DogeNews.Web.Mvp.News.Edit.EventArguments;

using WebFormsMvp;
using WebFormsMvp.Web;

namespace DogeNews.Web.News
{
    [PresenterBinding(typeof(EditArticlePresenter))]
    public partial class Edit : MvpPage<EditArticleViewModel>, IEditArticleView
    {
        public event EventHandler<PreInitPageEventArgs> PreInitPageEvent;

        public event EventHandler<EditArticleEventArgs> EditArticleButtonClick;

        protected void Page_Load(object sender, EventArgs e)
        {
            var isAdminUser = this.Context.User.IsInRole(Common.Constants.Roles.Admin);

            if (!isAdminUser)
            {
                //TODO throw error page
                this.Response.Redirect("/");
            }

            if (string.IsNullOrEmpty(this.ClientQueryString))
            {
                //TODO throw error page
                this.Response.Redirect("/");
            }

            var eventArgs = new PreInitPageEventArgs
            {
                IsAdminUser = isAdminUser,
                QueryString = this.ClientQueryString
            };

            this.PreInitPageEvent(this, eventArgs);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.AddNewsControl.Content = this.Model.NewsItem.Content;
            this.CategorySelect.Value = this.Model.NewsItem.Category.ToString();
            this.TitleInput.Value = this.Model.NewsItem.Title;
        }

        protected void EditNewsClick(object sender, EventArgs e)
        {
            var eventArgs = new EditArticleEventArgs
            {
                Id = this.Model.NewsItem.Id,
                Title = this.Server.HtmlEncode(this.TitleInput.Value),
                Image = this.ImageFileUpload.PostedFile,
                FileName = this.ImageFileUpload.PostedFile.FileName,
                Content = this.AddNewsControl.Content,
                Category = (NewsCategoryType)int.Parse(this.CategorySelect.Value)
            };

            this.EditArticleButtonClick(this, eventArgs);
        }
    }
}