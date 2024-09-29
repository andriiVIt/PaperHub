using DataAccess.Models;

namespace Service;

public class OrderEntryDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int? ProductId { get; set; }
    public int? OrderId { get; set; }
    public PaperDto? Product { get; set; } // Nested DTO for product (Paper)
    
    public static OrderEntryDto FromEntity(OrderEntry orderEntry)
    {
        return new OrderEntryDto
        {
            Id = orderEntry.Id,
            Quantity = orderEntry.Quantity,
            ProductId = orderEntry.ProductId,
            OrderId = orderEntry.OrderId,
            Product = orderEntry.Product != null ? PaperDto.FromEntity(orderEntry.Product) : null
        };
    }

    public static OrderEntry ToEntity(OrderEntryDto orderEntryDto)
    {
        return new OrderEntry
        {
            Id = orderEntryDto.Id,
            Quantity = orderEntryDto.Quantity,
            ProductId = orderEntryDto.ProductId,
            OrderId = orderEntryDto.OrderId
        };
    }
}