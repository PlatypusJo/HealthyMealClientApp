using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Services.Classes
{
    public class UnitOfWork
    {
        private FoodDataStore _foodDataStore;
        private MealDataStore _mealDataStore;
        private MealTypeDataStore _mealTypeDataStore;
        private UnitsDataStore _unitsDataStore;
        private NutritionalValueDataStore _nutritionalValueDataStore;

        public FoodDataStore Foods
        {
            get 
            {
                _foodDataStore ??= new();
                return _foodDataStore; 
            }
        }

        public MealDataStore Meals
        {
            get 
            {
                _mealDataStore ??= new();
                return _mealDataStore; 
            }
        }

        public MealTypeDataStore MealTypes
        {
            get 
            {
                _mealTypeDataStore ??= new();
                return _mealTypeDataStore; 
            }
        }

        public UnitsDataStore Units
        {
            get 
            {
                _unitsDataStore ??= new();
                return _unitsDataStore; 
            }
        }

        public NutritionalValueDataStore NutritionalValues
        {
            get 
            {
                _nutritionalValueDataStore ??= new();
                return _nutritionalValueDataStore; 
            }
        }
    }
}
