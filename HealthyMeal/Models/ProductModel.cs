using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
    }
}
