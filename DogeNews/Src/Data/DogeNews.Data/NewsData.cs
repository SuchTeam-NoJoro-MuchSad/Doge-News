using System;

using DogeNews.Data.Contracts;
using DogeNews.Common.Attributes;
using DogeNews.Common.Validators;

namespace DogeNews.Data
{
    [InRequestScope]
    public class NewsData : INewsData, IDisposable
    {
        private  INewsDbContext context;

        public NewsData(INewsDbContext context)
        {
            this.Context = context;
        }

        private INewsDbContext Context
        {
            set
            {
                Validator.ValidateThatObjectIsNotNull(value, nameof(value));

                this.context = value;
            }
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
