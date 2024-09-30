using DataAccess.Models;

namespace Service;

public class CreatePropertyDto
{
     
    public string PropertyName { get; set; } = null!;
    public List<GetPaperDto> Papers { get; set; } = new List<GetPaperDto>();

    

    public static Property ToEntity(CreatePropertyDto createPropertyDto)
    {
        return new Property
        {
             
            PropertyName = createPropertyDto.PropertyName,
            Papers = createPropertyDto.Papers.Select(GetPaperDto.ToEntity).ToList()
        };
    }
}