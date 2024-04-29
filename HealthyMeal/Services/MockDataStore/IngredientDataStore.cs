using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.MockDataStore
{
    public class IngredientDataStore : IDataStore<IngredientModel>
    {
        private readonly List<IngredientModel> _data;

        public IngredientDataStore() 
        {
            _data = [
                new()
                {
                    Id = "1",
                    RecipeId = "1",
                    FoodId = "15",
                    UnitsId = "2",
                    UnitsAmount = 3,
                },
                new()
                {
                    Id = "2",
                    RecipeId = "1",
                    FoodId = "16",
                    UnitsId = "2",
                    UnitsAmount = 2,
                },
                new()
                {
                    Id = "3",
                    RecipeId = "1",
                    FoodId = "17",
                    UnitsId = "1",
                    UnitsAmount = 200,
                },
                new()
                {
                    Id = "4",
                    RecipeId = "2",
                    FoodId = "13",
                    UnitsId = "1",
                    UnitsAmount = 300,
                },
                new()
                {
                    Id = "5",
                    RecipeId = "2",
                    FoodId = "14",
                    UnitsId = "1",
                    UnitsAmount = 150,
                },
                new()
                {
                    Id = "6",
                    RecipeId = "4",
                    FoodId = "4",
                    UnitsId = "1",
                    UnitsAmount = 100,
                },
                new()
                {
                    Id = "7",
                    RecipeId = "4",
                    FoodId = "18",
                    UnitsId = "1",
                    UnitsAmount = 50,
                },
                new()
                {
                    Id = "8",
                    RecipeId = "3",
                    FoodId = "10",
                    UnitsId = "2",
                    UnitsAmount = 2,
                },
                new()
                {
                    Id = "9",
                    RecipeId = "3",
                    FoodId = "12",
                    UnitsId = "1",
                    UnitsAmount = 150,
                },
                new()
                {
                    Id = "10",
                    RecipeId = "3",
                    FoodId = "15",
                    UnitsId = "2",
                    UnitsAmount = 2,
                },
                ];
        }

        public async Task<bool> AddItemAsync(IngredientModel item)
        {
            _data.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((IngredientModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<IngredientModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<IngredientModel>> GetAllItemsAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(IngredientModel item)
        {
            var oldItem = _data.Where((IngredientModel arg) => arg.Id == item.Id).FirstOrDefault();
            _data.Remove(oldItem);
            _data.Add(item);

            return await Task.FromResult(true);
        }
    }
}
