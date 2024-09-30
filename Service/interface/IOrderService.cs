using DataAccess.Models;
using Service.DTO.UpdateDto;

namespace Service;

public interface IOrderService
{
    List<GetOrderDto> GetAllOrders(int limit, int startAt);
    GetOrderDto? GetOrderById(int id);
    void CreateOrder(CreateOrderDto createOrderDto);
    void UpdateOrder(int id, UpdateOrderDto updateOrderDto);
    void DeleteOrder(int id);
}