using DataAccess.Models;

namespace Service.DTO.UpdateDto;

public class UpdatePropertyDto
{
     
    public string PropertyName { get; set; } = null!;
    public List<GetPaperDto> Papers { get; set; } = new List<GetPaperDto>();

    

    public static Property ToEntity(UpdatePropertyDto updatePropertyDto)
    {
        return new Property
        {
             
            PropertyName = updatePropertyDto.PropertyName,
            Papers = updatePropertyDto.Papers.Select(GetPaperDto.ToEntity).ToList()
        };
    }
}