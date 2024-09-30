using DataAccess.Models;

namespace Service;

public class GetPropertyDto
{
    public int Id { get; set; }
    public string PropertyName { get; set; } = null!;
    public List<GetPaperDto> Papers { get; set; } = new List<GetPaperDto>();

    public static GetPropertyDto FromEntity(Property property)
    {
        return new GetPropertyDto
        {
            Id = property.Id,
            PropertyName = property.PropertyName,
            Papers = property.Papers.Select(GetPaperDto.FromEntity).ToList()
        };
    }

    public static Property ToEntity(GetPropertyDto getPropertyDto)
    {
        return new Property
        {
            Id = getPropertyDto.Id,
            PropertyName = getPropertyDto.PropertyName,
            Papers = getPropertyDto.Papers.Select(GetPaperDto.ToEntity).ToList()
        };
    }
}