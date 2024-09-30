using DataAccess.Models;

namespace Service;

public class CreateOrderEntryDto
{
    
    public int Quantity { get; set; }
    public int? ProductId { get; set; }
    public int? OrderId { get; set; }
    public GetPaperDto? Product { get; set; } // Nested DTO for product (Paper)
    
     

    public static OrderEntry ToEntity(CreateOrderEntryDto createOrderEntryDto)
    {
        return new OrderEntry
        {
            
            Quantity = createOrderEntryDto.Quantity,
            ProductId = createOrderEntryDto.ProductId,
            OrderId = createOrderEntryDto.OrderId
        };
    }
}