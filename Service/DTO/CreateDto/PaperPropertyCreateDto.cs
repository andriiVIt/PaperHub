using DataAccess.Models;

namespace Service;

public class PaperPropertyCreateDto
{
     
    public GetPaperDto GetPaper { get; set; } = null!;
    
    public GetPropertyDto GetProperty { get; set; } = null!;

     

    public static PaperProperty ToEntity(PaperPropertyCreateDto paperPropertyDto)
    {
        return new PaperProperty
        {
             
            Paper = GetPaperDto.ToEntity(paperPropertyDto.GetPaper),
             
            Property = GetPropertyDto.ToEntity(paperPropertyDto.GetProperty)
        };
    }
}