using DataAccess.Models;

namespace Service;

public interface IPaperService
{
    List<PaperDto> GetAllPapers(int limit, int startAt);
    PaperDto? GetPaperById(int id);
    void CreatePaper(PaperCreateDto paperDto);
    void UpdatePaper(int id, PaperDto paperDto);
    void DeletePaper(int id);
}