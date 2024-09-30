using Service.DTO.UpdateDto;

namespace Service;

public interface IPaperPropertyService
{
    List<GetPaperPropertyDto> GetAllPaperProperties();
    GetPaperPropertyDto? GetPaperPropertyById(int paperId, int propertyId);
    void CreatePaperProperty(CreatePaperPropertyDto createPaperPropertyDto);
    void UpdatePaperProperty(UpdatePaperPropertyDto updatePaperPropertyDto);
    void DeletePaperProperty(int paperId, int propertyId);
}