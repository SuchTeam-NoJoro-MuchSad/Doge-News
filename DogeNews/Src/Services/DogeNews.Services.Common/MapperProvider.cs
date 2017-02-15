using AutoMapper;

using DogeNews.Common.Attributes;
using DogeNews.Services.Common.Contracts;

namespace DogeNews.Services.Common
{
    [InSingletonScope]
    public class MapperProvider : IMapperProvider
    {
        public IConfigurationProvider Configuration { get; set; }

        public IMapper Instance { get; set; }
    }
}
