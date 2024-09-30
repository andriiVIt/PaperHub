using DataAccess.Models;

namespace Service;

public interface IPaperService
{
    List<GetPaperDto> GetAllPapers(int limit, int startAt);
    GetPaperDto? GetPaperById(int id);
    void CreatePaper(PaperCreateDto paperDto);
    void UpdatePaper(int id, GetPaperDto getPaperDto);
    void DeletePaper(int id);
}