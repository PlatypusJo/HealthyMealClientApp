using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.MockDataStore
{
    public class MealDataStore : IDataStore<MealModel>
    {
        readonly List<MealModel> _data;

        public MealDataStore()
        {
            _data = [];
        }

        public async Task<bool> AddItemAsync(MealModel item)
        {
            _data.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((MealModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<MealModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<MealModel>> GetAllItemsAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(MealModel item)
        {
            int index = _data.FindIndex((MealModel arg) => arg.Id == item.Id);
            _data[index] = item;

            return await Task.FromResult(true);
        }
    }
}
