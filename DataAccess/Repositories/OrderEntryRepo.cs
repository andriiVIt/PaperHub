using DataAccess.Models;

namespace DataAccess;

public class OrderEntryRepo : IOrderEntryRepo
{
    private readonly MyDbContext _context;

    public OrderEntryRepo(MyDbContext context)
    {
        _context = context;
    }

    public List<OrderEntry> GetAllOrderEntries()
    {
        return _context.OrderEntries.ToList();
    }

    public OrderEntry? GetOrderEntryById(int id)
    {
        return _context.OrderEntries.FirstOrDefault(o => o.Id == id);
    }
}