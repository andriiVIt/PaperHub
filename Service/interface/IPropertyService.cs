namespace Service;

public interface IPropertyService
{
    List<PropertyDto> GetAllProperties();
    PropertyDto? GetPropertyById(int id);
    void CreateProperty(PropertyDto propertyDto);
    void UpdateProperty(int id, PropertyDto propertyDto);
    void DeleteProperty(int id);
}