using System.Collections.Generic;

using DogeNews.Web.Models;
using DogeNews.Web.Mvp.UserControls.ArticleComments;

using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.ModelsTests.UserControls
{
    [TestFixture]
    public class ArticleCommentsViewModelTests
    {
        [Test]
        public void CommentsShouldReturnSetValue()
        {
            var model = new ArticleCommentsViewModel();
            var comments = new List<CommentWebModel>();

            model.Comments = comments;
            Assert.AreEqual(comments, model.Comments);
        }
    }
}
