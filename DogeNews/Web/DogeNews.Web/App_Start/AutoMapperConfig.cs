using DogeNews.Services.Common.Contracts;
using DogeNews.Web.Infrastructure.Bindings;
using DogeNews.Web.Infrastructure.Mappings;

using Ninject;

namespace DogeNews.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            var mapperProvider = NinjectWebCommon.Kernel.Get<IMapperProvider>();
            var mapper = MappingsConfig.Map();

            mapperProvider.Instance = mapper;
            mapperProvider.Configuration = MappingsConfig.Configuration;
        }
    }
}