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
        private IDataStore<UserModel> _userDataStore;
        private IDataStore<SexModel> _sexDataStore;
        private IDataStore<PhysicalActivityModel> _physicalActivityDataStore;
        private IDataStore<RecipeModel> _recipeDataStore;
        private IDataStore<IngredientModel> _ingredientDataStore;
        private IDataStore<StepModel> _stepDataStore;
        private IDataStore<MenuModel> _menuDataStore;
        private IDataStore<MenuStringModel> _menuStringDataStore;
        private IDataStore<ProductToBuyModel> _productToBuyDataStore;

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

        public IDataStore<UserModel> Users
        {
            get
            {
                _userDataStore ??= new UserDataStore();
                return _userDataStore;
            }
        }

        public IDataStore<SexModel> Sexes
        {
            get
            {
                _sexDataStore ??= new SexDataStore();
                return _sexDataStore;
            }
        }

        public IDataStore<PhysicalActivityModel> PhysicalActivities
        {
            get
            {
                _physicalActivityDataStore ??= new PhysicalActivityDataStore();
                return _physicalActivityDataStore;
            }
        }

        public IDataStore<RecipeModel> Recipes
        {
            get
            {
                _recipeDataStore ??= new RecipeDataStore();
                return _recipeDataStore;
            }
        }

        public IDataStore<IngredientModel> Ingredients
        {
            get
            {
                _ingredientDataStore ??= new IngredientDataStore();
                return _ingredientDataStore;
            }
        }

        public IDataStore<StepModel> Steps
        {
            get
            {
                _stepDataStore ??= new StepDataStore();
                return _stepDataStore;
            }
        }

        public IDataStore<MenuModel> Menus
        {
            get
            {
                _menuDataStore ??= new MenuDataStore();
                return _menuDataStore;
            }
        }

        public IDataStore<MenuStringModel> MenuStrings
        {
            get
            {
                _menuStringDataStore ??= new MenuStringDataStore();
                return _menuStringDataStore;
            }
        }

        public IDataStore<ProductToBuyModel> ProductsToBuy
        {
            get
            {
                _productToBuyDataStore ??= new ProductToBuyDataStore();
                return _productToBuyDataStore;
            }
        }
    }
}
