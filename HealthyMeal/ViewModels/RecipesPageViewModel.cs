using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace HealthyMeal.ViewModels
{
    public class RecipesPageViewModel : BaseViewModel
    {
        #region Поля

        private int _pageIndex = 1;

        private bool _isVisible = true;

        private bool _isVisibleToNext;

        private bool _isVisibleToPrevious;

        ObservableCollection<RecipeModel> _recipes;

        #endregion

        #region Свойства

        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                _pageIndex = value;
                NotifyPropertyChanged(nameof(PageIndex));
            }
        }
        
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                NotifyPropertyChanged(nameof(IsVisible));
            }
        }
        
        public bool IsVisibleToNext
        {
            get => _isVisibleToNext;
            set
            {
                _isVisibleToNext = value;
                NotifyPropertyChanged(nameof(IsVisibleToNext));
            }
        }
        
        public bool IsVisibleToPrevious
        {
            get => _isVisibleToPrevious;
            set
            {
                _isVisibleToPrevious = value;
                NotifyPropertyChanged(nameof(IsVisibleToPrevious));
            }
        }

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
                    Id = 1,
                },
                new RecipeModel()
                {
                    Name = "Борщ",
                    Id = 2,
                },
                new RecipeModel()
                {
                    Name = "Фруктовый салат",
                    Id = 3,
                },
                new RecipeModel()
                {
                    Name = "Греческий салат",
                    Id = 4,
                },
            };
        }

        #endregion
    }
}
