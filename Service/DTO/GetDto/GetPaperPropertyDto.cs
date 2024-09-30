using DataAccess.Models;

namespace Service;

public class GetPaperPropertyDto
{
    public int PaperId { get; set; }
    public GetPaperDto GetPaper { get; set; } = null!;
    public int PropertyId { get; set; }
    public GetPropertyDto GetProperty { get; set; } = null!;

    public static GetPaperPropertyDto FromEntity(PaperProperty paperProperty)
    {
        return new GetPaperPropertyDto
        {
            PaperId = paperProperty.PaperId,
            GetPaper = GetPaperDto.FromEntity(paperProperty.Paper),
            PropertyId = paperProperty.PropertyId,
            GetProperty = GetPropertyDto.FromEntity(paperProperty.Property)
        };
    }

    public static PaperProperty ToEntity(GetPaperPropertyDto getPaperPropertyDto)
    {
        return new PaperProperty
        {
            PaperId = getPaperPropertyDto.PaperId,
            Paper = GetPaperDto.ToEntity(getPaperPropertyDto.GetPaper),
            PropertyId = getPaperPropertyDto.PropertyId,
            Property = GetPropertyDto.ToEntity(getPaperPropertyDto.GetProperty)
        };
    }
}