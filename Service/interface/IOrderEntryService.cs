namespace Service;

public interface IOrderEntryService
{
    List<GetOrderEntryDto> GetAllOrderEntries();
    GetOrderEntryDto? GetOrderEntryById(int id);
    void CreateOrderEntry(OrderEntryCreateDto orderEntryDto);
    void UpdateOrderEntry(int id, GetOrderEntryDto getOrderEntryDto);
    void DeleteOrderEntry(int id);
}