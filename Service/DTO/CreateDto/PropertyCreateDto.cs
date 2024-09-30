using DataAccess.Models;

namespace Service;

public class PropertyCreateDto
{
     
    public string PropertyName { get; set; } = null!;
    public List<GetPaperDto> Papers { get; set; } = new List<GetPaperDto>();

    

    public static Property ToEntity(PropertyCreateDto propertyDto)
    {
        return new Property
        {
             
            PropertyName = propertyDto.PropertyName,
            Papers = propertyDto.Papers.Select(GetPaperDto.ToEntity).ToList()
        };
    }
}