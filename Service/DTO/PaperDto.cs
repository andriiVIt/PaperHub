using DataAccess.Models;

namespace Service;

public class PaperDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public bool Discontinued { get; set; }

    public static PaperDto FromEntity(Paper paper)
    {
        return new PaperDto
        {
            Id = paper.Id,
            Name = paper.Name,
            Price = paper.Price,
            Discontinued = paper.Discontinued
        };
    }

    public static Paper ToEntity(PaperDto paperDto)
    {
        return new Paper
        {
            Id = paperDto.Id,
            Name = paperDto.Name,
            Price = paperDto.Price,
            Discontinued = paperDto.Discontinued
        };
    }
}