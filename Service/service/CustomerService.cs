using DataAccess;
using DataAccess.Models;

namespace Service;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepo _customerRepo;
    private readonly MyDbContext _context;

    public CustomerService(ICustomerRepo customerRepo, MyDbContext context)
    {
        _customerRepo = customerRepo;
        _context = context;
    }

    public List<CustomerDto> GetAllCustomers(int limit, int startAt)
    {
        var customers = _customerRepo.GetAllCustomers();
        return customers.OrderBy(c => c.Id)
            .Skip(startAt)
            .Take(limit)
            .Select(CustomerDto.FromEntity)
            .ToList();
    }

    public CustomerDto? GetCustomerById(int id)
    {
        var customer = _customerRepo.GetCustomerById(id);
        return customer != null ? CustomerDto.FromEntity(customer) : null;
    }

    public void CreateCustomer(CustomerCreateDto customerDto)
    {
        
        var customer = CustomerCreateDto.ToEntity(customerDto);
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public void UpdateCustomer(int id, CustomerDto customerDto)
    {
        var customer = _context.Customers.Find(id);
        if (customer != null)
        {
            customer.Name = customerDto.Name;
            customer.Address = customerDto.Address;
            customer.Phone = customerDto.Phone;
            customer.Email = customerDto.Email;
            _context.SaveChanges();
        }
    }

    public void DeleteCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}