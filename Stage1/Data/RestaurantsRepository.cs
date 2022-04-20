using Stage1.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stage1.Data
{
    public class RestaurantsRepository
    {
        private readonly ReservationContext _reservationContext;

        public RestaurantsRepository(ReservationContext reservationContext)
        {
            _reservationContext = reservationContext;
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            return _reservationContext.Restaurants.ToList();
        }

        public Restaurant GetRestaurant(int id)
        {
            return _reservationContext.Restaurants.Find(id);
        }

        public async Task AddRestaurant(string name)
        {
            var restaurant = new Restaurant() { Name = name };
            _reservationContext.Restaurants.Add(restaurant);

            await _reservationContext.SaveChangesAsync();
        }
    }
}
