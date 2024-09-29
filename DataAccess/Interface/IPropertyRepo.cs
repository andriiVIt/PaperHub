using DataAccess.Models;

namespace DataAccess;

public interface IPropertyRepo
{
    List<Property> GetAllProperties();
    Property? GetPropertyById(int id);
}