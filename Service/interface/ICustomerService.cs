using DataAccess.Models;
using Service.DTO.UpdateDto;

namespace Service;

public interface ICustomerService
{
    List<GetCustomerDto> GetAllCustomers(int limit, int startAt);
    GetCustomerDto? GetCustomerById(int id);
    void CreateCustomer(CreateCustomerDto createCustomerDto);
    void UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto);
    void DeleteCustomer(int id);
}