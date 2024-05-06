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

            searchBarText = Regex.Replace(searchBarText, @"[\(\s!@\#\$%\^&\*\(\)_\+=\-'\\:\|/`~\.,\{}\)]", "");
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

        public async Task<bool> GetAnyFoodBySearchText(string userId, string searchText)
        {
            if (searchText == string.Empty) 
                return true;

            string pattern = $"\\b{searchText}\\b";

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

            return foods.Exists(f => Regex.IsMatch(f.Name, pattern) || Regex.IsMatch(f.Description, pattern));
        }
    }
}
