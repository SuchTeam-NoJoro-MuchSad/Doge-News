using System;

using DogeNews.Data.Contracts;
using DogeNews.Common.Attributes;

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
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

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
