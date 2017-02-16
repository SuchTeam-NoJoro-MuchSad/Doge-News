using DogeNews.Common.Enums;

using NUnit.Framework;

namespace DogeNews.Web.Models.Tests
{
    [TestFixture]
    public class NewsWebModelTests
    {
        [Test]
        public void Id_GetShouldReturnSetValue()
        {
            int id = 1;
            NewsWebModel item = new NewsWebModel();

            item.Id = id;
            Assert.AreEqual(id, item.Id);
        }

        [Test]
        public void Title_GetShouldReturnSetValue()
        {
            string title = "title";
            NewsWebModel item = new NewsWebModel();

            item.Title = title;
            Assert.AreEqual(title, item.Title);
        }

        [Test]
        public void Category_GetShouldReturnSetValue()
        {
            NewsCategoryType category = NewsCategoryType.Breaking;
            NewsWebModel item = new NewsWebModel();

            item.Category = category;
            Assert.AreEqual(category, item.Category);
        }

        [Test]
        public void Contetn_GetShouldReturnSetValue()
        {
            string content = "content";
            NewsWebModel item = new NewsWebModel();

            item.Content = content;
            Assert.AreEqual(content, item.Content);
        }

        [Test]
        public void IsAddedByAdmin_GetShouldReturnSetValue()
        {
            bool isAddedByAdmin = true;
            NewsWebModel item = new NewsWebModel();

            item.IsAddedByAdmin = isAddedByAdmin;
            Assert.AreEqual(isAddedByAdmin, item.IsAddedByAdmin);
        }

        [Test]
        public void Author_GetShouldReturnSetValue()
        {
            UserWebModel author = new UserWebModel();
            NewsWebModel item = new NewsWebModel();

            item.Author = author;
            Assert.AreEqual(author, item.Author);
        }

        [Test]
        public void Image_GetShouldReturnSetValue()
        {
            ImageWebModel image = new ImageWebModel();
            NewsWebModel item = new NewsWebModel();

            item.Image = image;
            Assert.AreEqual(image, item.Image);
        }
    }
}
