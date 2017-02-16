using System;
using System.Reflection;
using DogeNews.Services.Data.Contracts;
using DogeNews.Web.Mvp.UserControls.ArticleComments;
using DogeNews.Web.Mvp.UserControls.ArticleComments.EventArguments;

using Moq;
using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.UserControls
{
    [TestFixture]
    public class ArticleCommentsPresenterTests
    {
        private Mock<IArticleCommentsService> commentsService;
        private Mock<IArticleCommentsView> view;

        [SetUp]
        public void Init()
        {
            this.commentsService = new Mock<IArticleCommentsService>();
            this.view = new Mock<IArticleCommentsView>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenArticleCommentsServiceIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new ArticleCommentsPresenter(this.view.Object, null));

            Assert.AreEqual("articleCommentsService", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldSetCommentsServiceField()
        {
            ArticleCommentsPresenter presenter = new ArticleCommentsPresenter(
                this.view.Object, 
                this.commentsService.Object);
            IArticleCommentsService commentServiceField = (IArticleCommentsService)typeof(ArticleCommentsPresenter)
                .GetField("articleCommentsService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.commentsService.Object, commentServiceField);
        }

        [Test]
        public void PageLoad_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            ArticleCommentsPresenter presenter = new ArticleCommentsPresenter(
                this.view.Object,
                this.commentsService.Object);
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.PageLoad(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }

        [Test]
        public void PageLoad_ArticleCommentsServiceGetCommentsForArticleByTitleShouldBeCalledWithTheEventArgsTitle()
        {
            this.view.SetupGet(x => x.Model).Returns(new ArticleCommentsViewModel());

            ArticleCommentsPresenter presenter = new ArticleCommentsPresenter(this.view.Object, this.commentsService.Object);
            string title = "Title";
            ArticleCommetnsPageLoadEventArgs eventArgs = new ArticleCommetnsPageLoadEventArgs { Title = title };

            presenter.PageLoad(null, eventArgs);
            this.commentsService.Verify(x => x.GetCommentsForArticleByTitle(It.Is<string>(a => a == title)), Times.Once);
        }

        [Test]
        public void AddComments_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            ArticleCommentsPresenter presenter = new ArticleCommentsPresenter(
                this.view.Object,
                this.commentsService.Object);
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.AddComment(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }

        [Test]
        public void AddComments_ArticleCommentsServiceAddCommentShouldBeCalledWithTheRightParameters()
        {
            this.view.SetupGet(x => x.Model).Returns(new ArticleCommentsViewModel());

            ArticleCommentsPresenter presenter = new ArticleCommentsPresenter(this.view.Object, this.commentsService.Object);
            AddCommentEventArguments eventArgs = new AddCommentEventArguments { ArticleTitle = "Title", Content = "Content", Username = "Username" };

            presenter.AddComment(null, eventArgs);
            this.commentsService.Verify(x =>
                x.AddComment(
                    It.Is<string>(a => a == eventArgs.ArticleTitle),
                    It.Is<string>(a => a == eventArgs.Content),
                    It.Is<string>(a => a == eventArgs.Username)),
                Times.Once);
        }

        [Test]
        public void AddComments_ArticleCommentsServiceGetCommentsForArticleByTitleShouldBeCalledWithArticleTitleFromEventArgs()
        {
            this.view.SetupGet(x => x.Model).Returns(new ArticleCommentsViewModel());

            ArticleCommentsPresenter presenter = new ArticleCommentsPresenter(this.view.Object, this.commentsService.Object);
            AddCommentEventArguments eventArgs = new AddCommentEventArguments { ArticleTitle = "Title", Content = "Content", Username = "Username" };

            presenter.AddComment(null, eventArgs);
            this.commentsService.Verify(x => x.GetCommentsForArticleByTitle(It.Is<string>(a => a == eventArgs.ArticleTitle)), Times.Once);
        }
    }
}
