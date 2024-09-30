using DataAccess.Models;

namespace Service;

public class CreatePaperPropertyDto
{
     
    public GetPaperDto GetPaper { get; set; } = null!;
    
    public GetPropertyDto GetProperty { get; set; } = null!;

     

    public static PaperProperty ToEntity(CreatePaperPropertyDto createPaperPropertyDto)
    {
        return new PaperProperty
        {
             
            Paper = GetPaperDto.ToEntity(createPaperPropertyDto.GetPaper),
             
            Property = GetPropertyDto.ToEntity(createPaperPropertyDto.GetProperty)
        };
    }
}