using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiFinalProject.Controllers;
using WebApiFinalProject.DTOs;
using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;
using WebApiFinalProject.Services;

namespace WebApiTesting.ControllersTests
{
    public class VehicleControllerTests
    {
        private readonly Mock<IOwnerVehicleFeature<Vehicle>> _mockCrud;
        private readonly VehicleService _service;
        private readonly VehicleController _controller;

        public VehicleControllerTests()
        {
            _mockCrud = new Mock<IOwnerVehicleFeature<Vehicle>>();
            _service = new VehicleService(_mockCrud.Object,null!);
            _controller = new VehicleController(_service);
        }

        [Fact]
        public async Task GetVehicles_ReturnsListOfVehicles()
        {
            // Arrange
            var vehicles = new List<Vehicle>
    {
        new Vehicle { VehicleId = 1, Make = "Tesla" },
        new Vehicle { VehicleId = 2, Make = "BMW" }
    };
            _mockCrud.Setup(r => r.GetAllAsync()).ReturnsAsync(vehicles);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // check Result
            var returnedList = Assert.IsAssignableFrom<IEnumerable<VehicleDTO>>(okResult.Value);

            Assert.Equal(2, returnedList.Count());
            Assert.Contains(returnedList, v => v.Make == "Tesla");
            Assert.Contains(returnedList, v => v.Make == "BMW");
        }

        [Fact]
        public async Task GetVehicleById_ReturnsVehicle_WhenExists()
        {
            // Arrange
            var vehicle = new Vehicle { VehicleId = 1, Make = "Tesla" };
            _mockCrud.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(vehicle);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // check Result
            var returned = Assert.IsType<VehicleDTO>(okResult.Value);

            Assert.Equal("Tesla", returned.Make);
            Assert.Equal(1, returned.VehicleId);
        }

        [Fact]
        public async Task GetVehicleById_ReturnsNotFound_WhenDoesNotExist()
        {
            

            // Arrange
            _mockCrud.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Vehicle?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateVehicle_ReturnsOkWithVehicleDTO()
        {
            // Arrange
            var dto = new CreateVehicleDTO { Make = "Tesla" };
            var entity = new Vehicle { VehicleId = 1, Make = "Tesla" };

            _mockCrud.Setup(r => r.AddAsync(It.IsAny<Vehicle>())).ReturnsAsync(entity);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returned = Assert.IsType<VehicleDTO>(createdAtActionResult.Value);

            Assert.Equal(entity.VehicleId, returned.VehicleId);
            Assert.Equal(dto.Make, returned.Make);




        }

        [Fact]
        public async Task DeleteVehicle_ReturnsNoContent_WhenVehicleExists()
        {
            _mockCrud.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
            _mockCrud.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }
}
