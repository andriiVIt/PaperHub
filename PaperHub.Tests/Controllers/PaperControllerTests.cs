using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service;
using Service.DTO.UpdateDto;
using Xunit;
using System.Collections.Generic;

namespace PaperHub.Tests.Controllers
{
    public class PaperControllerTests
    {
        // Mocking the IPaperService
        private readonly Mock<IPaperService> _mockPaperService;
        // Instance of the PaperController
        private readonly PaperController _controller;

        public PaperControllerTests()
        {
            _mockPaperService = new Mock<IPaperService>();
            _controller = new PaperController(_mockPaperService.Object, null);
        }

        // Test to verify that GetAllPapers returns a list of papers
        [Fact]
        public void GetAllPapers_ReturnsListOfPapers()
        {
            // Arrange: Setting up test data
            var papers = new List<GetPaperDto>
            {
                new GetPaperDto { Id = 1, Name = "A4 Paper", Price = 10.50, Discontinued = false },
                new GetPaperDto { Id = 2, Name = "A3 Paper", Price = 15.75, Discontinued = false }
            };

            // Setting up the mock service to return the list of papers
            _mockPaperService.Setup(service => service.GetAllPapers(10, 0))
                .Returns(papers);

            // Act: Calling the method under test
            var result = _controller.GetAllPapers();

            // Assert: Verifying that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifying that the value inside OkObjectResult is the expected list of papers
            var returnValue = Assert.IsType<List<GetPaperDto>>(okResult.Value);
            // Checking that the list contains the expected number of papers
            Assert.Equal(2, returnValue.Count);
        }

        // Test to verify that GetPaperById returns a paper when it exists
        [Fact]
        public void GetPaperById_ReturnsPaper_WhenPaperExists()
        {
            // Arrange: Setting up test data
            var paperId = 1;
            var expectedPaper = new GetPaperDto { Id = paperId, Name = "A4 Paper", Price = 10.50, Discontinued = false };

            // Setting up the mock service to return the expected paper when GetPaperById is called
            _mockPaperService.Setup(service => service.GetPaperById(paperId))
                .Returns(expectedPaper);

            // Act: Calling the method under test
            var result = _controller.GetPaperById(paperId);

            // Assert: Verifying that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifying that the value inside OkObjectResult is the expected paper
            var returnValue = Assert.IsType<GetPaperDto>(okResult.Value);
            // Checking that the paper's properties match the expected result
            Assert.Equal(expectedPaper.Name, returnValue.Name);
            Assert.Equal(expectedPaper.Price, returnValue.Price);
            Assert.Equal(expectedPaper.Discontinued, returnValue.Discontinued);
        }

        // Test to verify that GetPaperById returns NotFound when the paper doesn't exist
        [Fact]
        public void GetPaperById_ReturnsNotFound_WhenPaperDoesNotExist()
        {
            // Arrange: Setting up the mock service to return null when GetPaperById is called
            var paperId = 1;
            _mockPaperService.Setup(service => service.GetPaperById(paperId))
                .Returns((GetPaperDto)null);

            // Act: Calling the method under test
            var result = _controller.GetPaperById(paperId);

            // Assert: Verifying that the result is of type NotFoundResult
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Test to verify that CreatePaper returns Ok after creating a paper
        [Fact]
        public void CreatePaper_ReturnsOk()
        {
            // Arrange: Setting up test data
            var newPaper = new CreatePaperDto 
            { 
                Name = "A5 Paper", 
                Price = 8.50, 
                Discontinued = false,
                PaperProperties = new List<CreatePaperPropertyDto>()  // Example of paper properties if needed
            };

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.CreatePaper(newPaper);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test to verify that UpdatePaper returns Ok after updating a paper
        [Fact]
        public void UpdatePaper_ReturnsOk()
        {
            // Arrange: Setting up test data
            var paperId = 1;
            var updatePaperDto = new UpdatePaperDto { Name = "A4 Paper", Price = 12.00, Discontinued = true };

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.UpdatePaper(paperId, updatePaperDto);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test to verify that DeletePaper returns Ok after deleting a paper
        [Fact]
        public void DeletePaper_ReturnsOk()
        {
            // Arrange: Setting up test data
            var paperId = 1;

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.DeletePaper(paperId);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
