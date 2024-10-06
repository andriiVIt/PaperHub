using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service;
using Service.DTO.UpdateDto;
using Xunit;
using System.Collections.Generic;

namespace PaperHub.Tests.Controllers
{
    // public class PaperPropertyControllerTests
    // {
    //     // Mocking the IPaperPropertyService
    //     private readonly Mock<IPaperPropertyService> _mockPaperPropertyService;
    //     // Instance of the PaperPropertyController
    //     private readonly PaperPropertyController _controller;
    //
    //     public PaperPropertyControllerTests()
    //     {
    //         _mockPaperPropertyService = new Mock<IPaperPropertyService>();
    //         _controller = new PaperPropertyController(_mockPaperPropertyService.Object, null);
    //     }
    //
    //     // Test to verify that GetAllPaperProperties returns a list of paper properties
    //     [Fact]
    //     public void GetAllPaperProperties_ReturnsListOfPaperProperties()
    //     {
    //         // Arrange: Setting up test data
    //         var paperProperties = new List<GetPaperPropertyDto>
    //         {
    //             new GetPaperPropertyDto 
    //             { 
    //                 PaperId = 1, 
    //                 PropertyId = 101, 
    //                 GetPaper = new GetPaperDto { Id = 1, Name = "A4 Paper" }, 
    //                 GetProperty = new GetPropertyDto { Id = 101, Name = "Recycled" } 
    //             },
    //             new GetPaperPropertyDto 
    //             { 
    //                 PaperId = 2, 
    //                 PropertyId = 102, 
    //                 GetPaper = new GetPaperDto { Id = 2, Name = "A3 Paper" }, 
    //                 GetProperty = new GetPropertyDto { Id = 102, Name = "Glossy" } 
    //             }
    //         };
    //
    //         // Setting up the mock service to return the list of paper properties
    //         _mockPaperPropertyService.Setup(service => service.GetAllPaperProperties())
    //             .Returns(paperProperties);
    //
    //         // Act: Calling the method under test
    //         var result = _controller.GetAllPaperProperties();
    //
    //         // Assert: Verifying that the result is of type OkObjectResult
    //         var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //         // Verifying that the value inside OkObjectResult is the expected list of paper properties
    //         var returnValue = Assert.IsType<List<GetPaperPropertyDto>>(okResult.Value);
    //         // Checking that the list contains the expected number of properties
    //         Assert.Equal(2, returnValue.Count);
    //     }
    //
    //     // Test to verify that GetPaperPropertyById returns a paper property when it exists
    //     [Fact]
    //     public void GetPaperPropertyById_ReturnsPaperProperty_WhenItExists()
    //     {
    //         // Arrange: Setting up test data
    //         var paperId = 1;
    //         var propertyId = 101;
    //         var expectedPaperProperty = new GetPaperPropertyDto
    //         {
    //             PaperId = paperId,
    //             PropertyId = propertyId,
    //             GetPaper = new GetPaperDto { Id = paperId, Name = "A4 Paper" },
    //             GetProperty = new GetPropertyDto { Id = propertyId, Name = "Recycled" }
    //         };
    //
    //         // Setting up the mock service to return the expected paper property
    //         _mockPaperPropertyService.Setup(service => service.GetPaperPropertyById(paperId, propertyId))
    //             .Returns(expectedPaperProperty);
    //
    //         // Act: Calling the method under test
    //         var result = _controller.GetPaperPropertyById(paperId, propertyId);
    //
    //         // Assert: Verifying that the result is of type OkObjectResult
    //         var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //         // Verifying that the value inside OkObjectResult is the expected paper property
    //         var returnValue = Assert.IsType<GetPaperPropertyDto>(okResult.Value);
    //         // Checking that the properties match the expected result
    //         Assert.Equal(expectedPaperProperty.GetPaper.Name, returnValue.GetPaper.Name);
    //         Assert.Equal(expectedPaperProperty.GetProperty.Name, returnValue.GetProperty.Name);
    //     }
    //
    //     // Test to verify that GetPaperPropertyById returns NotFound when the paper property doesn't exist
    //     [Fact]
    //     public void GetPaperPropertyById_ReturnsNotFound_WhenItDoesNotExist()
    //     {
    //         // Arrange: Setting up the mock service to return null when GetPaperPropertyById is called
    //         var paperId = 1;
    //         var propertyId = 101;
    //         _mockPaperPropertyService.Setup(service => service.GetPaperPropertyById(paperId, propertyId))
    //             .Returns((GetPaperPropertyDto)null);
    //
    //         // Act: Calling the method under test
    //         var result = _controller.GetPaperPropertyById(paperId, propertyId);
    //
    //         // Assert: Verifying that the result is of type NotFoundResult
    //         Assert.IsType<NotFoundResult>(result.Result);
    //     }
    //
    //     // Test to verify that CreatePaperProperty returns Ok after creating a paper property
    //     [Fact]
    //     public void CreatePaperProperty_ReturnsOk()
    //     {
    //         // Arrange: Setting up test data
    //         var newPaperProperty = new CreatePaperPropertyDto
    //         {
    //             PropertyId = 101 // Example property ID
    //         };
    //
    //         // No need to mock the service for void methods; we just test the controller's response
    //
    //         // Act: Calling the method under test
    //         var result = _controller.CreatePaperProperty(newPaperProperty);
    //
    //         // Assert: Verifying that the result is of type OkResult
    //         var okResult = Assert.IsType<OkResult>(result);
    //         // Verifying the status code is 200 OK
    //         Assert.Equal(200, okResult.StatusCode);
    //     }
    //
    //     // Test to verify that UpdatePaperProperty returns Ok after updating a paper property
    //     [Fact]
    //     public void UpdatePaperProperty_ReturnsOk()
    //     {
    //         // Arrange: Setting up test data
    //         var updatePaperProperty = new UpdatePaperPropertyDto
    //         {
    //             GetPaper = new GetPaperDto { Id = 1, Name = "A4 Paper" },
    //             GetProperty = new GetPropertyDto { Id = 101, Name = "Recycled" }
    //         };
    //
    //         // No need to mock the service for void methods; we just test the controller's response
    //
    //         // Act: Calling the method under test
    //         var result = _controller.UpdatePaperProperty(updatePaperProperty);
    //
    //         // Assert: Verifying that the result is of type OkResult
    //         var okResult = Assert.IsType<OkResult>(result);
    //         // Verifying the status code is 200 OK
    //         Assert.Equal(200, okResult.StatusCode);
    //     }
    //
    //     // Test to verify that DeletePaperProperty returns Ok after deleting a paper property
    //     [Fact]
    //     public void DeletePaperProperty_ReturnsOk()
    //     {
    //         // Arrange: Setting up test data
    //         var paperId = 1;
    //         var propertyId = 101;
    //
    //         // No need to mock the service for void methods; we just test the controller's response
    //
    //         // Act: Calling the method under test
    //         var result = _controller.DeletePaperProperty(paperId, propertyId);
    //
    //         // Assert: Verifying that the result is of type OkResult
    //         var okResult = Assert.IsType<OkResult>(result);
    //         // Verifying the status code is 200 OK
    //         Assert.Equal(200, okResult.StatusCode);
    //     }
    // }
}
