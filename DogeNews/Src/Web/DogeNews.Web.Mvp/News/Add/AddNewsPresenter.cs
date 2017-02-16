using System.IO;

using DogeNews.Web.Models;
using DogeNews.Web.Mvp.News.Add.EventArguments;
using DogeNews.Services.Common.Contracts;
using DogeNews.Services.Http.Contracts;
using DogeNews.Common.Validators;
using DogeNews.Services.Data.Contracts;
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
            Validator.ValidateThatObjectIsNotNull(fileService, nameof(fileService));
            Validator.ValidateThatObjectIsNotNull(articleManagementService, nameof(articleManagementService));
            Validator.ValidateThatObjectIsNotNull(httpContextService, nameof(httpContextService));
            Validator.ValidateThatObjectIsNotNull(httpPostedFileService, nameof(httpPostedFileService));
            Validator.ValidateThatObjectIsNotNull(httpServerService, nameof(httpServerService));

            this.fileService = fileService;
            this.articleManagementService = articleManagementService;
            this.httpContextService = httpContextService;
            this.httpPostedFileService = httpPostedFileService;
            this.httpServerService = httpServerService;

            this.View.AddNews += this.AddNews;
        }

        public void AddNews(object sender, AddNewsEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            string fileExtension = Path.GetExtension(e.FileName);
            string username = this.httpContextService.GetUsername(this.HttpContext);
            string fileName = this.fileService.GetUniqueFileName(username) + fileExtension;
            string baseImagesPath = "~\\Resources\\Images";
            string basePath = this.httpServerService.MapPath(baseImagesPath);
            string userFolderPath = $"{basePath}\\{username}";
            string fullImageName = $"{basePath}\\{username}\\{fileName}";
            ImageWebModel image = new ImageWebModel
            {
                Name = fileName,
                FullName = fullImageName,
                FileExtention = fileExtension
            };
            NewsWebModel newsItem = new NewsWebModel
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
    }
}