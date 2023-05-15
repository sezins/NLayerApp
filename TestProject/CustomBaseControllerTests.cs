using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nlayer.Apý.Controllers;
using Nlayer.Core.DTOs;
using Nlayer.Core.Entity;
using Nlayer.Core.Repositories;
using Nlayer.Core.Service;

namespace TestProject
{
    public class CustomBaseControllerTests
    {
        [Fact]
        public void CreateActionResult_ReturnsObjectResult()
        {
            // Arrange
            var controller = new CustomBaseController();
            var response = new CustomResponseDto<string>
            {
                StatusCode = 200,
                Data = "Test data"
            };

            // Act
            var result = controller.CreateActionResult(response);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(response, objectResult.Value);
            Assert.Equal(response.StatusCode, objectResult.StatusCode);
        }

        [Fact]
        public void CreateActionResult_ReturnsNullObjectResult()
        {
            // Arrange
            var controller = new CustomBaseController();
            var response = new CustomResponseDto<string>
            {
                StatusCode = 204
            };

            // Act
            var result = controller.CreateActionResult(response);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Null(objectResult.Value);
            Assert.Equal(response.StatusCode, objectResult.StatusCode);
        }
    }
}