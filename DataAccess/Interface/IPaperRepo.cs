using DataAccess.Models;

namespace DataAccess;

public interface IPaperRepo
{
    List<Paper> GetAllPapers();
    Paper? GetPaperById(int id);
}