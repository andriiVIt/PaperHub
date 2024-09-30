using Service.DTO.UpdateDto;

namespace Service;

public interface IPropertyService
{
    List<GetPropertyDto> GetAllProperties();
    GetPropertyDto? GetPropertyById(int id);
    void CreateProperty(CreatePropertyDto createPropertyDto);
    void UpdateProperty(int id, UpdatePropertyDto updatePropertyDto);
    void DeleteProperty(int id);
}