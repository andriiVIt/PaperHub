namespace Service;

public interface IPaperPropertyService
{
    List<GetPaperPropertyDto> GetAllPaperProperties();
    GetPaperPropertyDto? GetPaperPropertyById(int paperId, int propertyId);
    void CreatePaperProperty(PaperPropertyCreateDto paperPropertyDto);
    void DeletePaperProperty(int paperId, int propertyId);
}