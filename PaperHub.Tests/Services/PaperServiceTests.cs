using Xunit;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Models;
using Service;
using Service.DTO.UpdateDto;
using System.Collections.Generic;
using System.Linq;

public class PaperServiceTests : IDisposable
{
    private readonly MyDbContext _dbContext;
    private readonly PaperService _paperService;

    public PaperServiceTests()
    {
        // Створення InMemory бази для тестування
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        
        _dbContext = new MyDbContext(options);
        _paperService = new PaperService(new PaperRepo(_dbContext), _dbContext); // Використовуємо InMemoryDbContext
    }

    [Fact]
    public void CreatePaper_AddsNewPaper()
    {
        // Arrange
        var createPaperDto = new CreatePaperDto
        {
            Name = "Test Paper",
            Price = 9.99,
            Discontinued = false,
            PaperProperties = new List<CreatePaperPropertyDto>()
        };
        
        // Act
        _paperService.CreatePaper(createPaperDto);

        // Assert
        var paper = _dbContext.Papers.FirstOrDefault(p => p.Name == "Test Paper");
        Assert.NotNull(paper);
        Assert.Equal(9.99, paper.Price);
    }

    [Fact]
    public void GetPaperById_ReturnsCorrectPaper()
    {
        // Arrange
        var paper = new Paper { Id = 1, Name = "Test Paper", Price = 5.0 };
        _dbContext.Papers.Add(paper); // Додаємо запис до бази даних
        _dbContext.SaveChanges();

        // Act
        var result = _paperService.GetPaperById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Paper", result.Name);
    }

    [Fact]
    public void GetAllPapers_ReturnsLimitedPapers()
    {
        // Arrange
        _dbContext.Papers.AddRange(
            new Paper { Id = 1, Name = "Paper1", Price = 5.0 },
            new Paper { Id = 2, Name = "Paper2", Price = 7.0 },
            new Paper { Id = 3, Name = "Paper3", Price = 9.0 }
        );
        _dbContext.SaveChanges();

        // Act
        var result = _paperService.GetAllPapers(2, 1);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Paper2", result[0].Name);
        Assert.Equal("Paper3", result[1].Name);
    }

    [Fact]
    public void UpdatePaper_UpdatesExistingPaper()
    {
        // Arrange
        var paper = new Paper { Id = 1, Name = "Old Paper", Price = 10.0 };
        _dbContext.Papers.Add(paper); // Додаємо початковий запис
        _dbContext.SaveChanges();

        var updateDto = new UpdatePaperDto
        {
            Name = "Updated Paper",
            Price = 20.0,
            Discontinued = false
        };

        // Act
        _paperService.UpdatePaper(1, updateDto); // Оновлюємо вже існуючий запис

        // Assert
        var updatedPaper = _dbContext.Papers.Find(1);
        Assert.NotNull(updatedPaper);
        Assert.Equal("Updated Paper", updatedPaper.Name);
        Assert.Equal(20.0, updatedPaper.Price);
    }

    [Fact]
    public void DeletePaper_RemovesPaper()
    {
        // Arrange
        var paper = new Paper { Id = 1, Name = "Test Paper" };
        _dbContext.Papers.Add(paper);
        _dbContext.SaveChanges();

        // Act
        _paperService.DeletePaper(1);

        // Assert
        var deletedPaper = _dbContext.Papers.Find(1);
        Assert.Null(deletedPaper);
    }

    // Використовуємо Dispose для очистки бази після кожного тесту
    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted(); 
        _dbContext.Dispose();
    }
}
