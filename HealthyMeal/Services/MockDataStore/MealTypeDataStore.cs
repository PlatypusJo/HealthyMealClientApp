using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using HealthyMeal.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.MockDataStore
{
    public class MealTypeDataStore : IDataStore<MealTypeModel>
    {
        readonly List<MealTypeModel> _data;

        public MealTypeDataStore() 
        {
            _data = [
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Завтрак",
                },
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Обед",
                },
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Ужин",
                },
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Перекус",
                }
                ];
        }

        public async Task<bool> AddItemAsync(MealTypeModel item)
        {
            _data.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((MealTypeModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<MealTypeModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<MealTypeModel>> GetAllItemsAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(MealTypeModel item)
        {
            var oldItem = _data.Where((MealTypeModel arg) => arg.Id == item.Id).FirstOrDefault();
            _data.Remove(oldItem);
            _data.Add(item);

            return await Task.FromResult(true);
        }
    }
}
