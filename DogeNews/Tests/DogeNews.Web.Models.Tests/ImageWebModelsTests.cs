using NUnit.Framework;

namespace DogeNews.Web.Models.Tests
{
    [TestFixture]
    public class ImageWebModelsTests
    {
        [Test]
        public void Id_GetShouldReturnSetValue()
        {
            int id = 1;
            ImageWebModel image = new ImageWebModel();

            image.Id = id;
            Assert.AreEqual(id, image.Id);
        }

        [Test]
        public void Name_GetShouldReturnSetValue()
        {
            string name = "name";
            ImageWebModel image = new ImageWebModel();

            image.Name = name;
            Assert.AreEqual(name, image.Name);
        }

        [Test]
        public void FullName_GetShouldReturnSetValue()
        {
            string fullname = "fullname";
            ImageWebModel image = new ImageWebModel();

            image.FullName = fullname;
            Assert.AreEqual(fullname, image.FullName);
        }

        [Test]
        public void FileExtension_GetShouldReturnSetValue()
        {
            string fileExtension = ".exe";
            ImageWebModel image = new ImageWebModel();

            image.FileExtention = fileExtension;
            Assert.AreEqual(fileExtension, image.FileExtention);
        }
    }
}
