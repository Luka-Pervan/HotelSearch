namespace HotelSearch.Core.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public GeoLocation Location { get; set; }

    }

    // In-memory data store
    public static class HotelDataStore
    {
        public static List<Hotel> Hotels { get; } = new List<Hotel>
        {
            new Hotel
            {
                Id = 1,
                Name = "Sunrise Resort",
                Price = 120.00m,
                Location = new GeoLocation(34.0522, -118.2437) // Close to user's location
            },
            new Hotel
            {
                Id = 2,
                Name = "Seaside Inn",
                Price = 95.00m,
                Location = new GeoLocation(34.0522, -118.2537) // Slightly farther from user's location
            },
            new Hotel
            {
                Id = 3,
                Name = "Mountain View Hotel",
                Price = 150.00m,
                Location = new GeoLocation(34.0722, -118.2937) // Farther from user's location
            },
            new Hotel
            {
                Id = 4,
                Name = "City Center Stay",
                Price = 85.00m,
                Location = new GeoLocation(34.0522, -118.2637) // Close to user's location and cheaper
            },
            new Hotel
            {
                Id = 5,
                Name = "Lakeside Lodge",
                Price = 110.00m,
                Location = new GeoLocation(34.0622, -118.2437) // Close and moderately priced
            },
            new Hotel
            {
                Id = 6,
                Name = "Urban Getaway",
                Price = 180.00m,
                Location = new GeoLocation(34.1122, -118.2937) // Farther and more expensive
            },
            new Hotel
            {
                Id = 7,
                Name = "Budget Suites",
                Price = 70.00m,
                Location = new GeoLocation(34.0422, -118.2437) // Close and very cheap
            },
            new Hotel
            {
                Id = 8,
                Name = "Luxury Palace",
                Price = 300.00m,
                Location = new GeoLocation(34.0922, -118.2537) // Expensive and moderately far
            },
            new Hotel
            {
                Id = 9,
                Name = "Family Inn",
                Price = 105.00m,
                Location = new GeoLocation(34.0522, -118.2637) // Reasonably close and mid-priced
            },
            new Hotel
            {
                Id = 10,
                Name = "Hilltop Escape",
                Price = 130.00m,
                Location = new GeoLocation(34.0822, -118.2837) // Mid-priced and moderately far
            }
        };

    }

}
