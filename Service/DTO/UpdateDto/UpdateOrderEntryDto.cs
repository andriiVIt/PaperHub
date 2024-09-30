using DataAccess.Models;

namespace Service.DTO.UpdateDto;

public class UpdateOrderEntryDto
{
    
    public int Quantity { get; set; }
    public int? ProductId { get; set; }
    public int? OrderId { get; set; }
    public GetPaperDto? Product { get; set; } // Nested DTO for product (Paper)
    
     

    public static OrderEntry ToEntity(UpdateOrderEntryDto updateOrderEntryDto)
    {
        return new OrderEntry
        {
            
            Quantity = updateOrderEntryDto.Quantity,
            ProductId = updateOrderEntryDto.ProductId,
            OrderId = updateOrderEntryDto.OrderId
        };
    }
}