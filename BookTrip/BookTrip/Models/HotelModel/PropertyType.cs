namespace BookTrip.Models.HotelModel
{
    public class PropertyType
    {
        public string Type { get; set; }
        public List<Hotel> Hotels { get; set; }
        private PropertyType()
        {

        }
        public PropertyType(string type)
        {
            Type = type;
        }
    }
}
