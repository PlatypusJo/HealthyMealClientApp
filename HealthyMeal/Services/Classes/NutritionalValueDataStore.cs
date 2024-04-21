using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.Classes
{
    public class NutritionalValueDataStore : IDataStore<NutritionalValueModel>
    {
        List<NutritionalValueModel> _data;

        public NutritionalValueDataStore() { }

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

        public async Task<IEnumerable<NutritionalValueModel>> GetAllItemsAsync(bool forceRefresh = false)
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
