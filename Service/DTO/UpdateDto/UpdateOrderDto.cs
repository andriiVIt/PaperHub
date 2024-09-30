using DataAccess.Models;

namespace Service.DTO.UpdateDto;

public class UpdateOrderDto
{
    
    public DateTime OrderDate { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public string Status { get; set; } = null!;
    public double TotalAmount { get; set; }
    public int? CustomerId { get; set; }
   
   

   

    public static Order ToEntity(UpdateOrderDto updateOrderDto)
    {
        return new Order
        {
           
            OrderDate = updateOrderDto.OrderDate,
            DeliveryDate = updateOrderDto.DeliveryDate,
            Status = updateOrderDto.Status,
            TotalAmount = updateOrderDto.TotalAmount,
            CustomerId = updateOrderDto.CustomerId,
           
        };
    }
}