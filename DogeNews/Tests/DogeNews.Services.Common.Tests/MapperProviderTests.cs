using AutoMapper;
using NUnit.Framework;

namespace DogeNews.Services.Common.Tests
{
    [TestFixture]
    public class MapperProviderTests
    {
        [Test]
        public void Instance_ShouldBeOfTypeMapper()
        {
            MapperProvider mapperProvider = new MapperProvider();

            mapperProvider.Instance =
                new Mapper(new MapperConfiguration(delegate (IMapperConfigurationExpression expression) { }));

            IMapper mapperProviderInstance = mapperProvider.Instance;

            Assert.AreEqual(mapperProviderInstance.GetType(), typeof(Mapper));
        }
    }
}
