

using DataAccess.Models;

namespace Service;

public class CreateCustomerDto
{
     
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    

    public static Customer ToEntity(CreateCustomerDto createCustomerDto)
    {
        return new Customer
        {
            
            Name = createCustomerDto.Name,
            Address = createCustomerDto.Address,
            Phone = createCustomerDto.Phone,
            Email = createCustomerDto.Email
        };
    }
    
}