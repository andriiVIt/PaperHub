using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service;
using Service.DTO.UpdateDto;
using Xunit;
using System.Collections.Generic;

namespace PaperHub.Tests.Controllers
{
    public class PropertyControllerTests
    {
        // Mocking the IPropertyService
        private readonly Mock<IPropertyService> _mockPropertyService;
        // Instance of the PropertyController
        private readonly PropertyController _controller;

        public PropertyControllerTests()
        {
            _mockPropertyService = new Mock<IPropertyService>();
            _controller = new PropertyController(_mockPropertyService.Object, null);
        }

        // Test to verify that GetAllProperties returns a list of properties
        [Fact]
        public void GetAllProperties_ReturnsListOfProperties()
        {
            // Arrange: Setting up test data
            var properties = new List<GetPropertyDto>
            {
                new GetPropertyDto { Id = 1, PropertyName = "Water-Resistant" },
                new GetPropertyDto { Id = 2, PropertyName = "Recycled" }
            };

            // Setting up the mock service to return the list of properties
            _mockPropertyService.Setup(service => service.GetAllProperties())
                .Returns(properties);

            // Act: Calling the method under test
            var result = _controller.GetAllProperties();

            // Assert: Verifying that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifying that the value inside OkObjectResult is the expected list of properties
            var returnValue = Assert.IsType<List<GetPropertyDto>>(okResult.Value);
            // Checking that the list contains the expected number of properties
            Assert.Equal(2, returnValue.Count);
        }

        // Test to verify that GetPropertyById returns a property when it exists
        [Fact]
        public void GetPropertyById_ReturnsProperty_WhenPropertyExists()
        {
            // Arrange: Setting up test data
            var propertyId = 1;
            var expectedProperty = new GetPropertyDto { Id = propertyId, PropertyName = "Water-Resistant" };

            // Setting up the mock service to return the expected property when GetPropertyById is called
            _mockPropertyService.Setup(service => service.GetPropertyById(propertyId))
                .Returns(expectedProperty);

            // Act: Calling the method under test
            var result = _controller.GetPropertyById(propertyId);

            // Assert: Verifying that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifying that the value inside OkObjectResult is the expected property
            var returnValue = Assert.IsType<GetPropertyDto>(okResult.Value);
            // Checking that the property's properties match the expected result
            Assert.Equal(expectedProperty.PropertyName, returnValue.PropertyName);
        }

        // Test to verify that GetPropertyById returns NotFound when the property doesn't exist
        [Fact]
        public void GetPropertyById_ReturnsNotFound_WhenPropertyDoesNotExist()
        {
            // Arrange: Setting up the mock service to return null when GetPropertyById is called
            var propertyId = 1;
            _mockPropertyService.Setup(service => service.GetPropertyById(propertyId))
                .Returns((GetPropertyDto)null);

            // Act: Calling the method under test
            var result = _controller.GetPropertyById(propertyId);

            // Assert: Verifying that the result is of type NotFoundResult
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Test to verify that CreateProperty returns Ok after creating a property
        [Fact]
        public void CreateProperty_ReturnsOk()
        {
            // Arrange: Setting up test data
            var newProperty = new CreatePropertyDto
            {
                PropertyName = "Eco-Friendly"
            };

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.CreateProperty(newProperty);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test to verify that UpdateProperty returns Ok after updating a property
        [Fact]
        public void UpdateProperty_ReturnsOk()
        {
            // Arrange: Setting up test data
            var propertyId = 1;
            var updatePropertyDto = new UpdatePropertyDto
            {
                PropertyName = "Recycled"
            };

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.UpdateProperty(propertyId, updatePropertyDto);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test to verify that DeleteProperty returns Ok after deleting a property
        [Fact]
        public void DeleteProperty_ReturnsOk()
        {
            // Arrange: Setting up test data
            var propertyId = 1;

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.DeleteProperty(propertyId);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
