using Microsoft.EntityFrameworkCore;
using Xunit;
using DataAccess;
using DataAccess.Models;
using Service;
using Service.DTO.UpdateDto;
using System.Collections.Generic;

namespace PaperHub.Tests.Services
{
    public class OrderEntryServiceTests : IDisposable
    {
        private readonly OrderEntryService _orderEntryService;
        private readonly MyDbContext _dbContext;

        public OrderEntryServiceTests()
        {
            // Setting up InMemoryDatabase options
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            // Creating an instance of the in-memory database context
            _dbContext = new MyDbContext(options);
            _orderEntryService = new OrderEntryService(new OrderEntryRepo(_dbContext), _dbContext);
        }

        // Test to check if GetAllOrderEntries returns the correct number of entries
        [Fact]
        public void GetAllOrderEntries_ReturnsCorrectNumberOfEntries()
        {
            // Arrange: adding test data to the in-memory database
            _dbContext.OrderEntries.AddRange(new List<OrderEntry>
            {
                new OrderEntry { Id = 1, Quantity = 5, ProductId = 1, OrderId = 1 },
                new OrderEntry { Id = 2, Quantity = 3, ProductId = 2, OrderId = 2 }
            });
            _dbContext.SaveChanges();

            // Act: calling the service method
            var result = _orderEntryService.GetAllOrderEntries();

            // Assert: verifying that the correct number of entries is returned
            Assert.Equal(2, result.Count); // The method should return two entries
        }

        // Test to check if GetOrderEntryById returns the correct entry when it exists
        // Test to check if GetOrderEntryById handles nullable OrderId
        [Fact]
        public void GetOrderEntryById_ReturnsOrderEntry_WhenEntryHasNullOrderId()
        {
             
            var orderEntry = new OrderEntry 
            { 
                Id = 1, 
                ProductId = 1, 
                Quantity = 10,
                OrderId = null  
            };

             
            _dbContext.OrderEntries.Add(orderEntry);
            _dbContext.SaveChanges();

             
            var result = _orderEntryService.GetOrderEntryById(orderEntry.Id);

             
            Assert.NotNull(result);
            Assert.Equal(orderEntry.Id, result.Id);
            Assert.Equal(orderEntry.ProductId, result.ProductId);
            Assert.Null(result.OrderId);  
        }


        // Test to verify that an order entry is successfully created
        // Test to verify that an order entry is successfully created
        // Test to verify that an order entry is successfully created
        [Fact]
        public void CreateOrderEntry_SuccessfullyAddsOrderEntry()
        {
            // Arrange: adding a test order and product to the in-memory database
            _dbContext.Orders.Add(new Order { Id = 1, OrderDate = DateTime.Now, Status = "Pending" });
            _dbContext.Papers.Add(new Paper { Id = 1, Name = "Test Paper", Price = 5.0 });
            _dbContext.SaveChanges();  

            // Arrange: creating a DTO for a new order entry
            var createOrderEntryDto = new CreateOrderEntryDto { Quantity = 10, ProductId = 1  };

            // Act: calling the service method to create an order entry
            _orderEntryService.CreateOrderEntry(createOrderEntryDto);

            // Assert: verifying that the order entry was added to the in-memory database
            var orderEntry = _dbContext.OrderEntries.FirstOrDefault(o => o.Quantity == 10);
            Assert.NotNull(orderEntry); // The entry should be added
            Assert.Equal(10, orderEntry.Quantity); // Verifying the quantity
        }



        // Test to verify that an order entry is updated correctly when it exists
        // Test to verify that an order entry is updated correctly when it exists
        [Fact]
        public void UpdateOrderEntry_UpdatesOrderEntry_WhenEntryExists()
        {
             
            var orderEntry = new OrderEntry { Id = 1, ProductId = 1, Quantity = 10 };
            _dbContext.OrderEntries.Add(orderEntry);
            _dbContext.SaveChanges();

            var updateDto = new UpdateOrderEntryDto { ProductId = 1, Quantity = 20 };

             
            _orderEntryService.UpdateOrderEntry(orderEntry.Id, updateDto);

             
            var updatedEntry = _dbContext.OrderEntries.Find(orderEntry.Id);
            Assert.NotNull(updatedEntry);
            Assert.Equal(20, updatedEntry.Quantity);
        }



        // Test to verify that an order entry is deleted when it exists
        [Fact]
        public void DeleteOrderEntry_RemovesOrderEntry_WhenEntryExists()
        {
            // Arrange: adding a test order entry to the in-memory database
            _dbContext.OrderEntries.Add(new OrderEntry { Id = 1, Quantity = 5, ProductId = 1, OrderId = 1 });
            _dbContext.SaveChanges();

            // Act: calling the service method to delete the order entry
            _orderEntryService.DeleteOrderEntry(1);

            // Assert: verifying that the order entry was removed from the in-memory database
            var deletedOrderEntry = _dbContext.OrderEntries.Find(1);
            Assert.Null(deletedOrderEntry); // The entry should be deleted
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
