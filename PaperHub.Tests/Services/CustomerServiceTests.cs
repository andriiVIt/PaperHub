using Microsoft.EntityFrameworkCore;
using Xunit;
using DataAccess;
using DataAccess.Models;
using Service;
using Service.DTO.UpdateDto;
using System.Collections.Generic;

namespace PaperHub.Tests.Services
{
    public class CustomerServiceTests : IDisposable
    {
        private readonly CustomerService _customerService;
        private readonly MyDbContext _dbContext;

        public CustomerServiceTests()
        {
            // Setting up InMemoryDatabase options
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            // Creating an instance of the in-memory database context
            _dbContext = new MyDbContext(options);
            _customerService = new CustomerService(new CustomerRepo(_dbContext), _dbContext);
        }

        // Test to check if GetAllCustomers returns the correct number of customers
        [Fact]
        public void GetAllCustomers_ReturnsCorrectNumberOfCustomers()
        {
            // Arrange: adding test data to the in-memory database
            _dbContext.Customers.AddRange(new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe", Address = "123 Main St", Phone = "123-456-7890", Email = "john@example.com" },
                new Customer { Id = 2, Name = "Jane Doe", Address = "456 Oak St", Phone = "987-654-3210", Email = "jane@example.com" }
            });
            _dbContext.SaveChanges();

            // Act: calling the service method
            var result = _customerService.GetAllCustomers(10, 0);

            // Assert: verifying that the correct number of customers is returned
            Assert.Equal(2, result.Count); // The method should return two customers
        }

        // Test to check if GetCustomerById returns the correct customer when it exists
        [Fact]
        public void GetCustomerById_ReturnsCustomer_WhenCustomerExists()
        {
            // Arrange: adding a test customer to the in-memory database
            _dbContext.Customers.Add(new Customer { Id = 1, Name = "John Doe", Address = "123 Main St", Phone = "123-456-7890", Email = "john@example.com" });
            _dbContext.SaveChanges();

            // Act: calling the service method
            var result = _customerService.GetCustomerById(1);

            // Assert: verifying that the returned customer is not null and has correct properties
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name); // The name should match the test data
        }

        // Test to verify that a customer is successfully created
        [Fact]
        public void CreateCustomer_SuccessfullyAddsCustomer()
        {
            // Arrange: creating a DTO for a new customer
            var createCustomerDto = new CreateCustomerDto { Name = "New Customer", Address = "789 Pine St", Phone = "555-555-5555", Email = "new@example.com" };

            // Act: calling the service method to create a customer
            _customerService.CreateCustomer(createCustomerDto);

            // Assert: verifying that the customer was added to the in-memory database
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Name == "New Customer");
            Assert.NotNull(customer); // The customer should be added
            Assert.Equal("New Customer", customer.Name); // Verifying the name
        }

        // Test to verify that a customer is updated correctly when they exist
        [Fact]
        public void UpdateCustomer_UpdatesCustomer_WhenCustomerExists()
        {
             
            var customer = new Customer { Id = 1, Name = "Test Customer", Email = "test@example.com" };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            var updateDto = new UpdateCustomerDto { Name = "Updated Customer", Email = "updated@example.com" };

             
            _customerService.UpdateCustomer(1, updateDto);

             
            var updatedCustomer = _dbContext.Customers.Find(1);
            Assert.NotNull(updatedCustomer);
            Assert.Equal("Updated Customer", updatedCustomer.Name);
            Assert.Equal("updated@example.com", updatedCustomer.Email);
        }


        // Test to verify that a customer is deleted when they exist
        [Fact]
        public void DeleteCustomer_RemovesCustomer_WhenCustomerExists()
        {
            // Arrange: adding a test customer to the in-memory database
            _dbContext.Customers.Add(new Customer { Id = 1, Name = "Test Customer" });
            _dbContext.SaveChanges();

 
            _customerService.DeleteCustomer(1);

 
            var customer = _dbContext.Customers.Find(1);
            Assert.Null(customer);
        }

        // Dispose method to reset the state of the database after each test
        public void Dispose()
        {
            // Clear the in-memory database after each test
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
