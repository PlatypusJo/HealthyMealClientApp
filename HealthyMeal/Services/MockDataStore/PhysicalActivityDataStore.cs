using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.MockDataStore
{
    public class PhysicalActivityDataStore : IDataStore<PhysicalActivityModel>
    {
        readonly List<PhysicalActivityModel> _data;

        public PhysicalActivityDataStore() 
        {
            _data = [
                new()
                {
                    Id = "1",
                    Name = "Почти нет активности",
                    Description = "Вы ведёте сидячий образ жизни, не занимаетесь спортом",
                    FactorActivity = 1.2,
                },
                new()
                {
                    Id = "2",
                    Name = "Слабая активность",
                    Description = "Сидячий образ жизни и немного спорта - до трёх малоинтенсивных тренировок в неделю",
                    FactorActivity = 1.375,
                },
                new()
                {
                    Id = "3",
                    Name = "Средняя активность",
                    Description = "Интенсивные, но не тяжёлые тренировки три-четыре раза в неделю",
                    FactorActivity = 1.55,
                },
                new()
                {
                    Id = "4",
                    Name = "Высокая активность",
                    Description = "Ежедневные занятия спортом или ежедневная работа, связанная с большим количеством перемещений и ручного труда",
                    FactorActivity = 1.7,
                },
                new()
                {
                    Id = "5",
                    Name = "Экстремальная активность",
                    Description = "Для профессиональных спортсменов и людей, активно трудящихся",
                    FactorActivity = 1.9,
                }
                ];
        }

        public async Task<bool> AddItemAsync(PhysicalActivityModel item)
        {
            _data.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((PhysicalActivityModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<PhysicalActivityModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<PhysicalActivityModel>> GetAllItemsAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(PhysicalActivityModel item)
        {
            int index = _data.FindIndex((PhysicalActivityModel arg) => arg.Id == item.Id);
            _data[index] = item;

            return await Task.FromResult(true);
        }
    }
}
