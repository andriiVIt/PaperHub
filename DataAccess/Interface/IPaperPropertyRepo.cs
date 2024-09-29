using DataAccess.Models;

namespace DataAccess;

public interface IPaperPropertyRepo
{
    List<PaperProperty> GetAllPaperProperties();
    PaperProperty? GetPaperPropertyById(int paperId, int propertyId);
}