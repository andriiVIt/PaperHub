namespace Service;

public interface IPropertyService
{
    List<GetPropertyDto> GetAllProperties();
    GetPropertyDto? GetPropertyById(int id);
    void CreateProperty(PropertyCreateDto propertyDto);
    void UpdateProperty(int id, GetPropertyDto getPropertyDto);
    void DeleteProperty(int id);
}