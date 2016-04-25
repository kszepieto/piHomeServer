using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using piHome.WebHost.Infrastructure.Mapping;

namespace PiHome.WebHost.Tests
{
    [TestClass]
    public class AutoMapperTests
    {
        [TestMethod]
        public void AssertConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}
