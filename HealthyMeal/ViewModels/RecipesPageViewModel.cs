using CommunityToolkit.Mvvm.ComponentModel;
using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace HealthyMeal.ViewModels
{
    public partial class RecipesPageViewModel : BaseViewModel
    {
        #region Поля

        ObservableCollection<RecipeModel> _recipes;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private bool _isVisible = true;

        [ObservableProperty]
        private bool _isVisibleToNext;

        [ObservableProperty]
        private bool _isVisibleToPrevious;

        #endregion

        #region Свойства

        public ObservableCollection<RecipeModel> Recipes
        {
            get => _recipes;
        }

        #endregion

        #region Команды

        public ICommand NextPageCommand { get; private set; }

        public ICommand BackPageCommand { get; private set; }

        #endregion

        #region Конструктор

        public RecipesPageViewModel()
        {
            LoadRecipes();
            _isVisible = _recipes.Count > 0;
            _isVisibleToNext = true; 
            _isVisibleToPrevious = true;
        }

        #endregion

        #region Внутренние методы

        private void LoadRecipes()
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
    }
}
