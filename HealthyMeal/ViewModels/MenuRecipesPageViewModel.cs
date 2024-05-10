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
    public partial class MenuRecipesPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private readonly int _pageSize = 15;

        private string _userId = string.Empty;

        private DateTime _date;

        List<RecipeModel> _recipes = [];

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

        [ObservableProperty]
        private List<MealTypeModel> _mealTypes;

        [ObservableProperty]
        private MealTypeModel _selectedMealType;

        #endregion

        #region Свойства

        public string Day
        {
            get => _date.ToString("dd.MM, ddd").ToUpper();
        }

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
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(SelectedMealType.Id, "MealTypeId");
            string IsAddToMenu = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsAddToMenu");
            string isOnlyInfo = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsOnlyInfo");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            await Shell.Current.GoToAsync($"{nameof(RecipeInfoPage)}?{userId}&{recipeId}&{mealTypeId}&{IsAddToMenu}&{date}&{isOnlyInfo}");
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

        public MenuRecipesPageViewModel()
        {
            LoadMealTypesAsync();
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query is null)
                return;

            bool isFromMenu = false;
            SelectedMealType = MealTypes.Find(x => x.Type == MealType.Breakfast);

            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }

            if (query.ContainsKey("MealTypeId"))
            {
                string mealTypeId = HttpUtility.UrlDecode(query["MealTypeId"]);
                mealTypeId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(mealTypeId);
                SelectedMealType = MealTypes.Find(x => x.Id == mealTypeId);
            }

            if (query.ContainsKey("IsFromMenu"))
            {
                string strBuf = HttpUtility.UrlDecode(query["IsFromMenu"]);
                isFromMenu = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(strBuf);
            }

            if (query.ContainsKey("Date"))
            {
                string date = HttpUtility.UrlDecode(query["Date"]);
                _date = NavigationParameterConverter.ObjectFromPairKeyValue<DateTime>(date);
            }
            else
            {
                DateTime today = DateTime.Now;
                _date = new(today.Year, today.Month, today.Day);
            }

            OnPropertyChanged(nameof(Day));
            LoadDataAfterNavigation(isFromMenu);
        }


        #endregion

        #region Внутренние методы

        private async void LoadDataAfterNavigation(bool isFromMenu)
        {
            if (isFromMenu)
            {
                _searchBarText = string.Empty;
                PageIndex = 1;
                SwitchPageAndReloadData(PageIndex);
            }
        }

        private async void LoadMealTypesAsync()
        {
            MealTypes = await GlobalDataStore.MealTypes.GetAllItemsAsync();
            MealTypes = [.. MealTypes.OrderBy(x => x.Type)];
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
