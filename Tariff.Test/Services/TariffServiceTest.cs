using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Tariff.Framework.Services;
using Tariff.Framework.Mappers;
using System.Linq;
using Tariff.Framework.Exceptions;

namespace Tariff.Test.Services
{
    public class TariffServiceTest
    {
        protected TariffService TariffServiceUnderTest { get; }
        protected Mock<ILogger<TariffService>> LoggerMock { get; }
        protected MapperConfiguration MappingConfig { get; }
        protected IMapper Mapper { get; }

        public TariffServiceTest()
        {
            LoggerMock = new Mock<ILogger<TariffService>>();
            MappingConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            Mapper = MappingConfig.CreateMapper();
            TariffServiceUnderTest = new TariffService(Mapper, LoggerMock.Object);
        }

        public class Tariff : TariffServiceTest
        {
            [Fact]
            public async Task Get_WhenCalled_ReturnsAllItems()
            {
                // Arange
                string consumption = "3500";

                // Act
                var result = await TariffServiceUnderTest.GetProducts(consumption);

                // Assert
                Assert.Equal(2, result.Count());
            }

            [Fact]
            public void Should_Throw_TariffException_When_Consumption_IsInvalid()
            {
                // Arange
                string consumption = "invalid";

                // Act
                var result = TariffServiceUnderTest.GetProducts(consumption);

                // Assert
                Assert.ThrowsAsync<TariffException>(async () => await result);
            }

            [Theory]
            [InlineData("3500", 830, 800)]
            [InlineData("4500", 1050, 950)]
            [InlineData("6000", 1380, 1400)]
            public async Task GetProducts_WhenCalled_ReturnsRightAnnualCost(string consumption, int expectedAnnualCostProductA, int expectedAnnualCostProductB)
            {
                // Act
                var result = await TariffServiceUnderTest.GetProducts(consumption);

                // Assert
                Assert.Equal(expectedAnnualCostProductA, result.FirstOrDefault(c => c.Name == "basic electricity tariff").AnnualCost);
                Assert.Equal(expectedAnnualCostProductB, result.FirstOrDefault(c => c.Name == "Packaged tariff").AnnualCost);
            }
        }
    }
}
