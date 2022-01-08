using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Kai's Pizza", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Chungs", Cuisine = CuisineType.Chinese },
                new Restaurant { Id = 3, Name = "Bayleaf", Cuisine = CuisineType.Indian }
            };
        }

    
        // GET all restaurants

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants.OrderBy(r => r.Name);
        }

        // GET restaurant by ID
        public Restaurant Get(int id)
        {
            // returns either restaurant or null
            return restaurants.FirstOrDefault(r => r.Id == id);
        }

        // Add new restaurant
        public void Add(Restaurant restaurant)
        {
            restaurants.Add(restaurant);
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
        }

        // Edit restaurant
        public void Update(Restaurant restaurant)
        {
            // Find restaurant from DB
            var existing = Get(restaurant.Id);

            // If it exists, proceed and update name/cuisine
            if(existing != null)
            {
                existing.Name = restaurant.Name;
                existing.Cuisine = restaurant.Cuisine;
            }
        }

        // Delete restaurant
        public void Delete(int id)
        {
            // Find restaurant from DB
            var restaurant = Get(id);

            // If it exists, proceed and remove it
            if(restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
        }
    }
}
