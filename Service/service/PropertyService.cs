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

    public List<GetPropertyDto> GetAllProperties()
    {
        var properties = _propertyRepo.GetAllProperties();
        return properties.Select(GetPropertyDto.FromEntity).ToList();
    }

    public GetPropertyDto? GetPropertyById(int id)
    {
        var property = _propertyRepo.GetPropertyById(id);
        return property != null ? GetPropertyDto.FromEntity(property) : null;
    }

    public void CreateProperty(PropertyCreateDto propertyDto)
    {
        var property = PropertyCreateDto.ToEntity(propertyDto);
        _context.Properties.Add(property);
        _context.SaveChanges();
    }

    public void UpdateProperty(int id, GetPropertyDto getPropertyDto)
    {
        var property = _context.Properties.Find(id);
        if (property != null)
        {
            property.PropertyName = getPropertyDto.PropertyName;
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