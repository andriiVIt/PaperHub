using DataAccess.Models;

namespace Service;

public interface ICustomerService
{
    List<CustomerDto> GetAllCustomers(int limit, int startAt);
    CustomerDto? GetCustomerById(int id);
    void CreateCustomer(CustomerCreateDto customerDto);
    void UpdateCustomer(int id, CustomerDto customerDto);
    void DeleteCustomer(int id);
}