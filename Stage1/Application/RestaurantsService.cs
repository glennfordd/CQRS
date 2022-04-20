using Stage1.Data;
using Stage1.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stage1.Application
{
    public class RestaurantsService
    {
        private readonly RestaurantsRepository _restaurantsRepository;
        public RestaurantsService(RestaurantsRepository restaurantsRepository)
        {
            _restaurantsRepository = restaurantsRepository;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurantsRepository.GetRestaurants();
        }

        public async Task AddRestaurant(string name)
        {
            await _restaurantsRepository.AddRestaurant(name);
        }
    }
}
