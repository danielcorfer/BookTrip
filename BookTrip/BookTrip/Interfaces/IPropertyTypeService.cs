using BookTrip.Models.HotelModel;

namespace BookTrip.Interfaces
{
    public interface IPropertyTypeService
    {
        PropertyType AddNewPropertyType(string type);
        List<PropertyType> GetAllPropertyTypes();
        PropertyType GetPropType(string type);
    }
}