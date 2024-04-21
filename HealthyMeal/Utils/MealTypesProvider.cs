using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Utils
{
    public enum MealType
    {
        Breakfast = 0,
        Lunch,
        Dinner,
        Snack
    }

    public static class MealTypesProvider
    {
        #region Поля

        public static readonly Dictionary<string, MealType> _dictionary = [];

        #endregion

        #region Методы

        public static void RegisterAll()
        {
            _dictionary.Clear();
            Register("Завтрак", MealType.Breakfast);
            Register("Обед", MealType.Lunch);
            Register("Ужин", MealType.Dinner);
            Register("Перекус", MealType.Snack);
        }

        public static void Register(string name, MealType type)
        {
            _dictionary.Add(name, type);
        }

        public static MealType Provide(string name)
        {
            return _dictionary[name];
        }

        #endregion
    }
}
