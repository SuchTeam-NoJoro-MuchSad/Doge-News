using System.IO;

using DogeNews.Web.Models;
using DogeNews.Web.Mvp.News.Edit.EventArguments;
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services.Contracts.Http;
using DogeNews.Common.Validators;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Edit
{
    public class EditArticlePresenter : Presenter<IEditArticleView>
    {
        private const string ArticleEditQueryParamId = "id";

        private IArticleManagementService articleManagementService;
        private INewsService newsService;
        private IHttpUtilityService httpUtilityService;
        private IHttpContextService httpContextService;
        private IFileService fileService;
        private IHttpServerUtilityService httpServerService;
        private IHttpPostedFileService httpPostedFileService;

        public EditArticlePresenter(
            IEditArticleView view,
            IArticleManagementService articleManagementService,
            INewsService newsService,
            IHttpUtilityService httpUtilityService,
            IHttpContextService httpContextService,
            IFileService fileService,
            IHttpServerUtilityService httpServerService,
            IHttpPostedFileService httpPostedFileService)
            : base(view)
        {
            Validator.ValidateThatObjectIsNotNull(articleManagementService, nameof(articleManagementService));
            Validator.ValidateThatObjectIsNotNull(newsService, nameof(newsService));
            Validator.ValidateThatObjectIsNotNull(httpUtilityService, nameof(httpUtilityService));
            Validator.ValidateThatObjectIsNotNull(httpContextService, nameof(httpContextService));
            Validator.ValidateThatObjectIsNotNull(fileService, nameof(fileService));
            Validator.ValidateThatObjectIsNotNull(httpServerService, nameof(httpServerService));
            Validator.ValidateThatObjectIsNotNull(httpPostedFileService, nameof(httpPostedFileService));

            this.articleManagementService = articleManagementService;
            this.newsService = newsService;
            this.httpUtilityService = httpUtilityService;
            this.httpContextService = httpContextService;
            this.fileService = fileService;
            this.httpServerService = httpServerService;
            this.httpPostedFileService = httpPostedFileService;

            this.View.PreInitPageEvent += PagePreInt;
            this.View.EditArticleButtonClick += EditArticle;
        }

        private void PagePreInt(object sender, PreInitPageEventArgs e)
        {
            var parsedQueryString = this.httpUtilityService.ParseQueryString(e.QueryString);
            var id = parsedQueryString[ArticleEditQueryParamId];
            this.View.Model.NewsItem = this.newsService.GetItemById(id);
        }

        private void EditArticle(object sender, EditArticleEventArgs e)
        {
            string username = this.httpContextService.GetUsername(this.HttpContext);

            var model = new NewsWebModel
            {
                Id = e.Id,
                Category = e.Category,
                Content = e.Content,
                Title = e.Title
            };

            if (e.Image.ContentLength != 0)
            {
                string fileExtension = Path.GetExtension(e.FileName);
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

                this.fileService.CreateFile(userFolderPath, fileName);
                this.httpPostedFileService.SaveAs(e.Image, fullImageName);

                model.Image = image;
            }

            this.articleManagementService.Update(model);
        }
    }
}