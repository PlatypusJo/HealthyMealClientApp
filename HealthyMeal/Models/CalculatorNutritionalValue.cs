using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public static class CalculatorNutritionalValue
    {
        public static double CalcPercentOfNutrient(double nutrientAmount, double nutrientsTotalAmount) => nutrientsTotalAmount > 0 ? nutrientAmount / nutrientsTotalAmount * 100 : throw new Exception("Деление на ноль невозможно!");
    }
}
