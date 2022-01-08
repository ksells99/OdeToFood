using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Services
{
    public class OdeToFoodDbContext: DbContext
    {
        // In a table named Restaurants, there will be some data returned that matches Restaurant model - cols for id, name, cuisine
        // Will also abide by requirements etc. specified in model - e.g. required field, min/max char length
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
