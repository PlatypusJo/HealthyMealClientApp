using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.MockDataStore
{
    public class FoodDataStore : IDataStore<FoodModel>
    {
        readonly List<FoodModel> _data;

        public FoodDataStore() 
        {
            _data = [
                new()
                {
                    Id = "1",
                    Name = "Печенье (Любятово)",
                    Description = "печенье, любятово",
                    UserId = "Common",
                    DefaultUnitsName = "шт",
                    DefaultUnitsAmount = 1,
                    Kcal = 122,
                    Proteins = 46,
                    Fats = 88,
                    Carbohydrates = 144,
                },
                new()
                {
                    Id = "2",
                    Name = "Яблоко",
                    Description = "яблоко, фрукт",
                    UserId = "Common",
                    DefaultUnitsName = "шт",
                    DefaultUnitsAmount = 1,
                    Kcal = 47,
                    Proteins = 0.4,
                    Fats = 0.4,
                    Carbohydrates = 9.8,
                },
                new()
                {
                    Id = "3",
                    Name = "Банан",
                    Description = "банан, фрукт",
                    UserId = "Common",
                    DefaultUnitsName = "шт",
                    DefaultUnitsAmount = 1,
                    Kcal = 96,
                    Proteins = 1.5,
                    Fats = 0.5,
                    Carbohydrates = 21,
                },
                new()
                {
                    Id = "4",
                    Name = "Гречневая крупа",
                    Description = "гречка, крупа, крупы, гречневая крупа",
                    UserId = "Common",
                    DefaultUnitsName = "г",
                    DefaultUnitsAmount = 100,
                    Kcal = 118,
                    Proteins = 4.2,
                    Fats = 1.1,
                    Carbohydrates = 21.3,
                },
                new()
                {
                    Id = "5",
                    Name = "Рис",
                    Description = "рис, зерновые",
                    UserId = "Common",
                    DefaultUnitsName = "г",
                    DefaultUnitsAmount = 100,
                    Kcal = 303,
                    Proteins = 7.5,
                    Fats = 2.6,
                    Carbohydrates = 62.3,
                },
                ];
        }

        public async Task<bool> AddItemAsync(FoodModel item)
        {
            _data.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((FoodModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<FoodModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<FoodModel>> GetAllItemsAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(FoodModel item)
        {
            var oldItem = _data.Where((FoodModel arg) => arg.Id == item.Id).FirstOrDefault();
            _data.Remove(oldItem);
            _data.Add(item);

            return await Task.FromResult(true);
        }
    }
}
