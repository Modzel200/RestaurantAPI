using RestaurantAPI.Entities;

namespace RestaurantAPI;

public class RestaurantSeeder
{
    private RestaurantDbContext _dbContext;
    public RestaurantSeeder(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _dbContext.Restaurants.AddRange(restaurants);
                _dbContext.SaveChanges();
            }
        }
    }

    public IEnumerable<Restaurant> GetRestaurants()
    {
        var restaurants = new List<Restaurant>()
        {
            new Restaurant()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description = "Kentucky Fried Chicken",
                ContactEmail = "contact@kfc.com",
                ContactNumber = "512234567",
                HasDelivery = true,
                Dishies = new List<Dish>()
                {
                    new Dish()
                    {
                        Name = "Nashville Hot Chicken",
                        Price = 10.30M,
                        Description = "Short Description",
                    },
                    new Dish()
                    {
                        Name = "Chicken Nuggets",
                        Price = 5.30M,
                        Description = "Short Description",
                    },
                },
                Address = new Address()
                {
                    City = "Kraków",
                    Street = "Długa 5",
                    PostalCode = "30-001"
                }
            }

        };
        return restaurants;
    }
}