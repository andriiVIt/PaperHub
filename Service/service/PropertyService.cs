using DataAccess;
using DataAccess.Models;
using Service.DTO.UpdateDto;

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

    public void CreateProperty(CreatePropertyDto createPropertyDto)
    {
        var property = CreatePropertyDto.ToEntity(createPropertyDto);
        _context.Properties.Add(property);
        _context.SaveChanges();
    }

    public void UpdateProperty(int id,UpdatePropertyDto updatePropertyDto)
    {
        var property = _context.Properties.Find(id);
        if (property != null)
        {
            property.PropertyName = updatePropertyDto.PropertyName;
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