using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class ProductToBuyModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UnitsId { get; set; }
        public string UnitsName { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsBuy { get; set; }

    }
}
