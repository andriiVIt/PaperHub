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

    public void CreatePaper(PaperCreateDto paperDto)
    {
        var paper = PaperCreateDto.ToEntity(paperDto);
        _context.Papers.Add(paper);
        _context.SaveChanges();
    }

    public void UpdatePaper(int id, GetPaperDto getPaperDto)
    {
        var paper = _context.Papers.Find(id);
        if (paper != null)
        {
            paper.Name = getPaperDto.Name;
            paper.Price = getPaperDto.Price;
            paper.Discontinued = getPaperDto.Discontinued;
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