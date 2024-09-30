using DataAccess.Models;

namespace Service.DTO.UpdateDto;

public class UpdateCustomerDto
{
     
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    

    public static Customer ToEntity(UpdateCustomerDto updateCustomerDto)
    {
        return new Customer
        {
            
            Name = updateCustomerDto.Name,
            Address = updateCustomerDto.Address,
            Phone = updateCustomerDto.Phone,
            Email = updateCustomerDto.Email
        };
    }
    
}