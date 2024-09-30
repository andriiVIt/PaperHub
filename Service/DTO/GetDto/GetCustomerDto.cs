using DataAccess.Models;

namespace Service;

public class GetCustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public static GetCustomerDto FromEntity(Customer customer)
    {
        return new GetCustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            Phone = customer.Phone,
            Email = customer.Email
        };
    }

    public static Customer ToEntity(GetCustomerDto getCustomerDto)
    {
        return new Customer
        {
            Id = getCustomerDto.Id,
            Name = getCustomerDto.Name,
            Address = getCustomerDto.Address,
            Phone = getCustomerDto.Phone,
            Email = getCustomerDto.Email
        };
    }
    
}