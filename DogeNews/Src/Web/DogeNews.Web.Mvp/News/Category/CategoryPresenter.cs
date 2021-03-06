﻿using DogeNews.Common.Validators;
using DogeNews.Services.Data.Contracts;
using DogeNews.Services.Http.Contracts;
using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Category
{
    public class CategoryPresenter : Presenter<ICategoryView>
    {
        private INewsService newsService;
        private IHttpContextService httpContextService;

        public CategoryPresenter(ICategoryView view, INewsService newsService, IHttpContextService httpContextService) 
            : base(view)
        {
            Validator.ValidateThatObjectIsNotNull(newsService, nameof(newsService));
            Validator.ValidateThatObjectIsNotNull(httpContextService, nameof(httpContextService));

            this.newsService = newsService;
            this.httpContextService = httpContextService;
        }
    }
}