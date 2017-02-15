using AutoMapper;

using DogeNews.Services.Common.Contracts;
using DogeNews.Web.Infrastructure.Mappings.Profiles;

namespace DogeNews.Web.Infrastructure.Mappings
{
    public class MappingsConfig
    {
        private readonly IMapperProvider mapperProvider;

        public MappingsConfig(IMapperProvider mapperProvider)
        {
            this.mapperProvider = mapperProvider;
        }

        public static MapperConfiguration Configuration { get; private set; }

        public static IMapper Map()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMappingsProfile>();
            });

            var mapper = config.CreateMapper();
            
            Configuration = config;
            return mapper;
        }
    }
}