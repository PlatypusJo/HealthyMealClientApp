using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Services.Interfaces
{
    /// <summary>
    /// Interface that unites DataStores with one data data context and encapsulates their functionality (similar UnitOfWork)
    /// </summary>
    public interface IGlobalDataStore
    {
        IDataStore<FoodModel> Foods { get; }
        IDataStore<MealTypeModel> MealTypes { get; }
        IDataStore<MealModel> Meals { get; }
        IDataStore<UnitsModel> Units { get; }
        IDataStore<NutritionalValueModel> NutritionalValues { get; }
        IDataStore<UserModel> Users { get; }
        IDataStore<SexModel> Sexes { get; }
        IDataStore<PhysicalActivityModel> PhysicalActivities { get; }
        IDataStore<RecipeModel> Recipes { get; }
        IDataStore<IngredientModel> Ingredients { get; }
        IDataStore<StepModel> Steps { get; }
        IDataStore<MenuModel> Menus { get; }
        IDataStore<MenuStringModel> MenuStrings { get; }
        IDataStore<ProductToBuyModel> ProductsToBuy { get; }
    }
}
