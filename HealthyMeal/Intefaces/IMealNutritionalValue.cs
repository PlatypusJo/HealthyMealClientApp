using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Intefaces
{
    public interface IMealNutritionalValue : INutritionalValue
    {
        string MealTypeId { get; set; }
    }
}
