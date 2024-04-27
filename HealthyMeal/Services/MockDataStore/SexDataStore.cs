using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.MockDataStore
{
    public class SexDataStore : IDataStore<SexModel>
    {
        readonly List<SexModel> _data;

        public SexDataStore() 
        {
            _data = [
                new()
                {
                    Id = "1",
                    Name = "Муж.",
                    Coeff = 5,
                },
                new()
                {
                    Id = "2",
                    Name = "Жен.",
                    Coeff = -161,
                }
                ];
        }

        public async Task<bool> AddItemAsync(SexModel item)
        {
            _data.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((SexModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<SexModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<SexModel>> GetAllItemsAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(SexModel item)
        {
            var oldItem = _data.Where((SexModel arg) => arg.Id == item.Id).FirstOrDefault();
            _data.Remove(oldItem);
            _data.Add(item);

            return await Task.FromResult(true);
        }
    }
}
