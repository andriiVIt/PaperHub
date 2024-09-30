using DataAccess.Models;

namespace Service;

public interface IOrderService
{
    List<GetOrderDto> GetAllOrders(int limit, int startAt);
    GetOrderDto? GetOrderById(int id);
    void CreateOrder(OrderCreateDto orderDto);
    void UpdateOrder(int id, GetOrderDto getOrderDto);
    void DeleteOrder(int id);
}