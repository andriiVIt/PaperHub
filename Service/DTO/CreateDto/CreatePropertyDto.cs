using DataAccess.Models;

namespace Service;

public class CreatePropertyDto
{
    public string PropertyName { get; set; } = null!;

     
    public static Property ToEntity(CreatePropertyDto createPropertyDto)
    {
        return new Property
        {
            PropertyName = createPropertyDto.PropertyName
        };
    }
}