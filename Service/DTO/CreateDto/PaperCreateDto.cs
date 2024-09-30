using DataAccess.Models;

namespace Service;

public class PaperCreateDto
{
     
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public bool Discontinued { get; set; }

    

    public static Paper ToEntity(PaperCreateDto paperDto)
    {
        return new Paper
        {
            
            Name = paperDto.Name,
            Price = paperDto.Price,
            Discontinued = paperDto.Discontinued
        };
    }
}