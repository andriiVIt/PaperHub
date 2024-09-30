

using DataAccess.Models;

namespace Service;

public class CustomerCreateDto
{
     
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    

    public static Customer ToEntity(CustomerCreateDto customerDto)
    {
        return new Customer
        {
            
            Name = customerDto.Name,
            Address = customerDto.Address,
            Phone = customerDto.Phone,
            Email = customerDto.Email
        };
    }
    
}