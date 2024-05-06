using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.MockDataStore
{
    public class NutritionalValueDataStore : IDataStore<NutritionalValueModel>
    {
        List<NutritionalValueModel> _data;

        public NutritionalValueDataStore() 
        {
            _data = [
                new()
                {
                    Id = "1",
                    FoodId = "1",
                    UnitsId = "2",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 122,
                    Proteins = 46,
                    Fats = 88,
                    Carbohydrates = 144,
                },
                new()
                {
                    Id = "2",
                    FoodId = "1",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = false,
                    Kcal = 410,
                    Proteins = 8,
                    Fats = 9.5,
                    Carbohydrates = 72,
                },
                new()
                {
                    Id = "3",
                    FoodId = "2",
                    UnitsId = "2",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 82.7,
                    Proteins = 0.7,
                    Fats = 0.7,
                    Carbohydrates = 17.2,
                },
                new()
                {
                    Id = "4",
                    FoodId = "2",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = false,
                    Kcal = 47,
                    Proteins = 0.4,
                    Fats = 0.4,
                    Carbohydrates = 9.8,
                },
                new()
                {
                    Id = "5",
                    FoodId = "3",
                    UnitsId = "2",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 96,
                    Proteins = 1.5,
                    Fats = 0.5,
                    Carbohydrates = 21,
                },
                new()
                {
                    Id = "6",
                    FoodId = "3",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = false,
                    Kcal = 115,
                    Proteins = 2.4,
                    Fats = 0.8,
                    Carbohydrates = 32,
                },
                new()
                {
                    Id = "7",
                    FoodId = "4",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = true,
                    Kcal = 118,
                    Proteins = 4.2,
                    Fats = 1.1,
                    Carbohydrates = 21.3,
                },
                new()
                {
                    Id = "8",
                    FoodId = "5",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = true,
                    Kcal = 303,
                    Proteins = 7.5,
                    Fats = 2.6,
                    Carbohydrates = 62.3,
                },
                new()
                {
                    Id = "9",
                    FoodId = "6",
                    UnitsId = "3",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 225,
                    Proteins = 103,
                    Fats = 54,
                    Carbohydrates = 188
                },
                new()
                {
                    Id = "10",
                    FoodId = "7",
                    UnitsId = "3",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 155,
                    Proteins = 120,
                    Fats = 22,
                    Carbohydrates = 201
                },
                new()
                {
                    Id = "11",
                    FoodId = "8",
                    UnitsId = "3",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 242,
                    Proteins = 98,
                    Fats = 33,
                    Carbohydrates = 231
                },
                new()
                {
                    Id = "12",
                    FoodId = "9",
                    UnitsId = "3",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 203,
                    Proteins = 11,
                    Fats = 6,
                    Carbohydrates = 199
                },
                new()
                {
                    Id = "13",
                    FoodId = "10",
                    UnitsId = "2",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 77,
                    Proteins = 6.2,
                    Fats = 5.6,
                    Carbohydrates = 0.3
                },
                new()
                {
                    Id = "14",
                    FoodId = "11",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = true,
                    Kcal = 399,
                    Proteins = 0,
                    Fats = 0,
                    Carbohydrates = 99.8
                },
                new()
                {
                    Id = "15",
                    FoodId = "12",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = true,
                    Kcal = 334,
                    Proteins = 10.8,
                    Fats = 1.3,
                    Carbohydrates = 69.9
                },
                new()
                {
                    Id = "16",
                    FoodId = "13",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = true,
                    Kcal = 293,
                    Proteins = 15.8,
                    Fats = 25,
                    Carbohydrates = 0
                },
                new()
                {
                    Id = "17",
                    FoodId = "14",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = true,
                    Kcal = 111,
                    Proteins = 11.1,
                    Fats = 3.2,
                    Carbohydrates = 13
                },
                new()
                {
                    Id = "18",
                    FoodId = "15",
                    UnitsId = "2",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 35,
                    Proteins = 1.3,
                    Fats = 0.1,
                    Carbohydrates = 6.9
                },
                new()
                {
                    Id = "19",
                    FoodId = "16",
                    UnitsId = "2",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 40,
                    Proteins = 1.1,
                    Fats = 0.1,
                    Carbohydrates = 9
                },
                new()
                {
                    Id = "20",
                    FoodId = "17",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = true,
                    Kcal = 380,
                    Proteins = 23.5,
                    Fats = 30.8,
                    Carbohydrates = 3.4
                },
                new()
                {
                    Id = "21",
                    FoodId = "18",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = true,
                    Kcal = 322,
                    Proteins = 0.3,
                    Fats = 0,
                    Carbohydrates = 80.3
                },
                new()
                {
                    Id = "22",
                    FoodId = "19",
                    UnitsId = "2",
                    UnitsAmount = 1,
                    IsDefault = true,
                    Kcal = 45,
                    Proteins = 0.5,
                    Fats = 1.9,
                    Carbohydrates = 6.3
                },
                new()
                {
                    Id = "23",
                    FoodId = "20",
                    UnitsId = "1",
                    UnitsAmount = 100,
                    IsDefault = true,
                    Kcal = 356,
                    Proteins = 24.9,
                    Fats = 27.4,
                    Carbohydrates = 2.2
                },
                ];
        }

        public async Task<bool> AddItemAsync(NutritionalValueModel item)
        {
            _data.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((NutritionalValueModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<NutritionalValueModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<NutritionalValueModel>> GetAllItemsAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(NutritionalValueModel item)
        {
            var oldItem = _data.Where((NutritionalValueModel arg) => arg.Id == item.Id).FirstOrDefault();
            _data.Remove(oldItem);
            _data.Add(item);

            return await Task.FromResult(true);
        }
    }
}
