using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Utils;
using HealthyMeal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class FoodPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private readonly int _pageSize = 15;

        private string _userId = string.Empty;

        private DateTime _date;

        List<FoodModel> _foods = [];

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
        private ObservableCollection<FoodModel> _foodsToShow = [];

        [ObservableProperty]
        private List<MealTypeModel> _mealTypes;

        [ObservableProperty]
        private MealTypeModel _selectedMealType;

        [ObservableProperty]
        private string _searchBarText;

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
        private async Task OpenSavingFoodPage(FoodModel food)
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(SelectedMealType.Id, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            string foodId = NavigationParameterConverter.ObjectToPairKeyValue(food.Id, "FoodId");
            string isEdit = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsEdit");
            await Shell.Current.GoToAsync($"{nameof(SavingFoodPage)}?{userId}&{mealTypeId}&{date}&{foodId}&{isEdit}");
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

        public FoodPageViewModel()
        {
            LoadMealTypesAsync();
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query is null)
                return;

            bool isFromDiary = false;
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
                
            if (query.ContainsKey("IsFromDiary"))
            {
                string strBuf = HttpUtility.UrlDecode(query["IsFromDiary"]);
                isFromDiary = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(strBuf);
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
            LoadDataAfterNavigation(isFromDiary);
        }


        #endregion

        #region Внутренние методы

        private async void LoadDataAfterNavigation(bool isFromDiary)
        {
            List<RecipeModel> recipes = await GlobalDataStore.Recipes.GetAllItemsAsync();
            _foods = await GlobalDataStore.Foods.GetAllItemsAsync();

            List<FoodModel> foods = [];
            for (int i = 0; i < recipes.Count; i++)
            {
                FoodModel food = _foods.Find(f => f.Id == recipes[i].FoodId);
                foods.Add(food);
            }

            _foods = _foods.Except(foods).ToList();
            IsVisible = _foods.Count > _pageSize;

            if (isFromDiary)
            {
                PageIndex = 1;
                SearchBarText = string.Empty;
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
            if (index < 0 || index > _foods.Count / _pageSize)
                return;

            LoadDataToShow(index);

            IsVisibleToPrevious = !(pageNumber == 1);
            IsVisibleToNext = !(pageNumber == _foods.Count / _pageSize + 1);
            PageIndex = pageNumber;
        }

        private void LoadDataToShow(int curIndexPage)
        {
            FoodsToShow.Clear();
            int startIndex = curIndexPage * _pageSize;
            for (int i = startIndex; i < _foods.Count && i < startIndex + _pageSize; i++)
            {
                FoodsToShow.Add(_foods[i]);
            }
        }

        #endregion
    }
}
