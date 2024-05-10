using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HealthyMeal.Services.BLL
{
    public class BlService
    {
        private IGlobalDataStore _globalDataStore;

        public BlService(IGlobalDataStore store)
        {
            _globalDataStore = store;
        }

        public async Task<GetFoodPageResponseModel> GetFoodPage(string userId, int pageSize, int curPage, string searchBarText)
        {
            GetFoodPageResponseModel response = new();

            //searchBarText = Regex.Replace(searchBarText, @"[\(\s!@\#\$%\^&\*\(\)_\+=\-'\\:\|/`~\.,\{}\)]", "");
            searchBarText = Regex.Escape(searchBarText);
            string pattern = $"\\b{searchBarText}\\b";

            // Формирование списка еды без рецептов.
            List<FoodModel> foods = [];
            List<RecipeModel> recipes = [];
            foods = await _globalDataStore.Foods.GetAllItemsAsync();
            recipes = await _globalDataStore.Recipes.GetAllItemsAsync();
            List<FoodModel> recipesInFood = [];
            for (int i = 0; i < recipes.Count; i++)
            {
                FoodModel food = foods.Find(f => f.Id == recipes[i].FoodId);
                recipesInFood.Add(food);
            }
            foods = foods.Except(recipesInFood).ToList();
                        
            // Отбор элементов с совпадениями по тексту.
            if (searchBarText != string.Empty)
            {
                foods = foods.Where(f => Regex.IsMatch(f.Name, pattern, RegexOptions.IgnoreCase) || Regex.IsMatch(f.Description, pattern, RegexOptions.IgnoreCase)).ToList();
            }

            // Формирование ответа.
            int skipAmount = (curPage - 1) * pageSize;
            response.Foods = foods.Skip(skipAmount).Take(pageSize).ToList();
            response.Count = foods.Count;

            return response;
        }

        public async Task<GetRecipePageResponseModel> GetRecipePage(string userId, int pageSize, int curPage, string searchBarText)
        {
            GetRecipePageResponseModel response = new();

            searchBarText = Regex.Escape(searchBarText);
            string pattern = $"\\b{searchBarText}\\b";

            // Формирование списка еды без рецептов.
            List<RecipeModel> recipes = [];
            recipes = await _globalDataStore.Recipes.GetAllItemsAsync();
                        
            // Отбор элементов с совпадениями по тексту.
            if (searchBarText != string.Empty)
            {
                recipes = recipes.Where(f => Regex.IsMatch(f.Name, pattern, RegexOptions.IgnoreCase) || Regex.IsMatch(f.Description, pattern, RegexOptions.IgnoreCase)).ToList();
            }

            // Формирование ответа.
            int skipAmount = (curPage - 1) * pageSize;
            response.Recipes = recipes.Skip(skipAmount).Take(pageSize).ToList();
            response.Count = recipes.Count;

            return response;
        }

        public async Task<GetShopListPageResponseModel> GetShopListPage(string userId, int pageSize, int curPage, DateTime date)
        {
            GetShopListPageResponseModel response = new();

            // Формирование списка еды без рецептов.
            List<ProductToBuyModel> productsToBuy = [];
            productsToBuy = await _globalDataStore.ProductsToBuy.GetAllItemsAsync();
                        
            // Отбор элементов с совпадениями по дате.
            productsToBuy = productsToBuy.Where(p => p.Date == date).ToList();

            // Формирование ответа.
            int skipAmount = (curPage - 1) * pageSize;
            response.ShopList = productsToBuy.Skip(skipAmount).Take(pageSize).ToList();
            response.Count = productsToBuy.Count;

            return response;
        }

        public async Task<GetMealsPageResponseModel> GetMealsPage(string userId, string mealTypeId, int pageSize, int curPage, DateTime date)
        {
            GetMealsPageResponseModel response = new();

            List<MealModel> meals = [];
            meals = await _globalDataStore.Meals.GetAllItemsAsync();

            // Отбор элементов с совпадениями по дате
            meals = meals.Where(m => m.UserId == userId && m.MealTypeId == mealTypeId && m.Date == date).ToList();

            // Формирование ответа.
            int skipAmount = (curPage - 1) * pageSize;
            response.Meals = meals.Skip(skipAmount).Take(pageSize).ToList();
            response.Count = meals.Count;

            return response;
        }
    }
}
