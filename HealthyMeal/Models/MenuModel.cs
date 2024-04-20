using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class MenuModel : BaseNutritionalValueModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }
    }
}
