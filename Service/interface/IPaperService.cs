using DataAccess.Models;
using Service.DTO.UpdateDto;

namespace Service;

public interface IPaperService
{
    List<GetPaperDto> GetAllPapers(int limit, int startAt);
    GetPaperDto? GetPaperById(int id);
    void CreatePaper(CreatePaperDto createPaperDto);
    void UpdatePaper(int id, UpdatePaperDto updatePaperDto);
    void DeletePaper(int id);
}