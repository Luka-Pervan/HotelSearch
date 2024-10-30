using System.Threading.Tasks;
using HotelSearch.Core.Helpers;

namespace HotelSearch.Infrastructure.ExternalServices
{
    public class GeoLocationService
    {
        public double CalculateDistance(double userLat, double userLon, double hotelLat, double hotelLon)
        {
            // Using GeoDistanceCalculator from Core.Helpers
            return GeoDistanceCalculator.CalculateDistance(userLat, userLon, hotelLat, hotelLon);
        }

        // If calling an external API for geolocation, add methods here for that integration
    }
}
