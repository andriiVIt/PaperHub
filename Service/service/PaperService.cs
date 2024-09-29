using DataAccess;
using DataAccess.Models;

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

    public List<PaperDto> GetAllPapers(int limit, int startAt)
    {
        var papers = _paperRepo.GetAllPapers();
        return papers.OrderBy(p => p.Id)
            .Skip(startAt)
            .Take(limit)
            .Select(PaperDto.FromEntity)
            .ToList();
    }

    public PaperDto? GetPaperById(int id)
    {
        var paper = _paperRepo.GetPaperById(id);
        return paper != null ? PaperDto.FromEntity(paper) : null;
    }

    public void CreatePaper(PaperDto paperDto)
    {
        var paper = PaperDto.ToEntity(paperDto);
        _context.Papers.Add(paper);
        _context.SaveChanges();
    }

    public void UpdatePaper(int id, PaperDto paperDto)
    {
        var paper = _context.Papers.Find(id);
        if (paper != null)
        {
            paper.Name = paperDto.Name;
            paper.Price = paperDto.Price;
            paper.Discontinued = paperDto.Discontinued;
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