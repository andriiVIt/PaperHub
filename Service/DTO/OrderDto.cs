using DataAccess.Models;

namespace Service;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public string Status { get; set; } = null!;
    public double TotalAmount { get; set; }
    public int? CustomerId { get; set; }
    public CustomerDto? Customer { get; set; } // Nested DTO for customer
    public List<OrderEntryDto> OrderEntries { get; set; } = new List<OrderEntryDto>();

    public static OrderDto FromEntity(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            DeliveryDate = order.DeliveryDate,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            CustomerId = order.CustomerId,
            Customer = order.Customer != null ? CustomerDto.FromEntity(order.Customer) : null,
            OrderEntries = order.OrderEntries.Select(OrderEntryDto.FromEntity).ToList()
        };
    }

    public static Order ToEntity(OrderDto orderDto)
    {
        return new Order
        {
            Id = orderDto.Id,
            OrderDate = orderDto.OrderDate,
            DeliveryDate = orderDto.DeliveryDate,
            Status = orderDto.Status,
            TotalAmount = orderDto.TotalAmount,
            CustomerId = orderDto.CustomerId,
            OrderEntries = orderDto.OrderEntries.Select(OrderEntryDto.ToEntity).ToList()
        };
    }
}