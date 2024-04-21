using HealthyMeal.Intefaces;
using HealthyMeal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HealthyMeal.Models
{
    public class MealTypeModel
    {
        #region Поля

        private int _countMeals;

        private double _kcalCount;

        #endregion

        #region Свойства

        public string Id { get; set; } = null!;

        public string Name { get; set; }

        public MealType Type { get; set; }

        #nullable enable
        public byte[]? Icon { get; set; }

        public int CountMeals => _countMeals;

        public double KcalCount => _kcalCount;

        #endregion

        #region Методы

        public void CalcKcalCount(IReadOnlyList<INutritionalValue> nutritionalValues)
        {
            _kcalCount = nutritionalValues.Sum(x => x.Kcal);
            _countMeals = nutritionalValues.Count();
        }

        #endregion
    }
}
