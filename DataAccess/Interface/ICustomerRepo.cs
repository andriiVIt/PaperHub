using DataAccess.Models;

namespace DataAccess;

public interface ICustomerRepo
{
    List<Customer> GetAllCustomers();
    Customer? GetCustomerById(int id);
}