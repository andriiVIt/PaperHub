using DataAccess.Models;

namespace Service;

public interface ICustomerService
{
    List<GetCustomerDto> GetAllCustomers(int limit, int startAt);
    GetCustomerDto? GetCustomerById(int id);
    void CreateCustomer(CustomerCreateDto customerDto);
    void UpdateCustomer(int id, GetCustomerDto getCustomerDto);
    void DeleteCustomer(int id);
}