using System.IO;

using DogeNews.Web.Models;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.User.Admin.Views;
using DogeNews.Web.User.Admin.Views.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.User.Admin.Presenters
{
    public class AdminAddNewsPresenter : Presenter<IAddNewsView>
    {
        private readonly IFileProvider fileProvider;
        private readonly INewsService newsService;

        public AdminAddNewsPresenter(
            IAddNewsView view,
            IFileProvider fileNameProvider,
            INewsService newsService)
            : base(view)
        {
            this.fileProvider = fileNameProvider;
            this.newsService = newsService;

            this.View.AddNewsEvent += this.AddNews;
        }

        private void AddNews(object sender, AdminAddNewsEventArgs e)
        {
            string username = this.HttpContext.Session["Username"].ToString();
            string fileName = this.fileProvider.GetUnique(username);
            string baseImagesPath = "~\\Files\\Images";
            string basePath = this.Server.MapPath(baseImagesPath);
            string userFolderPath = $"{basePath}\\{username}";
            string fullImageName = $"{basePath}\\{username}\\{fileName}";
            var image = new ImageWebModel
            {
                Name = fileName,
                FullName = fullImageName,
                FileExtention = Path.GetExtension(e.Image.FileName)
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