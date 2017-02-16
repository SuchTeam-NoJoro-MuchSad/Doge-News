using System.Collections.Specialized;
using DogeNews.Web.Models;
using DogeNews.Web.Mvp.News.Edit.EventArguments;
using DogeNews.Common.Validators;
using DogeNews.Services.Http.Contracts;
using DogeNews.Services.Common.Contracts;
using DogeNews.Services.Data.Contracts;
using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Edit
{
    public class EditArticlePresenter : Presenter<IEditArticleView>
    {
        private const string ArticleEditQueryParamId = "id";
        private const string BaseImagesPath = "~\\Resources\\Images";

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

        public void PagePreInt(object sender, PreInitPageEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, "preInitPageEventArgs");

            NameValueCollection parsedQueryString = this.httpUtilityService.ParseQueryString(e.QueryString);
            int id = int.Parse(parsedQueryString[ArticleEditQueryParamId]);

            this.View.Model.NewsItem = this.newsService.GetItemById(id);
        }

        public void EditArticle(object sender, EditArticleEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, "editArticleEventArgs");

            string username = this.httpContextService.GetUsername(this.HttpContext);
            NewsWebModel model = new NewsWebModel
            {
                Id = e.Id,
                Category = e.Category,
                Content = e.Content,
                Title = e.Title
            };

            if (e.Image.ContentLength > 0)
            {
                string fileExtension = this.fileService.GetFileExtension(e.FileName);
                string fileName = this.fileService.GetUniqueFileName(username) + fileExtension;
                string baseImagesPath = BaseImagesPath;
                string basePath = this.httpServerService.MapPath(baseImagesPath);
                string userFolderPath = $"{basePath}\\{username}";
                string fullImageName = $"{basePath}\\{username}\\{fileName}";

                ImageWebModel image = new ImageWebModel
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