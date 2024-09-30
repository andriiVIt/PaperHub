using DataAccess.Models;

namespace Service;

public class PaperPropertyCreateDto
{
     
    public PaperDto Paper { get; set; } = null!;
    
    public PropertyDto Property { get; set; } = null!;

     

    public static PaperProperty ToEntity(PaperPropertyCreateDto paperPropertyDto)
    {
        return new PaperProperty
        {
             
            Paper = PaperDto.ToEntity(paperPropertyDto.Paper),
             
            Property = PropertyDto.ToEntity(paperPropertyDto.Property)
        };
    }
}