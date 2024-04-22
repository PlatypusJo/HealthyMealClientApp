using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Services.MockDataStore
{
    public class GlobalDataStore : IGlobalDataStore
    {
        private IDataStore<FoodModel> _foodDataStore;
        private IDataStore<MealModel> _mealDataStore;
        private IDataStore<MealTypeModel> _mealTypeDataStore;
        private IDataStore<UnitsModel> _unitsDataStore;
        private IDataStore<NutritionalValueModel> _nutritionalValueDataStore;

        public IDataStore<FoodModel> Foods
        {
            get 
            {
                _foodDataStore ??= new FoodDataStore();
                return _foodDataStore; 
            }
        }

        public IDataStore<MealModel> Meals
        {
            get 
            {
                _mealDataStore ??= new MealDataStore();
                return _mealDataStore; 
            }
        }

        public IDataStore<MealTypeModel> MealTypes
        {
            get 
            {
                _mealTypeDataStore ??= new MealTypeDataStore();
                return _mealTypeDataStore; 
            }
        }

        public IDataStore<UnitsModel> Units
        {
            get 
            {
                _unitsDataStore ??= new UnitsDataStore();
                return _unitsDataStore; 
            }
        }

        public IDataStore<NutritionalValueModel> NutritionalValues
        {
            get 
            {
                _nutritionalValueDataStore ??= new NutritionalValueDataStore();
                return _nutritionalValueDataStore; 
            }
        }
    }
}
