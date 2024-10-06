using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service;
using Service.DTO.UpdateDto;
using Xunit;
using System.Collections.Generic;

namespace PaperHub.Tests.Controllers
{
    public class OrderControllerTests
    {
        // Mocking the IOrderService
        private readonly Mock<IOrderService> _mockOrderService;
        // Instance of the OrderController
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrderController(_mockOrderService.Object, null);
        }

        // Test to verify that GetOrderById returns an order when it exists
        [Fact]
        public void GetOrderById_ReturnsOrder_WhenOrderExists()
        {
            // Arrange: Setting up test data
            var orderId = 1;
            var expectedOrder = new GetOrderDto
            {
                Id = orderId,
                OrderDate = DateTime.Now,
                DeliveryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                Status = "Processing",
                TotalAmount = 150.75,
                CustomerId = 123
            };

            // Setting up the mock service to return the expected order when GetOrderById is called
            _mockOrderService.Setup(service => service.GetOrderById(orderId))
                .Returns(expectedOrder);

            // Act: Calling the method under test
            var result = _controller.GetOrderById(orderId);

            // Assert: Verifying that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifying that the value inside OkObjectResult is the expected order
            var returnValue = Assert.IsType<GetOrderDto>(okResult.Value);
            // Checking that the order's properties match the expected result
            Assert.Equal(expectedOrder.Status, returnValue.Status);
            Assert.Equal(expectedOrder.TotalAmount, returnValue.TotalAmount);
            Assert.Equal(expectedOrder.CustomerId, returnValue.CustomerId);
        }

        // Test to verify that GetOrderById returns NotFound when order doesn't exist
        [Fact]
        public void GetOrderById_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange: Setting up the mock service to return null when GetOrderById is called
            var orderId = 1;
            _mockOrderService.Setup(service => service.GetOrderById(orderId))
                .Returns((GetOrderDto)null);

            // Act: Calling the method under test
            var result = _controller.GetOrderById(orderId);

            // Assert: Verifying that the result is of type NotFoundResult
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Test to verify that GetAllOrders returns a list of orders
        [Fact]
        public void GetAllOrders_ReturnsListOfOrders()
        {
            // Arrange: Setting up test data
            var orders = new List<GetOrderDto>
            {
                new GetOrderDto { Id = 1, OrderDate = DateTime.Now, DeliveryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2)), Status = "Shipped", TotalAmount = 120.50, CustomerId = 101 },
                new GetOrderDto { Id = 2, OrderDate = DateTime.Now, DeliveryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Status = "Processing", TotalAmount = 200.75, CustomerId = 102 }
            };

            // Setting up the mock service to return the list of orders
            _mockOrderService.Setup(service => service.GetAllOrders(10, 0))
                .Returns(orders);

            // Act: Calling the method under test
            var result = _controller.GetAllOrders();

            // Assert: Verifying that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifying that the value inside OkObjectResult is the expected list of orders
            var returnValue = Assert.IsType<List<GetOrderDto>>(okResult.Value);
            // Checking that the list contains the expected number of orders
            Assert.Equal(2, returnValue.Count);
        }

        // Test to verify that CreateOrder returns Ok after creating an order
        [Fact]
        public void CreateOrder_ReturnsOk()
        {
            // Arrange: Setting up test data
            var newOrder = new CreateOrderDto { TotalAmount = 300.99, /* Add other properties if needed */ };

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.CreateOrder(newOrder);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test to verify that UpdateOrder returns Ok after updating an order
        [Fact]
        public void UpdateOrder_ReturnsOk()
        {
            // Arrange: Setting up test data
            var orderId = 1;
            var updateOrderDto = new UpdateOrderDto { TotalAmount = 150.75, /* Add other properties if needed */ };

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.UpdateOrder(orderId, updateOrderDto);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test to verify that DeleteOrder returns Ok after deleting an order
        [Fact]
        public void DeleteOrder_ReturnsOk()
        {
            // Arrange: Setting up test data
            var orderId = 1;

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.DeleteOrder(orderId);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
