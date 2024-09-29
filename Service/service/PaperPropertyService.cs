using DataAccess;
using DataAccess.Models;

namespace Service;

public class PaperPropertyService : IPaperPropertyService
{
    private readonly IPaperPropertyRepo _paperPropertyRepo;
    private readonly MyDbContext _context;

    public PaperPropertyService(IPaperPropertyRepo paperPropertyRepo, MyDbContext context)
    {
        _paperPropertyRepo = paperPropertyRepo;
        _context = context;
    }

    public List<PaperPropertyDto> GetAllPaperProperties()
    {
        var paperProperties = _paperPropertyRepo.GetAllPaperProperties();
        return paperProperties.Select(PaperPropertyDto.FromEntity).ToList();
    }

    public PaperPropertyDto? GetPaperPropertyById(int paperId, int propertyId)
    {
        var paperProperty = _paperPropertyRepo.GetPaperPropertyById(paperId, propertyId);
        return paperProperty != null ? PaperPropertyDto.FromEntity(paperProperty) : null;
    }

    public void CreatePaperProperty(PaperPropertyDto paperPropertyDto)
    {
        var paperProperty = PaperPropertyDto.ToEntity(paperPropertyDto);
        _context.PaperProperties.Add(paperProperty);
        _context.SaveChanges();
    }

    public void DeletePaperProperty(int paperId, int propertyId)
    {
        var paperProperty = _context.PaperProperties.FirstOrDefault(pp => pp.PaperId == paperId && pp.PropertyId == propertyId);
        if (paperProperty != null)
        {
            _context.PaperProperties.Remove(paperProperty);
            _context.SaveChanges();
        }
    }
}