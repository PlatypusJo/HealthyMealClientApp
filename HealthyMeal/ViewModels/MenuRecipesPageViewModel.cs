using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
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
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            await Shell.Current.GoToAsync($"{nameof(RecipeInfoPage)}?{userId}&{recipeId}&{mealTypeId}&{IsAddToMenu}&{date}");
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
            _recipes = await GlobalDataStore.Recipes.GetAllItemsAsync();
            IsVisible = _recipes.Count > _pageSize;

            if (isFromMenu)
            {
                PageIndex = 1;
                SwitchPageAndReloadData(PageIndex);
            }

        }

        private async void LoadMealTypesAsync()
        {
            MealTypes = await GlobalDataStore.MealTypes.GetAllItemsAsync();
            MealTypes = [.. MealTypes.OrderBy(x => x.Type)];
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
