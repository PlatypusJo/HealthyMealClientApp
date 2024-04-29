using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.MockDataStore
{
    public class RecipeDataStore : IDataStore<RecipeModel>
    {
        private readonly List<RecipeModel> _data;

        public RecipeDataStore() 
        {
            _data = [
                new()
                {
                    Id = "1",
                    FoodId = "6",
                    MealTypeId = "1",
                    CookingTime = new TimeSpan(0, 40, 0),
                    MealTypeName = "Завтрак",
                    Name = "Морковный суп",
                    Description = "Морковь, суп, супы, первое блюдо",
                    UserId = "Common",
                    Kcal = 225,
                    Proteins = 103,
                    Fats = 54,
                    Carbohydrates = 188
                },
                new()
                {
                    Id = "2",
                    FoodId = "7",
                    MealTypeId = "2",
                    CookingTime = new TimeSpan(1, 10, 0),
                    MealTypeName = "Обед",
                    Name = "Котлеты на пару, с фасолью",
                    Description = "Котлеты, фасоль",
                    UserId = "Common",
                    Kcal = 155,
                    Proteins = 120,
                    Fats = 22,
                    Carbohydrates = 201
                },
                new()
                {
                    Id = "3",
                    FoodId = "8",
                    MealTypeId = "2",
                    CookingTime = new TimeSpan(1, 30, 0),
                    MealTypeName = "Обед",
                    Name = "Морковный кекс",
                    Description = "Морковь, кекс, кексы, десерт, сладкое",
                    UserId = "Common",
                    Kcal = 242,
                    Proteins = 98,
                    Fats = 33,
                    Carbohydrates = 231
                },
                new()
                {
                    Id = "4",
                    FoodId = "9",
                    MealTypeId = "3",
                    CookingTime = new TimeSpan(0, 30, 0),
                    MealTypeName = "Ужин",
                    Name = "Гречневая каша с мёдом",
                    Description = "Гречневая крупа, гречка, каша, мёд, десерт, сладкое",
                    UserId = "Common",
                    Kcal = 203,
                    Proteins = 11,
                    Fats = 6,
                    Carbohydrates = 199
                },
                ];
        }

        public async Task<bool> AddItemAsync(RecipeModel item)
        {
            _data.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((RecipeModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<RecipeModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<RecipeModel>> GetAllItemsAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(RecipeModel item)
        {
            var oldItem = _data.Where((RecipeModel arg) => arg.Id == item.Id).FirstOrDefault();
            _data.Remove(oldItem);
            _data.Add(item);

            return await Task.FromResult(true);
        }
    }
}
