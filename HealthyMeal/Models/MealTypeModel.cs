using HealthyMeal.Intefaces;
using HealthyMeal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

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

        public MealType Type => MealTypesProvider.Provide(Name);

        #nullable enable
        public byte[]? Icon { get; set; }

        public int CountMeals => _countMeals;

        public double KcalCount => _kcalCount;

        #endregion

        #region Методы

        public void CalcKcalCount(IReadOnlyList<IMealNutritionalValue> nutritionalValues)
        {
            nutritionalValues = nutritionalValues.Where(n => n.MealTypeId == Id).ToList();
            _kcalCount = nutritionalValues.Sum(n => n.Kcal);
            _countMeals = nutritionalValues.Count();
        }

        public void ResetKcalCount()
        {
            _kcalCount = 0;
        }

        #endregion
    }
}
