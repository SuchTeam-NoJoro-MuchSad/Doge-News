using AutoMapper;

using DogeNews.Services.Common;

using NUnit.Framework;

namespace DogeNews.Web.Providers.Tests
{
    [TestFixture]
    public class MapperProviderTests
    {
        [Test]
        public void Instance_ShouldBeOfTypeMapper()
        {
            var mapperProvider = new MapperProvider();

            mapperProvider.Instance =
                new Mapper(new MapperConfiguration(delegate (IMapperConfigurationExpression expression) { }));

            var mapperProviderInstance = mapperProvider.Instance;

            Assert.AreEqual(mapperProviderInstance.GetType(), typeof(Mapper));
        }
    }
}
