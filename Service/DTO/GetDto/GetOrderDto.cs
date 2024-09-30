using DataAccess.Models;

namespace Service;

public class GetOrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public string Status { get; set; } = null!;
    public double TotalAmount { get; set; }
    public int? CustomerId { get; set; }
   
   

    public static GetOrderDto FromEntity(Order order)
    {
        return new GetOrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            DeliveryDate = order.DeliveryDate,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            CustomerId = order.CustomerId,
           
        };
    }

    public static Order ToEntity(GetOrderDto getOrderDto)
    {
        return new Order
        {
            Id = getOrderDto.Id,
            OrderDate = getOrderDto.OrderDate,
            DeliveryDate = getOrderDto.DeliveryDate,
            Status = getOrderDto.Status,
            TotalAmount = getOrderDto.TotalAmount,
            CustomerId = getOrderDto.CustomerId,
           
        };
    }
}