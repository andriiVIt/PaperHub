using DataAccess.Models;

namespace Service;

public class PropertyDto
{
    public int Id { get; set; }
    public string PropertyName { get; set; } = null!;
    public List<PaperDto> Papers { get; set; } = new List<PaperDto>();

    public static PropertyDto FromEntity(Property property)
    {
        return new PropertyDto
        {
            Id = property.Id,
            PropertyName = property.PropertyName,
            Papers = property.Papers.Select(PaperDto.FromEntity).ToList()
        };
    }

    public static Property ToEntity(PropertyDto propertyDto)
    {
        return new Property
        {
            Id = propertyDto.Id,
            PropertyName = propertyDto.PropertyName,
            Papers = propertyDto.Papers.Select(PaperDto.ToEntity).ToList()
        };
    }
}