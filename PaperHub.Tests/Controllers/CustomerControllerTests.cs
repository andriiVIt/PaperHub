using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service;
using Service.DTO.UpdateDto;
using Xunit;
using System.Collections.Generic;

namespace PaperHub.Tests.Controllers
{
    public class CustomerControllerTests
    {
        // Mocking the ICustomerService
        private readonly Mock<ICustomerService> _mockCustomerService;
        // Instance of the CustomerController
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockCustomerService.Object, null);
        }

        // Test to verify that GetCustomerById returns a customer when it exists
        [Fact]
        public void GetCustomerById_ReturnsCustomer_WhenCustomerExists()
        {
            // Arrange: Setting up test data
            var customerId = 1;
            var expectedCustomer = new GetCustomerDto
            {
                Id = customerId,
                Name = "John Doe",
                Address = "123 Main St",
                Phone = "123-456-7890",
                Email = "johndoe@example.com"
            };

            // Setting up the mock service to return the expected customer when GetCustomerById is called
            _mockCustomerService.Setup(service => service.GetCustomerById(customerId))
                .Returns(expectedCustomer);

            // Act: Calling the method under test
            var result = _controller.GetCustomerById(customerId);

            // Assert: Verifying that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifying that the value inside OkObjectResult is the expected customer
            var returnValue = Assert.IsType<GetCustomerDto>(okResult.Value);
            // Checking that the customer's properties match the expected result
            Assert.Equal(expectedCustomer.Name, returnValue.Name);
            Assert.Equal(expectedCustomer.Address, returnValue.Address);
            Assert.Equal(expectedCustomer.Phone, returnValue.Phone);
            Assert.Equal(expectedCustomer.Email, returnValue.Email);
        }

        // Test to verify that GetCustomerById returns NotFound when customer doesn't exist
        [Fact]
        public void GetCustomerById_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange: Setting up the mock service to return null when GetCustomerById is called
            var customerId = 1;
            _mockCustomerService.Setup(service => service.GetCustomerById(customerId))
                .Returns((GetCustomerDto)null);

            // Act: Calling the method under test
            var result = _controller.GetCustomerById(customerId);

            // Assert: Verifying that the result is of type NotFoundResult
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Test to verify that GetAllCustomers returns a list of customers
        [Fact]
        public void GetAllCustomers_ReturnsListOfCustomers()
        {
            // Arrange: Setting up test data
            var customers = new List<GetCustomerDto>
            {
                new GetCustomerDto { Id = 1, Name = "John Doe", Address = "123 Main St", Phone = "123-456-7890", Email = "johndoe@example.com" },
                new GetCustomerDto { Id = 2, Name = "Jane Doe", Address = "456 Elm St", Phone = "987-654-3210", Email = "janedoe@example.com" }
            };

            // Setting up the mock service to return the list of customers
            _mockCustomerService.Setup(service => service.GetAllCustomers(10, 0))
                .Returns(customers);

            // Act: Calling the method under test
            var result = _controller.GetAllCustomers();

            // Assert: Verifying that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifying that the value inside OkObjectResult is the expected list of customers
            var returnValue = Assert.IsType<List<GetCustomerDto>>(okResult.Value);
            // Checking that the list contains the expected number of customers
            Assert.Equal(2, returnValue.Count);
        }

        // Test to verify that CreateCustomer returns Ok after creating a customer
        [Fact]
        public void CreateCustomer_ReturnsOk()
        {
            // Arrange: Setting up test data
            var newCustomer = new CreateCustomerDto { Name = "New Customer", Address = "789 Maple St", Phone = "111-222-3333", Email = "newcustomer@example.com" };

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.CreateCustomer(newCustomer);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test to verify that UpdateCustomer returns Ok after updating a customer
        [Fact]
        public void UpdateCustomer_ReturnsOk()
        {
            // Arrange: Setting up test data
            var customerId = 1;
            var updateCustomerDto = new UpdateCustomerDto { Name = "Updated Customer", Address = "789 Maple St", Phone = "111-222-3333", Email = "updatedcustomer@example.com" };

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.UpdateCustomer(customerId, updateCustomerDto);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test to verify that DeleteCustomer returns Ok after deleting a customer
        [Fact]
        public void DeleteCustomer_ReturnsOk()
        {
            // Arrange: Setting up test data
            var customerId = 1;

            // No need to mock the service for void methods; we just test the controller's response

            // Act: Calling the method under test
            var result = _controller.DeleteCustomer(customerId);

            // Assert: Verifying that the result is of type OkResult
            var okResult = Assert.IsType<OkResult>(result);
            // Verifying the status code is 200 OK
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
