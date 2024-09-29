using DataAccess.Models;

namespace Service;

public class PaperPropertyDto
{
    public int PaperId { get; set; }
    public PaperDto Paper { get; set; } = null!;
    public int PropertyId { get; set; }
    public PropertyDto Property { get; set; } = null!;

    public static PaperPropertyDto FromEntity(PaperProperty paperProperty)
    {
        return new PaperPropertyDto
        {
            PaperId = paperProperty.PaperId,
            Paper = PaperDto.FromEntity(paperProperty.Paper),
            PropertyId = paperProperty.PropertyId,
            Property = PropertyDto.FromEntity(paperProperty.Property)
        };
    }

    public static PaperProperty ToEntity(PaperPropertyDto paperPropertyDto)
    {
        return new PaperProperty
        {
            PaperId = paperPropertyDto.PaperId,
            Paper = PaperDto.ToEntity(paperPropertyDto.Paper),
            PropertyId = paperPropertyDto.PropertyId,
            Property = PropertyDto.ToEntity(paperPropertyDto.Property)
        };
    }
}