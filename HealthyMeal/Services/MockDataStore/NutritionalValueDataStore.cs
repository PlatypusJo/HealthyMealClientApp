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
                    IsDefault = false,
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
                    IsDefault = false,
                    Kcal = 303,
                    Proteins = 7.5,
                    Fats = 2.6,
                    Carbohydrates = 62.3,
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

        public async Task<IEnumerable<NutritionalValueModel>> GetAllItemsAsync()
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
