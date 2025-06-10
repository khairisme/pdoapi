using System.Collections.Generic;
using System.Threading.Tasks;
using HR.API.Controllers.PDO;
using HR.Application.DTOs;
using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HR.Test.PDO
{
    public class RujStatusPermohonanControllerTests
    {
        private readonly Mock<IRujStatusPermohonanService> _mockService;
        private readonly Mock<ILogger<RujStatusPermohonanController>> _mockLogger;
        private readonly RujStatusPermohonanController _controller;

        public RujStatusPermohonanControllerTests()
        {
            _mockService = new Mock<IRujStatusPermohonanService>();
            _mockLogger = new Mock<ILogger<RujStatusPermohonanController>>();
            _controller = new RujStatusPermohonanController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task getAll_ReturnsSuccess_WhenItemsExist()
        {
            // Arrange
            var items = new List<RujStatusPermohonanDto>
            {
                new RujStatusPermohonanDto { Kod = "A", Nama = "Test", Keterangan = "Desc" }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(items);

            // Act
            var result = await _controller.getAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic value = okResult.Value;
            Assert.Equal("Sucess", (string)value.status);
            Assert.Equal(items, value.items);
        }

        [Fact]
        public async Task getAll_ReturnsFailed_WhenNoItems()
        {
            // Arrange
            var items = new List<RujStatusPermohonanDto>();
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(items);

            // Act
            var result = await _controller.getAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic value = okResult.Value;
            Assert.Equal("Failed", (string)value.status);
            Assert.Empty(value.items);
        }
    }
}
