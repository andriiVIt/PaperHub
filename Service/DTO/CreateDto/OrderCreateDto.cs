using DataAccess.Models;

namespace Service;

public class OrderCreateDto
{
    
    public DateTime OrderDate { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public string Status { get; set; } = null!;
    public double TotalAmount { get; set; }
    public int? CustomerId { get; set; }
   
   

   

    public static Order ToEntity(OrderCreateDto orderDto)
    {
        return new Order
        {
           
            OrderDate = orderDto.OrderDate,
            DeliveryDate = orderDto.DeliveryDate,
            Status = orderDto.Status,
            TotalAmount = orderDto.TotalAmount,
            CustomerId = orderDto.CustomerId,
           
        };
    }
}