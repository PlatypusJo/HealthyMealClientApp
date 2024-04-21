using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.Classes
{
    public class FoodDataStore : IDataStore<FoodModel>
    {
        readonly List<FoodModel> _data;

        public FoodDataStore() 
        { 
            
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

        public async Task<IEnumerable<FoodModel>> GetAllItemsAsync(bool forceRefresh = false)
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
