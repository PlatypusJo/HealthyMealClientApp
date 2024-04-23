using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Intefaces
{
    public interface IModelWithMealType : INutritionalValue
    {
        string MealTypeId { get; set; }
    }
}
