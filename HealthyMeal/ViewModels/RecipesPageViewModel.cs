using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Utils;
using HealthyMeal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class RecipesPageViewModel : BaseViewModel
    {
        #region Поля

        private readonly int _pageSize = 15;

        private string _userId = string.Empty;

        List<RecipeModel> _recipes = [];

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private bool _isVisible = false;

        [ObservableProperty]
        private bool _isVisibleToNext = false;

        [ObservableProperty]
        private bool _isVisibleToPrevious = false;

        [ObservableProperty]
        private ObservableCollection<RecipeModel> _recipesToShow = [];

        #endregion

        #region Свойства


        #endregion

        #region Команды

        [RelayCommand]
        private async Task OpenRecipeInfo(RecipeModel recipe)
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string recipeId = NavigationParameterConverter.ObjectToPairKeyValue(recipe.Id, "RecipeId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(recipe.MealTypeId, "MealTypeId");
            string isAddToMenu = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsAddToMenu");
            string isOnlyInfo = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsOnlyInfo");
            await Shell.Current.GoToAsync($"{nameof(RecipeInfoPage)}?{userId}&{recipeId}&{mealTypeId}&{isAddToMenu}&{isOnlyInfo}");
        }

        [RelayCommand]
        private void NextPage()
        {
            SwitchPageAndReloadData(PageIndex + 1);
        }

        [RelayCommand]
        private void BackPage()
        {
            SwitchPageAndReloadData(PageIndex - 1);
        }

        [RelayCommand]
        private async Task Search(string searchText)
        {

        }

        #endregion

        #region Конструктор

        public RecipesPageViewModel()
        {
            _userId = "1";
        }

        #endregion

        #region Методы

        public void LoadDataAfterNavigation()
        {
            LoadRecipes();
        }

        #endregion

        #region Внутренние методы

        private async void LoadRecipes()
        {
            _recipes = await GlobalDataStore.Recipes.GetAllItemsAsync();
            IsVisible = _recipes.Count > _pageSize;
            SwitchPageAndReloadData(PageIndex);
        }

        private void SwitchPageAndReloadData(int pageNumber)
        {
            int index = pageNumber - 1;
            if (index < 0 || index > _recipes.Count / _pageSize)
                return;

            LoadDataToShow(index);

            IsVisibleToPrevious = !(pageNumber == 1);
            IsVisibleToNext = !(pageNumber == _recipes.Count / _pageSize + 1);
            PageIndex = pageNumber;
        }

        private void LoadDataToShow(int curIndexPage)
        {
            RecipesToShow.Clear();
            int startIndex = curIndexPage * _pageSize;
            for (int i = startIndex; i < _recipes.Count && i < startIndex + _pageSize; i++)
            {
                RecipesToShow.Add(_recipes[i]);
            }
        }

        #endregion
    }
}
