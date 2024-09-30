using DataAccess.Models;

namespace Service;

public class GetOrderEntryDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int? ProductId { get; set; }
    public int? OrderId { get; set; }
    public GetPaperDto? Product { get; set; } // Nested DTO for product (Paper)
    
    public static GetOrderEntryDto FromEntity(OrderEntry orderEntry)
    {
        return new GetOrderEntryDto
        {
            Id = orderEntry.Id,
            Quantity = orderEntry.Quantity,
            ProductId = orderEntry.ProductId,
            OrderId = orderEntry.OrderId,
            Product = orderEntry.Product != null ? GetPaperDto.FromEntity(orderEntry.Product) : null
        };
    }

    public static OrderEntry ToEntity(GetOrderEntryDto getOrderEntryDto)
    {
        return new OrderEntry
        {
            Id = getOrderEntryDto.Id,
            Quantity = getOrderEntryDto.Quantity,
            ProductId = getOrderEntryDto.ProductId,
            OrderId = getOrderEntryDto.OrderId
        };
    }
}