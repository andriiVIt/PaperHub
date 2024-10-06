using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service;
using Service.DTO.UpdateDto;
using Xunit;
using System.Collections.Generic;
using Service; 
namespace PaperHub.Tests.Controllers
{
    public class OrderEntryControllerTests
    {
        // Mocking the IOrderEntryService
        private readonly Mock<IOrderEntryService> _mockOrderEntryService;
        // Instance of the OrderEntryController
        private readonly OrderEntryController _controller;

        public OrderEntryControllerTests()
        {
            _mockOrderEntryService = new Mock<IOrderEntryService>();
            _controller = new OrderEntryController(_mockOrderEntryService.Object, null);
        }

        // Test to verify that GetAllOrderEntries returns a list of order entries
        [Fact]
        public void GetAllOrderEntries_ReturnsListOfOrderEntries()
        {
            // Arrange: Setting up test data
            var orderEntries = new List<GetOrderEntryDto>
            {
                new GetOrderEntryDto { Id = 1, Quantity = 5, ProductId = 101, OrderId = 201 },
                new GetOrderEntryDto { Id = 2, Quantity = 3, ProductId = 102, OrderId = 202 }
            };

            // Setting up the mock service to return the list of order entries
            _mockOrderEntryService.Setup(service => service.GetAllOrderEntries())
                .Returns(orderEntries);

            // Act: Calling the method under test
            var result = _controller.GetAllOrderEntries();

            // Assert: Verifying that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifying that the value inside OkObjectResult is the expected list of order entries
            var returnValue = Assert.IsType<List<GetOrderEntryDto>>(okResult.Value);
            // Checking that the list contains the expected number of order entries
            Assert.Equal(2, returnValue.Count);
        }

        // Test to verify that GetOrderEntryById returns an order entry when it exists
        [Fact]
        public void GetOrderEntryById_ReturnsOrderEntry_WhenOrderEntryExists()
        {
            // Arrange: Setting up test data
            var orderEntryId = 1;
            var expectedOrderEntry = new GetOrderEntryDto { Id = orderEntryId, Quantity = 5, ProductId = 101, OrderId = 201 };

            // Setting up the mock service to return the expected order entry when GetOrderEntryById is called
            _mockOrderEntryService.Setup(service => service.GetOrderEntryById(orderEntryId))
                .Returns(expectedOrderEntry);

            // Act: Calling the method under test
            var result = _controller.GetOrderEntryById(orderEntryId);

            // Assert: Verifying that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifying that the value inside OkObjectResult is the expected order entry
            var returnValue = Assert.IsType<GetOrderEntryDto>(okResult.Value);
            // Checking that the order entry's properties match the expected result
            Assert.Equal(expectedOrderEntry.Quantity, returnValue.Quantity);
            Assert.Equal(expectedOrderEntry.ProductId, returnValue.ProductId);
            Assert.Equal(expectedOrderEntry.OrderId, returnValue.OrderId);
        }

        // Test to verify that GetOrderEntryById returns NotFound when the order entry doesn't exist
        [Fact]
        public void GetOrderEntryById_ReturnsNotFound_WhenOrderEntryDoesNotExist()
        {
            // Arrange: Setting up the mock service to return null when GetOrderEntryById is called
            var orderEntryId = 1;
            _mockOrderEntryService.Setup(service => service.GetOrderEntryById(orderEntryId))
                .Returns((GetOrderEntryDto)null);

            // Act: Calling the method under test
            var result = _controller.GetOrderEntryById(orderEntryId);

            // Assert: Verifying that the result is of type NotFoundResult
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Test to verify that CreateOrderEntry returns Ok after creating an order entry
        [Fact]
        public void CreateOrderEntry_ReturnsOk()
        {
             
            var newOrderEntry = new CreateOrderEntryDto 
            { 
                Quantity = 5, 
                ProductId = 101   
            };

             
            var result = _controller.CreateOrderEntry(newOrderEntry);

            
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }
        // Test to verify that UpdateOrderEntry returns Ok after updating an order entry
        [Fact]
        public void UpdateOrderEntry_ReturnsOk()
        {
            // Arrange: Setting up test data
            var orderEntryId = 1;
            var updateOrderEntryDto = new UpdateOrderEntryDto { Quantity = 10, ProductId = 101, OrderId = 201 };

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.UpdateOrderEntry(orderEntryId, updateOrderEntryDto);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test to verify that DeleteOrderEntry returns Ok after deleting an order entry
        [Fact]
        public void DeleteOrderEntry_ReturnsOk()
        {
            // Arrange: Setting up test data
            var orderEntryId = 1;

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.DeleteOrderEntry(orderEntryId);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
