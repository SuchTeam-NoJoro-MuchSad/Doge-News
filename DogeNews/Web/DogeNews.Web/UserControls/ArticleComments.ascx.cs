using System;

using DogeNews.Web.Mvp.UserControls.ArticleComments;
using DogeNews.Web.Mvp.UserControls.ArticleComments.EventArguments;

using WebFormsMvp.Web;
using WebFormsMvp;

namespace DogeNews.Web.UserControls
{
    [PresenterBinding(typeof(ArticleCommentsPresenter))]
    public partial class ArticleComments : MvpUserControl<ArticleCommentsViewModel>, IArticleCommentsView
    {
        public event EventHandler<ArticleCommetnsPageLoadEventArgs> PageLoad;
        public event EventHandler<AddCommentEventArguments> AddComment;

        protected void Page_Load(object sender, EventArgs e)
        {
            var eventArgs = new ArticleCommetnsPageLoadEventArgs
            {
                ViewState = this.ViewState,
                IsPostBack = false,
                Title = Context.Request.QueryString["Title"]
            };

            if (IsPostBack)
            {
                eventArgs.IsPostBack = true;
            }

            this.PageLoad(this, eventArgs);
        }

        protected void ButtonSubmitComment(object sender, EventArgs e)
        {
            var eventArguments = new AddCommentEventArguments
            {
                Username = this.Context.User.Identity.Name,
                Content = this.AddCommentTextBox.Text,
                ArticleTitle = this.Context.Request.QueryString["Title"]
            };

            this.AddComment(this, eventArguments);
        }
    }
}