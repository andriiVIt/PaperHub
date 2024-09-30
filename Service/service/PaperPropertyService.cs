using DataAccess;
using Service.DTO.UpdateDto;

namespace Service.service;

public class PaperPropertyService : IPaperPropertyService
{
    private readonly IPaperPropertyRepo _paperPropertyRepo;
    private readonly MyDbContext _context;

    public PaperPropertyService(IPaperPropertyRepo paperPropertyRepo, MyDbContext context)
    {
        _paperPropertyRepo = paperPropertyRepo;
        _context = context;
    }

    public List<GetPaperPropertyDto> GetAllPaperProperties()
    {
        var paperProperties = _paperPropertyRepo.GetAllPaperProperties();
        return paperProperties.Select(GetPaperPropertyDto.FromEntity).ToList();
    }

    public GetPaperPropertyDto? GetPaperPropertyById(int paperId, int propertyId)
    {
        var paperProperty = _paperPropertyRepo.GetPaperPropertyById(paperId, propertyId);
        return paperProperty != null ? GetPaperPropertyDto.FromEntity(paperProperty) : null;
    }

    public void CreatePaperProperty(CreatePaperPropertyDto createPaperPropertyDto)
    {
        var paperProperty = CreatePaperPropertyDto.ToEntity(createPaperPropertyDto);
        _context.PaperProperties.Add(paperProperty);
        _context.SaveChanges();
    }

    public void UpdatePaperProperty(UpdatePaperPropertyDto updatePaperPropertyDto)
    {
        throw new NotImplementedException();
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