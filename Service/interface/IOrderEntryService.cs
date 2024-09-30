using Service.DTO.UpdateDto;

namespace Service;

public interface IOrderEntryService
{
    List<GetOrderEntryDto> GetAllOrderEntries();
    GetOrderEntryDto? GetOrderEntryById(int id);
    void CreateOrderEntry(CreateOrderEntryDto createOrderEntryDto);
    void UpdateOrderEntry(int id, UpdateOrderEntryDto updateOrderEntryDto);
    void DeleteOrderEntry(int id);
}