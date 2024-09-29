using DataAccess;
using DataAccess.Models;

namespace Service;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepo _propertyRepo;
    private readonly MyDbContext _context;

    public PropertyService(IPropertyRepo propertyRepo, MyDbContext context)
    {
        _propertyRepo = propertyRepo;
        _context = context;
    }

    public List<PropertyDto> GetAllProperties()
    {
        var properties = _propertyRepo.GetAllProperties();
        return properties.Select(PropertyDto.FromEntity).ToList();
    }

    public PropertyDto? GetPropertyById(int id)
    {
        var property = _propertyRepo.GetPropertyById(id);
        return property != null ? PropertyDto.FromEntity(property) : null;
    }

    public void CreateProperty(PropertyDto propertyDto)
    {
        var property = PropertyDto.ToEntity(propertyDto);
        _context.Properties.Add(property);
        _context.SaveChanges();
    }

    public void UpdateProperty(int id, PropertyDto propertyDto)
    {
        var property = _context.Properties.Find(id);
        if (property != null)
        {
            property.PropertyName = propertyDto.PropertyName;
            _context.SaveChanges();
        }
    }

    public void DeleteProperty(int id)
    {
        var property = _context.Properties.Find(id);
        if (property != null)
        {
            _context.Properties.Remove(property);
            _context.SaveChanges();
        }
    }
}