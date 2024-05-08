using HealthyMeal.Models;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.Services.MockDataStore
{
    public class StepDataStore : IDataStore<StepModel>
    {
        private readonly List<StepModel> _data;

        public StepDataStore()
        {
            _data = [
                new()
                {
                    Id = "1",
                    StepNumber = 1,
                    RecipeId = "1",
                    Description = "Вскипятите воду",
                },
                new()
                {
                    Id = "2",
                    StepNumber = 2,
                    RecipeId = "1",
                    Description = "Обжарьте морковь с луком на сковороде",
                },
                new()
                {
                    Id = "3",
                    StepNumber = 3,
                    RecipeId = "1",
                    Description = "Добавьте обжарку в кипящую воду",
                },
                new()
                {
                    Id = "4",
                    StepNumber = 4,
                    RecipeId = "1",
                    Description = "Варите 10 минут на медленном огне",
                },
                new()
                {
                    Id = "5",
                    StepNumber = 5,
                    RecipeId = "1",
                    Description = "Добавьте мелконатёртый сыр и варите ещё 15 минут",
                },
                new()
                {
                    Id = "6",
                    StepNumber = 1,
                    RecipeId = "2",
                    Description = "Слепите котлеты из фарша",
                },
                new()
                {
                    Id = "7",
                    StepNumber = 2,
                    RecipeId = "2",
                    Description = "Тушите котлеты на сковороде на медленном огне в течении 20-25 минут",
                },
                new()
                {
                    Id = "8",
                    StepNumber = 3,
                    RecipeId = "2",
                    Description = "Сварите фасоль",
                },
                new()
                {
                    Id = "9",
                    StepNumber = 4,
                    RecipeId = "2",
                    Description = "Наложите в тарелку фасоль с котлетами",
                },
                new()
                {
                    Id = "10",
                    StepNumber = 1,
                    RecipeId = "3",
                    Description = "Из муки и яиц слепите тесто",
                },
                new()
                {
                    Id = "11",
                    StepNumber = 2,
                    RecipeId = "3",
                    Description = "Добавьте мелконатёртой моркови в тесто",
                },
                new()
                {
                    Id = "12",
                    StepNumber = 3,
                    RecipeId = "3",
                    Description = "Поставьте заготовку в духовку и запекайте в течение 30 минут при 200 градусах",
                },
                new()
                {
                    Id = "13",
                    StepNumber = 1,
                    RecipeId = "4",
                    Description = "Сварите гречку в кастрюле 1/2 гречки и воды",
                },
                new()
                {
                    Id = "14",
                    StepNumber = 2,
                    RecipeId = "4",
                    Description = "Выложите сваренную гречку в тарелку",
                },
                new()
                {
                    Id = "15",
                    StepNumber = 3,
                    RecipeId = "4",
                    Description = "Полейте гречку мёдом",
                },
                ];
        }

        public async Task<bool> AddItemAsync(StepModel item)
        {
            _data.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _data.Where((StepModel arg) => arg.Id == id).FirstOrDefault();
            _data.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<StepModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<StepModel>> GetAllItemsAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<bool> UpdateItemAsync(StepModel item)
        {
            int index = _data.FindIndex((StepModel arg) => arg.Id == item.Id);
            _data[index] = item;

            return await Task.FromResult(true);
        }
    }
}
