using DataAccess.Models;

namespace DataAccess;

public interface IOrderEntryRepo
{
    List<OrderEntry> GetAllOrderEntries();
    OrderEntry? GetOrderEntryById(int id);
}