using DataAccess.Models;

namespace Service;

public class CreateOrderDto
{
    
    public DateTime OrderDate { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public string Status { get; set; } = null!;
    public double TotalAmount { get; set; }
    public int? CustomerId { get; set; }
    
    public List<CreateOrderEntryDto> OrderEntries { get; set; } = null!;
   
   

   

    public static Order ToEntity(CreateOrderDto createOrderDto)
    {
        return new Order
        {
           
            OrderDate = createOrderDto.OrderDate,
            DeliveryDate = createOrderDto.DeliveryDate,
            Status = createOrderDto.Status,
            TotalAmount = createOrderDto.TotalAmount,
            CustomerId = createOrderDto.CustomerId,
           
        };
    }
    
}
