using DataAccess.Models;

namespace DataAccess;

public class OrderRepo : IOrderRepo
{
    private readonly MyDbContext _context;

    public OrderRepo(MyDbContext context)
    {
        _context = context;
    }

    public List<Order> GetAllOrders()
    {
        return _context.Orders.ToList();
    }

    public Order? GetOrderById(int id)
    {
        return _context.Orders.FirstOrDefault(o => o.Id == id);
    }
}