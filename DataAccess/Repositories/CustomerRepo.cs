using DataAccess.Models;

namespace DataAccess;

public class CustomerRepo : ICustomerRepo
{
    private readonly MyDbContext _context;

    public CustomerRepo(MyDbContext context)
    {
        _context = context;
    }

    public List<Customer> GetAllCustomers()
    {
        return _context.Customers.ToList();
    }

    public Customer? GetCustomerById(int id)
    {
        return _context.Customers.FirstOrDefault(c => c.Id == id);
    }
}