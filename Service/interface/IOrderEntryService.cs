namespace Service;

public interface IOrderEntryService
{
    List<OrderEntryDto> GetAllOrderEntries();
    OrderEntryDto? GetOrderEntryById(int id);
    void CreateOrderEntry(OrderEntryCreateDto orderEntryDto);
    void UpdateOrderEntry(int id, OrderEntryDto orderEntryDto);
    void DeleteOrderEntry(int id);
}