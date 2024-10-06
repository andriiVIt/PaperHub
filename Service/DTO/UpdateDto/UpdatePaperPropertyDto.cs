using DataAccess.Models;

namespace Service.DTO.UpdateDto;

public class UpdatePaperPropertyDto
{
     
    public GetPaperDto GetPaper { get; set; } = null!;
    
    public GetPropertyDto GetProperty { get; set; } = null!;
    
     
    
    public static PaperProperty ToEntity(UpdatePaperPropertyDto updatePaperPropertyDto)
    {
        return new PaperProperty
        {
             
            Paper = GetPaperDto.ToEntity(updatePaperPropertyDto.GetPaper),
             
            Property = GetPropertyDto.ToEntity(updatePaperPropertyDto.GetProperty)
        };
    }
}