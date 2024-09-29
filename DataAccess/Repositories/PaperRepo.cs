using DataAccess.Models;

namespace DataAccess;

public class PaperRepo : IPaperRepo
{
    private readonly MyDbContext _context;

    public PaperRepo(MyDbContext context)
    {
        _context = context;
    }

    public List<Paper> GetAllPapers()
    {
        return _context.Papers.ToList();
    }

    public Paper? GetPaperById(int id)
    {
        return _context.Papers.FirstOrDefault(p => p.Id == id);
    }
}