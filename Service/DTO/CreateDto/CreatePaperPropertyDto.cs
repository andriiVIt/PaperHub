using DataAccess.Models;

namespace Service;

public class CreatePaperPropertyDto
{
    public int PropertyId { get; set; }  // Просто ідентифікатор існуючої властивості

    // Створення сутності з DTO, де передається лише ідентифікатор властивості
    public static PaperProperty ToEntity(CreatePaperPropertyDto createPaperPropertyDto)
    {
        return new PaperProperty
        {
            PropertyId = createPaperPropertyDto.PropertyId
        };
    }
}