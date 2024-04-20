using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Intefaces
{
    public interface INutritionalValue
    {
        double Kcal { get; }
        double Proteins { get; }
        double Fats { get; }
        double Carbohydrates { get; }
    }
}
