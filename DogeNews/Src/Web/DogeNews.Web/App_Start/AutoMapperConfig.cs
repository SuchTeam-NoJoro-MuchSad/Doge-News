using AutoMapper;
using DogeNews.Services.Common.Contracts;
using DogeNews.Web.Infrastructure.Bindings;
using DogeNews.Web.Infrastructure.Mappings;
using Ninject;

namespace DogeNews.Web
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            IMapperProvider mapperProvider = NinjectWebCommon.Kernel.Get<IMapperProvider>();
            IMapper mapper = MappingsConfig.Map();

            mapperProvider.Instance = mapper;
            mapperProvider.Configuration = MappingsConfig.Configuration;
        }
    }
}