using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HealthyMeal.ViewModels
{
    public partial class MenuPageViewModel : BaseViewModel
    {
        #region Поля

        ObservableCollection<RecipeModel> _recipes;

        #endregion

        #region ObservableProperties

        #endregion

        #region Свойства

        public ObservableCollection<RecipeModel> Recipes 
        {
            get => _recipes; 
        }

        #endregion

        #region Команды



        #endregion

        #region Конструктор

        public MenuPageViewModel() 
        {
            _recipes = new ObservableCollection<RecipeModel>()
            {
                new RecipeModel() 
                { 
                    Name = "Овощной суп",
                    Id = 1.ToString(),
                },
                new RecipeModel() 
                { 
                    Name = "Борщ",
                    Id = 2.ToString(),
                },
                new RecipeModel() 
                { 
                    Name = "Фруктовый салат",
                    Id = 3.ToString(),
                },
                new RecipeModel() 
                { 
                    Name = "Греческий салат",
                    Id = 4.ToString(),
                },
            };
        }

        #endregion

        #region Методы



        #endregion

        #region Внутренние методы



        #endregion
    }
}
