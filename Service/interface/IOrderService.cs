using DataAccess.Models;

namespace Service;

public interface IOrderService
{
    List<OrderDto> GetAllOrders(int limit, int startAt);
    OrderDto? GetOrderById(int id);
    void CreateOrder(OrderDto orderDto);
    void UpdateOrder(int id, OrderDto orderDto);
    void DeleteOrder(int id);
}