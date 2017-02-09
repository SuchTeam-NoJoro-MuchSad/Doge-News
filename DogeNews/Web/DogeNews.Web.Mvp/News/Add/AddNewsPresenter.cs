using System;
using System.IO;

using DogeNews.Web.Models;
using DogeNews.Web.Mvp.News.Add.EventArguments;
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services.Contracts.Http;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Add
{
    public class AddNewsPresenter : Presenter<IAddNewsView>
    {
        private readonly IFileService fileService;
        private readonly IArticleManagementService articleManagementService;
        private readonly IHttpContextService httpContextService;
        private readonly IHttpPostedFileService httpPostedFileService;
        private readonly IHttpServerUtilityService httpServerService;

        public AddNewsPresenter(
            IAddNewsView view,
            IFileService fileService,
            IArticleManagementService articleManagementService,
            IHttpContextService httpContextService,
            IHttpPostedFileService httpPostedFileService,
            IHttpServerUtilityService httpServerService)
                : base(view)
        {
            this.ValidateConstructorParams(fileService, articleManagementService, httpContextService, httpPostedFileService, httpServerService);

            this.fileService = fileService;
            this.articleManagementService = articleManagementService;
            this.httpContextService = httpContextService;
            this.httpPostedFileService = httpPostedFileService;
            this.httpServerService = httpServerService;

            this.View.AddNews += this.AddNews;
        }

        public void AddNews(object sender, AddNewsEventArgs e)
        {
            string fileExtension = Path.GetExtension(e.FileName);
            string username = this.httpContextService.GetUsername(this.HttpContext);
            string fileName = this.fileService.GetUniqueFileName(username) + fileExtension;
            string baseImagesPath = "~\\Resources\\Images";
            string basePath = this.httpServerService.MapPath(baseImagesPath);
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
            
            this.fileService.CreateFile(userFolderPath, fileName);
            this.httpPostedFileService.SaveAs(e.Image, fullImageName);
            this.articleManagementService.Add(username, newsItem);
        }

        private void ValidateConstructorParams(
            IFileService fileService,
            IArticleManagementService articleManagementService,
            IHttpContextService httpContextService,
            IHttpPostedFileService httpPostedFileService,
            IHttpServerUtilityService httpServerService)
        {
            if (fileService == null)
            {
                throw new ArgumentNullException("fileService");
            }

            if (articleManagementService == null)
            {
                throw new ArgumentNullException("newsService");
            }

            if (httpContextService == null)
            {
                throw new ArgumentNullException("httpContextService");
            }

            if (httpPostedFileService == null)
            {
                throw new ArgumentNullException("httpPostedFileService");
            }

            if (httpServerService == null)
            {
                throw new ArgumentNullException("httpServerService");
            }
        }
    }
}