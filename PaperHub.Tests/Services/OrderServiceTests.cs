using System;
using System.Linq;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.DTO.UpdateDto;
using Xunit;

namespace PaperHub.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly MyDbContext _dbContext;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            // Setup in-memory database for testing
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new MyDbContext(options);
            _orderService = new OrderService(new OrderRepo(_dbContext), _dbContext);

            // Clean the database before each test to avoid conflicts
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void GetOrderById_ReturnsCorrectOrder()
        {
            // Arrange: adding a test order to the in-memory database
            var testOrder = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = 100
            };

            _dbContext.Orders.Add(testOrder);
            _dbContext.SaveChanges();

            // Ensure the order is correctly added
            Assert.NotNull(_dbContext.Orders.Find(1));

            // Act: calling the service method to retrieve the order by ID
            var result = _orderService.GetOrderById(1);

            // Assert: verifying that the returned order matches the expected order
            Assert.NotNull(result); // Ensure the result is not null
            Assert.Equal(1, result.Id); // Verify the ID
            Assert.Equal("Pending", result.Status); // Verify the status
            Assert.Equal(100, result.TotalAmount); // Verify the total amount
        }

        [Fact]
public void CreateOrder_SuccessfullyAddsOrderAndOrderEntries()
{
    // Arrange: create and add a customer and products to the in-memory database
    var customer = new Customer { Id = 1, Name = "Test Customer" };
    var product1 = new Paper { Id = 1, Name = "Product 1", Price = 10.0 };
    var product2 = new Paper { Id = 2, Name = "Product 2", Price = 15.0 };
    
    _dbContext.Customers.Add(customer);
    _dbContext.Papers.AddRange(product1, product2);
    _dbContext.SaveChanges();

    // Arrange: create a new order DTO with order entries
    var createOrderDto = new CreateOrderDto
    {
        OrderDate = DateTime.Now,
        Status = "Pending",
        TotalAmount = 200,
        CustomerId = customer.Id, // Use the existing customer
        OrderEntries = new List<CreateOrderEntryDto>
        {
            new CreateOrderEntryDto { Quantity = 10, ProductId = product1.Id },
            new CreateOrderEntryDto { Quantity = 5, ProductId = product2.Id }
        }
    };

    // Act: call the service method to create an order
    _orderService.CreateOrder(createOrderDto);

    // Assert: verify that the order and its entries were added
    var createdOrder = _dbContext.Orders.FirstOrDefault(o => o.TotalAmount == 200);
    Assert.NotNull(createdOrder); // Ensure the order was added
    Assert.Equal(2, _dbContext.OrderEntries.Count()); // Ensure the order entries were added

    // Additional assertions to check specific order entries
    var orderEntries = _dbContext.OrderEntries.Where(e => e.OrderId == createdOrder.Id).ToList();
    Assert.Equal(2, orderEntries.Count);

    // Log actual quantities for debugging
    var firstOrderEntry = orderEntries.FirstOrDefault(e => e.ProductId == product1.Id);
    var secondOrderEntry = orderEntries.FirstOrDefault(e => e.ProductId == product2.Id);

    Assert.NotNull(firstOrderEntry);
    Assert.NotNull(secondOrderEntry);

    Assert.Equal(10, firstOrderEntry.Quantity); // Expecting 10
    Assert.Equal(5, secondOrderEntry.Quantity); // Expecting 5
}



        [Fact]
        public void UpdateOrder_UpdatesExistingOrder()
        {
            // Arrange: adding a test order to the in-memory database
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = 100
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            // Create an update DTO
            var updateOrderDto = new UpdateOrderDto
            {
                OrderDate = DateTime.Now,
                Status = "Completed",
                TotalAmount = 150
            };

            // Act: update the existing order
            _orderService.UpdateOrder(1, updateOrderDto);

            // Assert: verify the order was updated
            var updatedOrder = _dbContext.Orders.Find(1);
            Assert.NotNull(updatedOrder); // Ensure the order exists
            Assert.Equal("Completed", updatedOrder.Status); // Verify status change
            Assert.Equal(150, updatedOrder.TotalAmount); // Verify total amount change
        }

        [Fact]
        public void DeleteOrder_RemovesOrder()
        {
            // Arrange: adding a test order to the in-memory database
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = 100
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            // Act: delete the order
            _orderService.DeleteOrder(1);

            // Assert: verify that the order was removed
            var deletedOrder = _dbContext.Orders.Find(1);
            Assert.Null(deletedOrder); // Ensure the order is deleted
        }
    }
}
