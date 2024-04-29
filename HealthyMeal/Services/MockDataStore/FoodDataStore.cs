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
                    Kcal = 82.7,
                    Proteins = 0.7,
                    Fats = 0.7,
                    Carbohydrates = 17.2,
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
                new()
                {
                    Id = "6",
                    Name = "Морковный суп",
                    Description = "Морковь, суп, супы, первое блюдо",
                    UserId = "Common",
                    DefaultUnitsName = "порция",
                    DefaultUnitsAmount = 1,
                    Kcal = 225,
                    Proteins = 103,
                    Fats = 54,
                    Carbohydrates = 188
                },
                new()
                {
                    Id = "7",
                    Name = "Котлеты на пару, с фасолью",
                    Description = "Котлеты, фасоль",
                    UserId = "Common",
                    DefaultUnitsName = "порция",
                    DefaultUnitsAmount = 1,
                    Kcal = 155,
                    Proteins = 120,
                    Fats = 22,
                    Carbohydrates = 201
                },
                new()
                {
                    Id = "8",
                    Name = "Морковный кекс",
                    Description = "Морковь, кекс, кексы, десерт, сладкое",
                    UserId = "Common",
                    DefaultUnitsName = "порция",
                    DefaultUnitsAmount = 1,
                    Kcal = 242,
                    Proteins = 98,
                    Fats = 33,
                    Carbohydrates = 231
                },
                new()
                {
                    Id = "9",
                    Name = "Гречневая каша с мёдом",
                    Description = "Гречневая крупа, гречка, каша, мёд, десерт, сладкое",
                    UserId = "Common",
                    DefaultUnitsName = "порция",
                    DefaultUnitsAmount = 1,
                    Kcal = 203,
                    Proteins = 11,
                    Fats = 6,
                    Carbohydrates = 199
                },
                new()
                {
                    Id = "10",
                    Name = "Яйцо куриное",
                    Description = "яйцо",
                    UserId = "Common",
                    DefaultUnitsName = "шт",
                    DefaultUnitsAmount = 1,
                    Kcal = 77,
                    Proteins = 6.2,
                    Fats = 5.6,
                    Carbohydrates = 0.3
                },
                new()
                {
                    Id = "11",
                    Name = "Сахар белый",
                    Description = "сахар",
                    UserId = "Common",
                    DefaultUnitsName = "г",
                    DefaultUnitsAmount = 100,
                    Kcal = 399,
                    Proteins = 0,
                    Fats = 0,
                    Carbohydrates = 99.8
                },
                new()
                {
                    Id = "12",
                    Name = "Мука пшеничная (высшего сорта)",
                    Description = "мука, пшеничная, пшеница",
                    UserId = "Common",
                    DefaultUnitsName = "г",
                    DefaultUnitsAmount = 100,
                    Kcal = 334,
                    Proteins = 10.8,
                    Fats = 1.3,
                    Carbohydrates = 69.9
                },
                new()
                {
                    Id = "13",
                    Name = "Фарш говяжий",
                    Description = "фарш, мясо, говядина",
                    UserId = "Common",
                    DefaultUnitsName = "г",
                    DefaultUnitsAmount = 100,
                    Kcal = 293,
                    Proteins = 15.8,
                    Fats = 25,
                    Carbohydrates = 0
                },
                new()
                {
                    Id = "14",
                    Name = "Фасоль стручковая",
                    Description = "фасоль, бобовые",
                    UserId = "Common",
                    DefaultUnitsName = "г",
                    DefaultUnitsAmount = 100,
                    Kcal = 111,
                    Proteins = 11.1,
                    Fats = 3.2,
                    Carbohydrates = 13
                },
                new()
                {
                    Id = "15",
                    Name = "Морковь",
                    Description = "морковь",
                    UserId = "Common",
                    DefaultUnitsName = "шт",
                    DefaultUnitsAmount = 1,
                    Kcal = 35,
                    Proteins = 1.3,
                    Fats = 0.1,
                    Carbohydrates = 6.9
                },
                new()
                {
                    Id = "16",
                    Name = "Лук",
                    Description = "лук",
                    UserId = "Common",
                    DefaultUnitsName = "шт",
                    DefaultUnitsAmount = 1,
                    Kcal = 40,
                    Proteins = 1.1,
                    Fats = 0.1,
                    Carbohydrates = 9
                },
                new()
                {
                    Id = "17",
                    Name = "Сыр чедер",
                    Description = "сыр, сыры, молочный продукт, кисломолочный",
                    UserId = "Common",
                    DefaultUnitsName = "г",
                    DefaultUnitsAmount = 100,
                    Kcal = 380,
                    Proteins = 23.5,
                    Fats = 30.8,
                    Carbohydrates = 3.4
                },
                new()
                {
                    Id = "18",
                    Name = "Мёд липовый",
                    Description = "мёд, сладкое",
                    UserId = "Common",
                    DefaultUnitsName = "г",
                    DefaultUnitsAmount = 100,
                    Kcal = 322,
                    Proteins = 0.3,
                    Fats = 0,
                    Carbohydrates = 80.3
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
