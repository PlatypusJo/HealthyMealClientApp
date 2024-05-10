using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Services.BLL;
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

        private string _searchBarText = string.Empty;

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
        private void Search(string searchText)
        {
            _searchBarText = searchText;
            PageIndex = 1;
            SwitchPageAndReloadData(PageIndex);
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
            IsVisible = RecipesToShow.Count > _pageSize;
            SwitchPageAndReloadData(PageIndex);
        }

        private async void SwitchPageAndReloadData(int pageNumber)
        {
            if (pageNumber < 0)
                return;

            int recipesCount = await LoadDataToShow(pageNumber);

            IsVisible = recipesCount > _pageSize;
            IsVisibleToPrevious = !(pageNumber == 1);
            IsVisibleToNext = !(pageNumber == recipesCount / _pageSize + 1);
            PageIndex = pageNumber;
        }

        private async Task<int> LoadDataToShow(int curPage)
        {
            RecipesToShow.Clear();

            GetRecipePageResponseModel response = await BlService.GetRecipePage(_userId, _pageSize, curPage, _searchBarText);
            foreach (RecipeModel food in response.Recipes)
            {
                RecipesToShow.Add(food);
            }

            return response.Count;
        }

        #endregion
    }
}
