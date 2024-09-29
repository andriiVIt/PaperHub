using DataAccess.Models;

namespace DataAccess;

public interface IOrderRepo
{
    List<Order> GetAllOrders();
    Order? GetOrderById(int id);
}