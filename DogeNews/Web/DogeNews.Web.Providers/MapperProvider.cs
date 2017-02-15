using System;
using AutoMapper;

using DogeNews.Common.Attributes;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Providers
{
    [InSingletonScope]
    public class MapperProvider : IMapperProvider
    {
        public MapperConfiguration Configuration { get; set; }

        public IMapper Instance { get; set; }
    }
}
