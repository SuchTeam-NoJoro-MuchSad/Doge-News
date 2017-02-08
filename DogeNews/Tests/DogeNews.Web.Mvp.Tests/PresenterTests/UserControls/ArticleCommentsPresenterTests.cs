using System.Reflection;

using DogeNews.Web.Mvp.UserControls.ArticleComments;
using DogeNews.Web.Services.Contracts;

using Moq;
using NUnit.Framework;
using DogeNews.Web.Mvp.UserControls.ArticleComments.EventArguments;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.UserControls
{
    [TestFixture]
    public class ArticleCommentsPresenterTests
    {
        private Mock<IArticleCommentsService> mockedCommentsService;
        private Mock<IArticleCommentsView> mockedView;

        [SetUp]
        public void Init()
        {
            this.mockedCommentsService = new Mock<IArticleCommentsService>();
            this.mockedView = new Mock<IArticleCommentsView>();
        }

        [Test]
        public void Constructor_ShouldSetCommentsServiceField()
        {
            var presenter = new ArticleCommentsPresenter(this.mockedView.Object, this.mockedCommentsService.Object);
            var commentServiceField = (IArticleCommentsService)typeof(ArticleCommentsPresenter)
                .GetField("articleCommentsService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockedCommentsService.Object, commentServiceField);
        }

        [Test]
        public void PageLoad_ArticleCommentsServiceGetCommentsForArticleByTitleShouldBeCalledWithTheEventArgsTitle()
        {
            this.mockedView.SetupGet(x => x.Model).Returns(new ArticleCommentsViewModel());

            var presenter = new ArticleCommentsPresenter(this.mockedView.Object, this.mockedCommentsService.Object);
            var title = "Title";
            var eventArgs = new ArticleCommetnsPageLoadEventArgs { Title = title };

            presenter.PageLoad(null, eventArgs);
            this.mockedCommentsService.Verify(x => x.GetCommentsForArticleByTitle(It.Is<string>(a => a == title)), Times.Once);
        }

        [Test]
        public void AddComments_ArticleCommentsServiceAddCommentShouldBeCalledWithTheRightParameters()
        {
            this.mockedView.SetupGet(x => x.Model).Returns(new ArticleCommentsViewModel());

            var presenter = new ArticleCommentsPresenter(this.mockedView.Object, this.mockedCommentsService.Object);
            var eventArgs = new AddCommentEventArguments { ArticleTitle = "Title", Content = "Content", Username = "Username" };

            presenter.AddComment(null, eventArgs);
            this.mockedCommentsService.Verify(x =>
                x.AddComment(
                    It.Is<string>(a => a == eventArgs.ArticleTitle),
                    It.Is<string>(a => a == eventArgs.Content),
                    It.Is<string>(a => a == eventArgs.Username)),
                Times.Once);
        }

        [Test]
        public void AddComments_ArticleCommentsServiceGetCommentsForArticleByTitleShouldBeCalledWithArticleTitleFromEventArgs()
        {
            this.mockedView.SetupGet(x => x.Model).Returns(new ArticleCommentsViewModel());

            var presenter = new ArticleCommentsPresenter(this.mockedView.Object, this.mockedCommentsService.Object);
            var eventArgs = new AddCommentEventArguments { ArticleTitle = "Title", Content = "Content", Username = "Username" };

            presenter.AddComment(null, eventArgs);
            this.mockedCommentsService.Verify(x => x.GetCommentsForArticleByTitle(It.Is<string>(a => a == eventArgs.ArticleTitle)), Times.Once);
        }
    }
}
