using DataAccess.Models;

namespace Service;

public class OrderEntryCreateDto
{
    
    public int Quantity { get; set; }
    public int? ProductId { get; set; }
    public int? OrderId { get; set; }
    public GetPaperDto? Product { get; set; } // Nested DTO for product (Paper)
    
     

    public static OrderEntry ToEntity(OrderEntryCreateDto orderEntryDto)
    {
        return new OrderEntry
        {
            
            Quantity = orderEntryDto.Quantity,
            ProductId = orderEntryDto.ProductId,
            OrderId = orderEntryDto.OrderId
        };
    }
}