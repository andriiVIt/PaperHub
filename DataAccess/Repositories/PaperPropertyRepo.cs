using DataAccess.Models;

namespace DataAccess;

public class PaperPropertyRepo : IPaperPropertyRepo
{
    private readonly MyDbContext _context;

    public PaperPropertyRepo(MyDbContext context)
    {
        _context = context;
    }

    public List<PaperProperty> GetAllPaperProperties()
    {
        return _context.PaperProperties.ToList();
    }

    public PaperProperty? GetPaperPropertyById(int paperId, int propertyId)
    {
        return _context.PaperProperties.FirstOrDefault(pp => pp.PaperId == paperId && pp.PropertyId == propertyId);
    }
}