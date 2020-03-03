using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Tariff.Comparison.Controllers;
using Tariff.Framework.Mappers;
using Tariff.Framework.Models;
using Tariff.Framework.Services;
using Tariff.Framework.Services.Interface;
using Xunit;

namespace Tariff.Test.Controllers
{
    public class TariffControllerTest
    {
        readonly TariffController _controller;
        protected Mock<ILogger<TariffService>> LoggerMock { get; }
        protected MapperConfiguration MappingConfig { get; }
        protected IMapper Mapper { get; }

        public TariffControllerTest()
        {
            LoggerMock = new Mock<ILogger<TariffService>>();
            MappingConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            Mapper = MappingConfig.CreateMapper();

            ITariffService provider = new TariffService(Mapper, LoggerMock.Object);

            _controller = new TariffController(provider);
        }


        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            const string usage = "3500";
            // Act
            var okResult = await _controller.GetProducts(usage);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            const string usage = "3500";

            // Act
            var okResult = await _controller.GetProducts(usage) as OkObjectResult;

            // Assert
            var items = Assert.IsAssignableFrom<IOrderedEnumerable<TariffResult>>(okResult?.Value);
            Assert.Equal(2, items.Count());
        }


        [Fact]
        public async Task Get_WhenCalled_WithEmptyParameter_ReturnsBadRequest()
        {
            // Arrange
            const string usage = "";
            // Act
            var badResponse = await _controller.GetProducts(usage);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }
    }
}
