using DataAccess;
using DataAccess.Models;
using Service.DTO.UpdateDto;

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

    public List<GetCustomerDto> GetAllCustomers(int limit, int startAt)
    {
        var customers = _customerRepo.GetAllCustomers();
        return customers.OrderBy(c => c.Id)
            .Skip(startAt)
            .Take(limit)
            .Select(GetCustomerDto.FromEntity)
            .ToList();
    }

    public GetCustomerDto? GetCustomerById(int id)
    {
        var customer = _customerRepo.GetCustomerById(id);
        return customer != null ? GetCustomerDto.FromEntity(customer) : null;
    }

    public void CreateCustomer(CreateCustomerDto createCustomerDto)
    {
        
        var customer = CreateCustomerDto.ToEntity(createCustomerDto);
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public void UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
    {
        if (updateCustomerDto == null)
        {
            throw new ArgumentNullException(nameof(updateCustomerDto), "Update data cannot be null");
        }
    
        var customer = _context.Customers.Find(id);
    
        if (customer != null)
        {
            // Alternatively, you can map the entire entity with ToEntity method, if it makes sense.
             // var updatecustomer = UpdateCustomerDto.ToEntity(updateCustomerDto);
        
            customer.Name = updateCustomerDto.Name;
            customer.Address = updateCustomerDto.Address;
            customer.Phone = updateCustomerDto.Phone;
            customer.Email = updateCustomerDto.Email;
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
        else
        {
            // Throw an exception or handle not found scenario.
            throw new KeyNotFoundException($"Customer with ID {id} not found.");
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