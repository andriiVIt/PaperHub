using DataAccess;
using DataAccess.Models;
using Service.DTO.UpdateDto;

namespace Service;

public class PaperService : IPaperService
{
    private readonly IPaperRepo _paperRepo;
    private readonly MyDbContext _context;

    public PaperService(IPaperRepo paperRepo, MyDbContext context)
    {
        _paperRepo = paperRepo;
        _context = context;
    }

    public List<GetPaperDto> GetAllPapers(int limit, int startAt)
    {
        var papers = _paperRepo.GetAllPapers();
        return papers.OrderBy(p => p.Id)
            .Skip(startAt)
            .Take(limit)
            .Select(GetPaperDto.FromEntity)
            .ToList();
    }

    public GetPaperDto? GetPaperById(int id)
    {
        var paper = _paperRepo.GetPaperById(id);
        return paper != null ? GetPaperDto.FromEntity(paper) : null;
    }

    public void CreatePaper(CreatePaperDto createPaperDto)
    {
        var paper = CreatePaperDto.ToEntity(createPaperDto);
        var paperProperties = createPaperDto.PaperProperties;
        var createdPaper = _context.Papers.Add(paper).Entity;
        _context.SaveChanges();

        
        foreach (var paperProperty in paperProperties)
        {
            var paperPropertyEntity = CreatePaperPropertyDto.ToEntity(paperProperty);
            paperPropertyEntity.PaperId = createdPaper.Id;
            _context.PaperProperties.Add(paperPropertyEntity);
        }
        
        _context.SaveChanges();
    }

    public void UpdatePaper(int id, UpdatePaperDto updatePaperDto)
    {
        
        var paper = _context.Papers.Find(id);
        if (paper != null)
        {
            paper.Name = updatePaperDto.Name;
            paper.Price = updatePaperDto.Price;
            paper.Discontinued = updatePaperDto.Discontinued;
            _context.SaveChanges();
        }
    }

    public void DeletePaper(int id)
    {
        var paper = _context.Papers.Find(id);
        if (paper != null)
        {
            _context.Papers.Remove(paper);
            _context.SaveChanges();
        }
    }
}