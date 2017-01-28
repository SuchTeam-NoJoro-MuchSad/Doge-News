using System.IO;

using DogeNews.Web.Models;
using DogeNews.Web.MVP.News.Add.EventArguments;
using DogeNews.Web.Services.Contracts;

using WebFormsMvp;

namespace DogeNews.Web.MVP.News.Add
{
    public class AddNewsPresenter : Presenter<IAddNewsView>
    {
        private readonly IFileService fileProvider;
        private readonly INewsService newsService;

        public AddNewsPresenter(
            IAddNewsView view,
            IFileService fileNameProvider,
            INewsService newsService)
                : base(view)
        {
            this.fileProvider = fileNameProvider;
            this.newsService = newsService;

            this.View.AddNews += this.AddNews;
        }

        private void AddNews(object sender, AddNewsEventArgs e)
        {
            string fileExtension = Path.GetExtension(e.Image.FileName);
            string username = this.HttpContext.User.Identity.Name;
            string fileName = this.fileProvider.GetUniqueFileName(username) + fileExtension;
            string baseImagesPath = "~\\Resources\\Images";
            string basePath = this.Server.MapPath(baseImagesPath);
            string userFolderPath = $"{basePath}\\{username}";
            string fullImageName = $"{basePath}\\{username}\\{fileName}";
            var image = new ImageWebModel
            {
                Name = fileName,
                FullName = fullImageName,
                FileExtention = fileExtension
            };
            var newsItem = new NewsWebModel
            {
                Title = e.Title,
                Category = e.Category,
                Content = e.Content,
                IsAddedByAdmin = true,
                Image = image
            };

            this.fileProvider.CreateFile(userFolderPath, fileName);
            e.Image.SaveAs(fullImageName);
            this.newsService.Add(username, newsItem);
        }
    }
}