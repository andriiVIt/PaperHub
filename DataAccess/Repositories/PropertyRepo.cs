using DataAccess.Models;

namespace DataAccess;

public class PropertyRepo : IPropertyRepo
{
    private readonly MyDbContext _context;

    public PropertyRepo(MyDbContext context)
    {
        _context = context;
    }

    public List<Property> GetAllProperties()
    {
        return _context.Properties.ToList();
    }

    public Property? GetPropertyById(int id)
    {
        return _context.Properties.FirstOrDefault(p => p.Id == id);
    }
}