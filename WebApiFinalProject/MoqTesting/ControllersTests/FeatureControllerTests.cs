using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiFinalProject.Controllers;
using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;
using WebApiFinalProject.DTOs;
using WebApiFinalProject.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WebApiTesting.ControllersTests
{
    public class FeatureControllerTests
    {
        private readonly Mock<IOwnerVehicleFeature<Feature>> _mockCrud;
        private readonly FeatureService _service;
        private readonly FeatureController _controller;

        public FeatureControllerTests()
        {
            _mockCrud = new Mock<IOwnerVehicleFeature<Feature>>();
            _service = new FeatureService(_mockCrud.Object,null!);
            _controller = new FeatureController(_service);
        }

        [Fact]
        public async Task GetFeatures_ReturnsListOfFeatures()
        {
            // Arrange
            var features = new List<Feature>
    {
        new Feature { Id = 1, Name = "Sunroof", VehicleId = 1 },
        new Feature { Id = 2, Name = "GPS", VehicleId = 2 }
    };
            _mockCrud.Setup(r => r.GetAllAsync()).ReturnsAsync(features);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // check Result
            var returnedList = Assert.IsAssignableFrom<IEnumerable<FeatureDTO>>(okResult.Value);

            Assert.Equal(2, returnedList.Count());
            Assert.Contains(returnedList, f => f.Name == "Sunroof");
            Assert.Contains(returnedList, f => f.Name == "GPS");
        }

        [Fact]
        public async Task GetFeatureById_ReturnsFeature_WhenExists()
        {
            // Arrange
            var feature = new Feature { Id = 1, Name = "Sunroof", VehicleId = 1 };
            _mockCrud.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(feature);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // check Result
            var returned = Assert.IsType<FeatureDTO>(okResult.Value);

            Assert.Equal("Sunroof", returned.Name);
            Assert.Equal(1, returned.Id);
        }

        [Fact]
        public async Task GetFeatureById_ReturnsNotFound_WhenDoesNotExist()
        {
            // Arrange
            _mockCrud.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Feature?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result); // check Result for NotFound
        }

        [Fact]
        public async Task CreateFeature_ReturnsOkWithFeatureDTO()
        {
            // Arrange
            var dto = new CreateFeatureDTO { Name = "Sunroof", Price = 1000 };
            var entity = new Feature { Id = 1, Name = "Sunroof", Price = 1000 };

            _mockCrud.Setup(r => r.AddAsync(It.IsAny<Feature>())).ReturnsAsync(entity);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returned = Assert.IsType<FeatureDTO>(createdAtActionResult.Value);

            Assert.Equal(entity.Id, returned.Id);
            Assert.Equal(dto.Name, returned.Name);
        }

        [Fact]
        public async Task DeleteFeature_ReturnsNoContent_WhenFeatureExists()
        {
            _mockCrud.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
            _mockCrud.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }
}
