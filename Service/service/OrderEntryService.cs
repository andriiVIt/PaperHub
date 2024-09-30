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

    public List<GetOrderEntryDto> GetAllOrderEntries()
    {
        var orderEntries = _orderEntryRepo.GetAllOrderEntries();
        return orderEntries.Select(GetOrderEntryDto.FromEntity).ToList();
    }

    public GetOrderEntryDto? GetOrderEntryById(int id)
    {
        var orderEntry = _orderEntryRepo.GetOrderEntryById(id);
        return orderEntry != null ? GetOrderEntryDto.FromEntity(orderEntry) : null;
    }

    public void CreateOrderEntry(OrderEntryCreateDto orderEntryDto)
    {
        var orderEntry = OrderEntryCreateDto.ToEntity(orderEntryDto);
        _context.OrderEntries.Add(orderEntry);
        _context.SaveChanges();
    }

    public void UpdateOrderEntry(int id, GetOrderEntryDto getOrderEntryDto)
    {
        var orderEntry = _context.OrderEntries.Find(id);
        if (orderEntry != null)
        {
            orderEntry.Quantity = getOrderEntryDto.Quantity;
            orderEntry.ProductId = getOrderEntryDto.ProductId;
            orderEntry.OrderId = getOrderEntryDto.OrderId;
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