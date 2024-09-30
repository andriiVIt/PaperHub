using DataAccess;
using DataAccess.Models;
using Service.DTO.UpdateDto;

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

    public void CreateOrderEntry(CreateOrderEntryDto createOrderEntryDto)
    {
        var orderEntry = CreateOrderEntryDto.ToEntity(createOrderEntryDto);
        _context.OrderEntries.Add(orderEntry);
        _context.SaveChanges();
    }

    public void UpdateOrderEntry(int id, UpdateOrderEntryDto updateOrderEntryDto)
    {
        var orderEntry = _context.OrderEntries.Find(id);
        if (orderEntry != null)
        {
            orderEntry.Quantity = updateOrderEntryDto.Quantity;
            orderEntry.ProductId = updateOrderEntryDto.ProductId;
            orderEntry.OrderId = updateOrderEntryDto.OrderId;
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