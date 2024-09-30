using DataAccess;
using DataAccess.Models;

namespace Service;

public class OrderEntryService : IOrderEntryService
{
    private readonly IOrderEntryRepo _orderEntryRepo;
    private readonly MyDbContext _context;

    public OrderEntryService(IOrderEntryRepo orderEntryRepo, MyDbContext context)
    {
        _orderEntryRepo = orderEntryRepo;
        _context = context;
    }

    public List<OrderEntryDto> GetAllOrderEntries()
    {
        var orderEntries = _orderEntryRepo.GetAllOrderEntries();
        return orderEntries.Select(OrderEntryDto.FromEntity).ToList();
    }

    public OrderEntryDto? GetOrderEntryById(int id)
    {
        var orderEntry = _orderEntryRepo.GetOrderEntryById(id);
        return orderEntry != null ? OrderEntryDto.FromEntity(orderEntry) : null;
    }

    public void CreateOrderEntry(OrderEntryCreateDto orderEntryDto)
    {
        var orderEntry = OrderEntryCreateDto.ToEntity(orderEntryDto);
        _context.OrderEntries.Add(orderEntry);
        _context.SaveChanges();
    }

    public void UpdateOrderEntry(int id, OrderEntryDto orderEntryDto)
    {
        var orderEntry = _context.OrderEntries.Find(id);
        if (orderEntry != null)
        {
            orderEntry.Quantity = orderEntryDto.Quantity;
            orderEntry.ProductId = orderEntryDto.ProductId;
            orderEntry.OrderId = orderEntryDto.OrderId;
            _context.SaveChanges();
        }
    }

    public void DeleteOrderEntry(int id)
    {
        var orderEntry = _context.OrderEntries.Find(id);
        if (orderEntry != null)
        {
            _context.OrderEntries.Remove(orderEntry);
            _context.SaveChanges();
        }
    }
}