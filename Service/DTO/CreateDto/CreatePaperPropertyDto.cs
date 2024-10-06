using DataAccess.Models;

namespace Service;

public class CreatePaperPropertyDto
{
    public int PropertyId { get; set; }   
    // public int PaperId { get; set; }  
     
    public static PaperProperty ToEntity(CreatePaperPropertyDto createPaperPropertyDto)
    {
        return new PaperProperty
        {
            PropertyId = createPaperPropertyDto.PropertyId,
            // PaperId = createPaperPropertyDto.PaperId 
        };
    }
}