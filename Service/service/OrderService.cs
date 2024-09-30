using DataAccess;
using DataAccess.Models;

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

    public void CreateOrder(OrderCreateDto orderDto)
    {
        var order = OrderCreateDto.ToEntity(orderDto);
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void UpdateOrder(int id, GetOrderDto getOrderDto)
    {
        var order = _context.Orders.Find(id);
        if (order != null)
        {
            order.Status = getOrderDto.Status;
            order.OrderDate = getOrderDto.OrderDate;
            order.DeliveryDate = getOrderDto.DeliveryDate;
            order.TotalAmount = getOrderDto.TotalAmount;
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