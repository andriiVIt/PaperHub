using DataAccess.Models;

namespace Service.DTO.UpdateDto;

public class UpdatePaperDto
{
     
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public bool Discontinued { get; set; }

    

    public static Paper ToEntity(UpdatePaperDto updatePaperDto)
    {
        return new Paper
        {
            
            Name = updatePaperDto.Name,
            Price = updatePaperDto.Price,
            Discontinued = updatePaperDto.Discontinued
        };
    }
}