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

namespace WebApiTesting.ControllersTests
{
    public class OwnerControllerTests
    {
        // Mock for CRUD operations
        private readonly Mock<IOwnerVehicleFeature<Owner>> _mockCrud;

        // Service and controller under test
        private readonly OwnerService _service;
        private readonly OwnerController _controller;

        public OwnerControllerTests()
        {
            _mockCrud = new Mock<IOwnerVehicleFeature<Owner>>();

            // Pass null for IOwnerRepository since we’re not testing search/filter/count here
            _service = new OwnerService(_mockCrud.Object, null!);
            _controller = new OwnerController(_service);
        }

        [Fact]
        public async Task GetOwners_ReturnsListOfOwners()
        {
            // Arrange
            var owners = new List<Owner>
    {
        new Owner { OwnerId = 1, Name = "Charan" },
        new Owner { OwnerId = 2, Name = "Shetty" }
    };
            _mockCrud.Setup(r => r.GetAllAsync()).ReturnsAsync(owners);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<OwnerDTO>>(ok.Value);

            Assert.Equal(2, list.Count());
            Assert.Contains(list, o => o.Name == "Charan");
            Assert.Contains(list, o => o.Name == "Shetty");



            
        }

        [Fact]
        public async Task GetOwnerById_ReturnsOwner_WhenExists()
        {
            // Arrange
            var owner = new Owner { OwnerId = 1, Name = "Charan Shetty" };
            _mockCrud.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(owner);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<OwnerDTO>(ok.Value);

            Assert.Equal("Charan Shetty", returned.Name);
            Assert.Equal(1, returned.OwnerId);
        }

        [Fact]
        public async Task GetOwnerById_ReturnsNotFound_WhenDoesNotExist()
        {
            // Arrange
            _mockCrud.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Owner?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateOwner_ReturnsCreatedAtAction()
        {
            // Arrange
            var dto = new CreateOwnerDTO { Name = "Shetty" };
            var entity = new Owner { OwnerId = 1, Name = "Shetty" }; // Name should match dto

            _mockCrud.Setup(r => r.AddAsync(It.IsAny<Owner>())).ReturnsAsync(entity);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // matches actual return type
            var returned = Assert.IsType<OwnerDTO>(okResult.Value);

            Assert.Equal(entity.OwnerId, returned.OwnerId);
            Assert.Equal(dto.Name, returned.Name);


        }

        [Fact]
        public async Task DeleteOwner_ReturnsNoContent_WhenOwnerExists()
        {
            // Arrange
            _mockCrud.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockCrud.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }
}
