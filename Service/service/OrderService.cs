using DataAccess;
using DataAccess.Models;
using Service.DTO.UpdateDto;

namespace Service;

public class OrderService : IOrderService
{
    private readonly IOrderRepo _orderRepo;
    private readonly MyDbContext _context;

    public OrderService(IOrderRepo orderRepo, MyDbContext context)
    {
        _orderRepo = orderRepo;
        _context = context;
    }

    public List<GetOrderDto> GetAllOrders(int limit, int startAt)
    {
        var orders = _orderRepo.GetAllOrders();
        return orders.OrderBy(o => o.Id)
            .Skip(startAt)
            .Take(limit)
            .Select(GetOrderDto.FromEntity)
            .ToList();
    }

    public GetOrderDto? GetOrderById(int id)
    {
        var order = _orderRepo.GetOrderById(id);
        return order != null ? GetOrderDto.FromEntity(order) : null;
    }

    public void CreateOrder(CreateOrderDto createOrderDto)
    {
        var order = CreateOrderDto.ToEntity(createOrderDto);
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void UpdateOrder(int id, UpdateOrderDto updateOrderDto)
    {
        var order = _context.Orders.Find(id);
        if (order != null)
        {
            order.Status = updateOrderDto.Status;
            order.OrderDate = updateOrderDto.OrderDate;
            order.DeliveryDate = updateOrderDto.DeliveryDate;
            order.TotalAmount = updateOrderDto.TotalAmount;
            _context.SaveChanges();
        }
    }

    public void DeleteOrder(int id)
    {
        var order = _context.Orders.Find(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}