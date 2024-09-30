using DataAccess.Models;

namespace Service;

public class PropertyCreateDto
{
     
    public string PropertyName { get; set; } = null!;
    public List<PaperDto> Papers { get; set; } = new List<PaperDto>();

    

    public static Property ToEntity(PropertyCreateDto propertyDto)
    {
        return new Property
        {
             
            PropertyName = propertyDto.PropertyName,
            Papers = propertyDto.Papers.Select(PaperDto.ToEntity).ToList()
        };
    }
}