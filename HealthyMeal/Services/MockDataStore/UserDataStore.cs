using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.MockDataStore
{
    public class UserDataStore : IDataStore<UserModel>
    {
        readonly List<UserModel> _data;

        public UserDataStore() 
        {
            _data = [
                new()
                {
                    Id = "1",
                    Name = "Иван",
                    Login = "LoVan",
                    Rdc = 2351.3,
                    KcalAmountGoal = 2000,
                    Age = 25,
                    Height = 176,
                    Weight = 73,
                    PhysicalActivityId = "2",
                    SexId = "1",
                }
                ];
        }

        public async Task<bool> AddItemAsync(UserModel item)
        {
            _data.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((UserModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<UserModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<UserModel>> GetAllItemsAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(UserModel item)
        {
            int index = _data.FindIndex((UserModel arg) => arg.Id == item.Id);
            _data[index] = item;

            return await Task.FromResult(true);
        }
    }
}
