namespace Service;

public interface IPaperPropertyService
{
    List<PaperPropertyDto> GetAllPaperProperties();
    PaperPropertyDto? GetPaperPropertyById(int paperId, int propertyId);
    void CreatePaperProperty(PaperPropertyCreateDto paperPropertyDto);
    void DeletePaperProperty(int paperId, int propertyId);
}