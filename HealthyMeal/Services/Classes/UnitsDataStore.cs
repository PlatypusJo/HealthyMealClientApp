using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.Classes
{
    public class UnitsDataStore : IDataStore<UnitsModel>
    {
        List<UnitsModel> _data;

        public UnitsDataStore() 
        {
            _data = [
                new()
                {
                    Id = "1",
                    Name = "г",
                },
                new()
                {
                    Id = "2",
                    Name = "шт",
                },
                new()
                {
                    Id = "3",
                    Name = "порция",
                },
                new()
                {
                    Id = "4",
                    Name = "л",
                },
                new()
                {
                    Id = "5",
                    Name = "мл",
                },
                new()
                {
                    Id = "6",
                    Name = "ч.ложка",
                },
                new()
                {
                    Id = "7",
                    Name = "ст.ложка",
                },
                new()
                {
                    Id = "8",
                    Name = "щепотка",
                },
                ];
        }

        public async Task<bool> AddItemAsync(UnitsModel item)
        {
            _data.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((UnitsModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<UnitsModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<UnitsModel>> GetAllItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(UnitsModel item)
        {
            var oldItem = _data.Where((UnitsModel arg) => arg.Id == item.Id).FirstOrDefault();
            _data.Remove(oldItem);
            _data.Add(item);

            return await Task.FromResult(true);
        }
    }
}
