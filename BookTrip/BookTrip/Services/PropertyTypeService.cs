using BookTrip.Database;
using BookTrip.Interfaces;
using BookTrip.Models.HotelModel;

namespace BookTrip.Services
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private AppDbContext database;
        public PropertyTypeService(AppDbContext database)
        {
            this.database = database;
        }
        public List<PropertyType> GetAllPropertyTypes()
        {
            return database.PropertyTypes.ToList();
        }
        public PropertyType AddNewPropertyType(string type)
        {
            var newType = new PropertyType(type);
            database.PropertyTypes.Add(newType);
            database.SaveChanges();
            return newType;
        }
        public PropertyType GetPropType(string type)
        {
            return database.PropertyTypes.Find(type);
        }
    }
}
