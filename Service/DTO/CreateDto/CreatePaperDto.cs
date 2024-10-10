using DataAccess.Models;

namespace Service;

public class CreatePaperDto
{
     
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public bool Discontinued { get; set; }
    public int Stock { get; set; }
   
    public List<CreatePaperPropertyDto> PaperProperties { get; set; }= null!;
    

    public static Paper ToEntity(CreatePaperDto createPaperDto)
    {
        return new Paper
        {
            
            Name = createPaperDto.Name,
            Price = createPaperDto.Price,
            Discontinued = createPaperDto.Discontinued,
            Stock = createPaperDto.Stock,
        };
    }
}