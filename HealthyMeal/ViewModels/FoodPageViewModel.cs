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

        private string _userId;

        private DateTime _date;

        List<FoodModel> _foods;

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

        [ObservableProperty]
        private ObservableCollection<FoodModel> _foodsToShow;

        [ObservableProperty]
        private List<MealTypeModel> _mealTypes;

        [ObservableProperty]
        private bool _isAdd;

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
        private async Task OpenSavingFoodPage(FoodModel food)
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string mealType = NavigationParameterConverter.ObjectToPairKeyValue(SelectedMealType, "MealType");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            string foodId = NavigationParameterConverter.ObjectToPairKeyValue(food.Id, "FoodId");
            string isEdit = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsEdit");
            await Shell.Current.GoToAsync($"{nameof(SavingFoodPage)}?{userId}&{mealType}&{date}&{foodId}&{isEdit}");
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

        #endregion

        #region Конструкторы

        public FoodPageViewModel()
        {
            LoadMealTypesAsync();
            _foods = [];
            FoodsToShow = [];
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
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
            if (query.ContainsKey("Date"))
            {
                string date = HttpUtility.UrlDecode(query["Date"]);
                _date = NavigationParameterConverter.ObjectFromPairKeyValue<DateTime>(date);
                OnPropertyChanged(nameof(Day));
            }
            if (query.ContainsKey("IsAdd"))
            {
                string isAdd = HttpUtility.UrlDecode(query["IsAdd"]);
                IsAdd = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(isAdd);
            }
        }

        public async void LoadDataAfterNavigation()
        {
            _foods = await GlobalDataStore.Foods.GetAllItemsAsync();
            IsVisible = _foods.Count > _pageSize;
            PageIndex = IsAdd ? 1 : PageIndex;
            SwitchPageAndReloadData(PageIndex);
        }

        #endregion

        #region Внутренние методы

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
