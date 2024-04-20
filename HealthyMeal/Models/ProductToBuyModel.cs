using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HealthyMeal.Models
{
    public class ProductToBuyModel
    {
        public string Id { get; set; }

        public string FoodId { get; set; }

        public string UnitsId { get; set; }

        public string UnitsName { get; set; }

        public string FoodName { get; set; }

        public double UnitsAmount { get; set; }

        public DateTime Date { get; set; }

        public bool IsBought { get; set; }
    }
}
