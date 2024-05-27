using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Services.BLL;
using HealthyMeal.Utils;
using HealthyMeal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class UserRecipesPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private readonly int _pageSize = 5;

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
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        [RelayCommand]
        private async Task OpenRecipeInfo(RecipeModel recipe)
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string recipeId = NavigationParameterConverter.ObjectToPairKeyValue(recipe.Id, "RecipeId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(recipe.MealTypeId, "MealTypeId");
            string IsAddToMenu = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsAddToMenu");
            string isOnlyInfo = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsOnlyInfo");
            await Shell.Current.GoToAsync($"{nameof(RecipeInfoPage)}?{userId}&{recipeId}&{IsAddToMenu}&{isOnlyInfo}&{mealTypeId}");
        }

        [RelayCommand]
        private async Task OpenCreateRecipePage(RecipeModel recipe)
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string isFromRecipes = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsFromRecipes");
            string isEdit = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsEdit");
            await Shell.Current.GoToAsync($"{nameof(RecipeCrudPage)}?{userId}&{isFromRecipes}&{isEdit}");
        }

        [RelayCommand]
        private async Task RemoveRecipe(RecipeModel recipe)
        {

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

        #region Конструкторы

        public UserRecipesPageViewModel()
        {
            
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query is null)
                return;

            bool isFromProfile = false;

            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }

            if (query.ContainsKey("IsFromProfile"))
            {
                string strBuf = HttpUtility.UrlDecode(query["IsFromProfile"]);
                isFromProfile = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(strBuf);
            }

            LoadDataAfterNavigation(isFromProfile);
        }


        #endregion

        #region Внутренние методы

        private void LoadDataAfterNavigation(bool isFromProfile)
        {
            if (isFromProfile)
            {
                _searchBarText = string.Empty;
                PageIndex = 1;
                SwitchPageAndReloadData(PageIndex);
            }
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
            foreach (RecipeModel recipe in response.Recipes)
            {
                RecipesToShow.Add(recipe);
            }

            return response.Count;
        }

        #endregion
    }
}
