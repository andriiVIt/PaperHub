using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Models;
using Service.DTO.UpdateDto;
using Service;

public class PropertyServiceTests
{
    private readonly Mock<IPropertyRepo> _propertyRepoMock;
    private readonly MyDbContext _dbContext;
    private readonly PropertyService _propertyService;

    public PropertyServiceTests()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _dbContext = new MyDbContext(options);
        _propertyService = new PropertyService(new PropertyRepo(_dbContext), _dbContext);
    }
    
    // Test for getting all properties
    [Fact]
    public void GetAllProperties_ReturnsListOfProperties()
    {
         
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();

         
        var property1 = new Property { Id = 1, PropertyName = "Size" };
        var property2 = new Property { Id = 2, PropertyName = "Color" };

         
        _dbContext.Properties.Add(property1);
        _dbContext.Properties.Add(property2);
        _dbContext.SaveChanges();

         
        var allPropertiesInDb = _dbContext.Properties.ToList();
        Assert.NotNull(allPropertiesInDb);
        Assert.NotEmpty(allPropertiesInDb);  

         
        var result = _propertyService.GetAllProperties();

         
        Assert.NotNull(result);  
        Assert.Equal(2, result.Count);  
        Assert.Contains(result, p => p.PropertyName == "Size");
        Assert.Contains(result, p => p.PropertyName == "Color");
    }



    // Test for getting property by ID
    [Fact]
    public void GetPropertyById_ReturnsCorrectProperty()
    {
         
        var property1 = new Property { Id = 1, PropertyName = "Property 1" };
        var property2 = new Property { Id = 2, PropertyName = "Property 2" };
    
        _dbContext.Properties.Add(property1);
        _dbContext.Properties.Add(property2);
        _dbContext.SaveChanges();

         
        var result = _propertyService.GetPropertyById(1);

         
        Assert.NotNull(result);
        Assert.Equal("Property 1", result.PropertyName);
    }



    // Test for creating a property
    [Fact]
    public void CreateProperty_AddsPropertyToDatabase()
    {
        // Arrange: Create a DTO for the new property
        var createPropertyDto = new CreatePropertyDto
        {
            PropertyName = "Test Property"
        };

        // Act: Call the service method to create the property
        _propertyService.CreateProperty(createPropertyDto);

        // Assert: Check that the property was added to the database
        var property = _dbContext.Properties.FirstOrDefault(p => p.PropertyName == "Test Property");
        Assert.NotNull(property);
        Assert.Equal("Test Property", property.PropertyName);
    }

    // Test for updating a property
    [Fact]
    public void UpdateProperty_UpdatesExistingProperty_WhenPropertyExists()
    {
        // Arrange: Add a test property to the in-memory database
        var property = new Property { Id = 1, PropertyName = "Old Name" };
        _dbContext.Properties.Add(property);
        _dbContext.SaveChanges();

        var updatePropertyDto = new UpdatePropertyDto
        {
            PropertyName = "Updated Name"
        };

        // Act: Call the service method to update the property
        _propertyService.UpdateProperty(1, updatePropertyDto);

        // Assert: Verify that the property was updated
        var updatedProperty = _dbContext.Properties.FirstOrDefault(p => p.Id == 1);
        Assert.NotNull(updatedProperty);
        Assert.Equal("Updated Name", updatedProperty.PropertyName);
    }

    // Test for deleting a property
    [Fact]
    public void DeleteProperty_RemovesPropertyFromDatabase()
    {
        // Arrange: Add a test property to the in-memory database
        var property = new Property { Id = 1, PropertyName = "Test Property" };
        _dbContext.Properties.Add(property);
        _dbContext.SaveChanges();

        // Act: Call the service method to delete the property
        _propertyService.DeleteProperty(1);

        // Assert: Verify that the property was removed from the database
        var deletedProperty = _dbContext.Properties.FirstOrDefault(p => p.Id == 1);
        Assert.Null(deletedProperty);
    }
}

